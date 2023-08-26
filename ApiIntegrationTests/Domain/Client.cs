using MongoDB.Bson.Serialization.Attributes;

namespace ApiIntegrationTests.Domain
{
    public sealed class Client
    {
        [BsonId]
        public int Id { get; set; }

        public string Name { get; set; }

        public Client(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
