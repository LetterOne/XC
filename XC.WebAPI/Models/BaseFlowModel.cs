using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace XC.WebAPI.Models
{
    public class BaseFlowModel
    {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }     
    }
}
