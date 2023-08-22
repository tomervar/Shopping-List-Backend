using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingListServer.Models
{
    [BsonIgnoreExtraElements]
    public record Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("category_id")]
        public string CategoryId { get; init; }

        [BsonElement("category_name")]
        public string CategoryName { get; init; }

        [BsonElement("category_img_location")]
        public string CategoryImgLocation { get; init; }
    }
}
