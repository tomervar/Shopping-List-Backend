using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShoppingListServer.Models
{
    [BsonIgnoreExtraElements]
    public record Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("item_id")]
        public string ItemId { get; init; }

        [BsonElement("order_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; init; }

        [BsonElement("category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; init; }

        [BsonElement("item_name")]
        public string ItemName { get; init; }

        public int Quantity { get; init; }

    }
}
