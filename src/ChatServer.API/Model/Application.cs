using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatServer.API.Model {
    public class Application {
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string APIKey { get; set; }
    }
}