using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingListServer.Models;

namespace ShoppingListServer.Services
{
    public class OrdersService
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public OrdersService(
            IOptions<ShoppingListDatabaseSettings> shoppingListDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                shoppingListDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                shoppingListDatabaseSettings.Value.DatabaseName);

            _ordersCollection = mongoDatabase.GetCollection<Order>(
                shoppingListDatabaseSettings.Value.OrdersCollectionName);
        }

        public async Task<List<Order>> GetAllOrdersAsync() =>
            await _ordersCollection.Find(_ => true).ToListAsync();

        public async Task<Order?> GetOrderByIdAsync(string order_id) =>
            await _ordersCollection.Find(o => o.OrderId == order_id).FirstOrDefaultAsync();

        public async Task<Order> CreateOrderAsync(Order newOrder)
        {
            await _ordersCollection.InsertOneAsync(newOrder);
            return newOrder;
        }

        public async Task<Order> UpdateOrderAsync(string id, Order updatedOrder)
        {
            var result = await _ordersCollection.ReplaceOneAsync(o => o.OrderId == id, updatedOrder);
            if (result is null)
            {
                return null;
            }
            return result.ModifiedCount == 0 ? null : updatedOrder;
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var result = await _ordersCollection.DeleteOneAsync(o => o.OrderId == id);
            return result.DeletedCount > 0;
        }
       
    }
}
