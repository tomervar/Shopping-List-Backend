using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ShoppingListServer.Models
{
    [BsonIgnoreExtraElements]
    public record Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("order_id")]
        public string OrderId { get; init; }

        [BsonElement("items")]
        public List<Item> Items { get; init; }

    }
}
