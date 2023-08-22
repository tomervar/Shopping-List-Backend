using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShoppingListServer.Models
{
    [BsonIgnoreExtraElements]
    public record CreateCategory
    {
        [BsonElement("category_name")]
        public string CategoryName { get; init; }

        [BsonElement("category_img_location")]
        public string CategoryImgLocation { get; init; }
    }
}
