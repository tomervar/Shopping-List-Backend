using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShoppingListServer.Models
{
    public record CreateItem
    {
        [BsonElement("order_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; init; }

        [BsonElement("category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; init; }

        [BsonElement("item_name")]
        public string ItemName { get; init; }

    }
}
