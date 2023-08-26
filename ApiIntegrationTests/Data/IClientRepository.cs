using ApiIntegrationTests.Domain;

namespace ApiIntegrationTests.Data
{
    public interface IClientRepository
	{
		Task AddAsync(Client client);
		Task<Client> GetByIdAsync(int id);
	}
}
