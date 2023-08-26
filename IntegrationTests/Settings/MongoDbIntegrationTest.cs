using Mongo2Go;

namespace IntegrationTests.Settings;

public sealed class MongoDbIntegrationTest
{
    private MongoDbRunner? _mongoDbRunner;

    public string GetConnectionString()
    {
        _mongoDbRunner = MongoDbRunner.Start();
        return _mongoDbRunner.ConnectionString;
    }
}
