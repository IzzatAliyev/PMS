using MongoDB.Bson.Serialization.Attributes;

namespace PMS.Infrastructure.Entities
{
    public class Message : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Content { get; set; }
    }
}