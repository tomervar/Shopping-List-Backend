using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShoppingListServer.Models;

namespace ShoppingListServer.Services
{
    public class CategoriesService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoriesService(
            IOptions<ShoppingListDatabaseSettings> shoppingListDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                shoppingListDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                shoppingListDatabaseSettings.Value.DatabaseName);

            _categoriesCollection = mongoDatabase.GetCollection<Category>(
                shoppingListDatabaseSettings.Value.CategoriesCollectionName);
        }

        public async Task<List<Category>> GetAllCategoriesAsync() =>
            await _categoriesCollection.Find(_ => true).ToListAsync();

        public async Task<Category?> GetCategoryByIdAsync(string category_id) =>
            await _categoriesCollection.Find(c => c.CategoryId == category_id).FirstOrDefaultAsync();

        private async Task<Category?> GetCategoryByNameAsync(string category_name) =>
            await _categoriesCollection.Find(c => c.CategoryName == category_name).FirstOrDefaultAsync();
        private async Task<Category?> CategoryExist(string category_name)
        {
            var result = await GetCategoryByNameAsync(category_name);
            if (result is null) 
            {
                return null;
            }
            return result;
        }

        public async Task<Category?> CreateCategoryAsync(Category newCategory)
        {
            var existCategory = await CategoryExist(newCategory.CategoryName);
            if (existCategory is null)
            {
                await _categoriesCollection.InsertOneAsync(newCategory);
                return await GetCategoryByNameAsync(newCategory.CategoryName);
            }

            return existCategory;

        }
            
        public async Task<Category> UpdateCategoryAsync(string id, Category updatedCategory)
        {
            var result = await _categoriesCollection.ReplaceOneAsync(c => c.CategoryId == id, updatedCategory);
            if (result is null)
            {
                // Handle the case where the operation failed
                return null;
            }
            return result.ModifiedCount == 0 ? null : updatedCategory;
        }
            
        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var result = await _categoriesCollection.DeleteOneAsync(c => c.CategoryId == id);
            return result.DeletedCount > 0;
        }  
    }
}
