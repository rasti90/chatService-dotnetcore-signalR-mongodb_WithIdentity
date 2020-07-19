using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatServer.API.Model {
    public class ChatMember {
        [BsonRepresentation (BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonIgnore]
        public User User { get; set; }
    }
}