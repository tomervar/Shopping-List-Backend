namespace ShoppingListServer.Models
{
    public class ShoppingListDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ItemsCollectionName { get; set; } = null!;

        public string CategoriesCollectionName { get; set; } = null!;

        public string OrdersCollectionName { get; set; } = null!;
    }
}
