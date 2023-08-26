using ApiIntegrationTests.Domain;
using MongoDB.Driver;

namespace ApiIntegrationTests.Data
{
    public sealed class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _collection;

        public ClientRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Client>("clients");
        }

        public async Task AddAsync(Client client)
        {
            await _collection.InsertOneAsync(client);
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _collection.Find(Builders<Client>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();
        }
    }
}

