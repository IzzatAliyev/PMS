using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PMS.Infrastructure.Entities;

namespace PMS.Service.Services
{
    public class ChatService
    {
        private readonly IMongoCollection<Message> messageCollection;

        public ChatService(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("ChatConnection");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("chat");
            this.messageCollection = database.GetCollection<Message>("message");
        }

        public async Task<List<Message>> Get() =>
            await this.messageCollection.Find(_ => true).ToListAsync();

        public async Task<Message> Get(string id) =>
            await this.messageCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task Create(Message message) =>
            await this.messageCollection.InsertOneAsync(message);

        public async Task Update(string id, Message updateMessage) =>
            await this.messageCollection.ReplaceOneAsync(m => m.Id == id, updateMessage);

        public async Task Remove(string id) =>
            await this.messageCollection.DeleteOneAsync(m => m.Id == id);
    }
}