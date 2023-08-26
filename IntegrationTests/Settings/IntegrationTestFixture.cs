using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IntegrationTests.Settings;

public sealed class IntegrationTestFixture
{
    public ApiFactory<Program> ApiFactory;

    public IntegrationTestFixture()
    {
        ApiFactory = new ApiFactory<Program>();
    }

    public async Task ClearCollectionAsync<TCollection>(string collectionName) where TCollection : class
    {
        var database = ApiFactory.Services.GetRequiredService<IMongoDatabase>();
        await database.GetCollection<TCollection>(collectionName).DeleteManyAsync("{}");
    }

    public async Task InsertDataAsync<TData>(TData data, string collectionName) where TData : class
    {
        var database = ApiFactory.Services.GetRequiredService<IMongoDatabase>();
        await database.GetCollection<TData>(collectionName).InsertOneAsync(data);
    }
}
