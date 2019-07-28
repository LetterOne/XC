using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace XC.WebAPI.Models
{
    /// <summary>
    /// 需要在将公共语言运行时 (CLR) 对象映射到 MongoDB 集合时使用。
    ///使用[BsonId] 进行批注，以将此属性指定为文档的主键。
    ///使用[BsonRepresentation(BsonType.ObjectId)] 进行批注，以允许将参数作为类型 string 而不是 ObjectId 结构进行传递。Mongo 处理从 string 到 ObjectId 的转换。
    /// </summary>
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string BookName { get; set; }
       //[BsonElement("价格")]
        public decimal Price { get; set; }
        //[BsonElement("目录")]
        public string Category { get; set; }
        //[BsonElement("作者")]
        public string Author { get; set; }
    }
}