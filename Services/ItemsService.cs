using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingListServer.Models;

namespace ShoppingListServer.Services
{
    public class ItemsService
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public ItemsService(
            IOptions<ShoppingListDatabaseSettings> shoppingListDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                shoppingListDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                shoppingListDatabaseSettings.Value.DatabaseName);

            _itemsCollection = mongoDatabase.GetCollection<Item>(
                shoppingListDatabaseSettings.Value.ItemsCollectionName);
        }

        public async Task<List<Item>> GetAllItemsAsync() =>
            await _itemsCollection.Find(_ => true).ToListAsync();

        public async Task<Item?> GetItemByIdAsync(string item_id) =>
            await _itemsCollection.Find(i => i.ItemId == item_id).FirstOrDefaultAsync();

        private async Task<Item?> GetItemByOrderIdCategoryIdAndNameAsync(string order_id, string category_id, string item_name) =>
            await _itemsCollection.Find(
                i => i.OrderId == order_id &
                i.CategoryId == category_id &
                i.ItemName == item_name
                ).FirstOrDefaultAsync();

        public async Task<Item> CreateItemAsync(Item newItem)
        {
            var item = await GetItemByOrderIdCategoryIdAndNameAsync(newItem.OrderId, newItem.CategoryId, newItem.ItemName);
            if (item != null)
            {
                var updateItem = new Item { 
                    ItemId=item.ItemId,
                    OrderId=item.OrderId,
                    CategoryId=item.CategoryId,
                    ItemName=item.ItemName,
                    Quantity=item.Quantity +1,
                };
                return await UpdateItemAsync(updateItem.ItemId, updateItem);
            }
            await _itemsCollection.InsertOneAsync(newItem);
            return await GetItemByOrderIdCategoryIdAndNameAsync(newItem.OrderId, newItem.CategoryId, newItem.ItemName); ;
        }
            
        public async Task<Item> UpdateItemAsync(string id, Item updatedItem)
        {
            var result = await _itemsCollection.ReplaceOneAsync(i => i.ItemId == id, updatedItem);
            if (result is null)
            {
                // Handle the case where the operation failed
                return null;
            }
            return result.ModifiedCount == 0 ? null : updatedItem;
        }
            
        public async Task<bool> DeleteItemAsync(string id)
        {
            var result = await _itemsCollection.DeleteOneAsync(i => i.ItemId == id);
            return result.DeletedCount > 0;
        }     
    }
}
