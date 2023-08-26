using Mongo2Go;

namespace IntegrationTests.Settings;

public static class MongoDbIntegrationTest
{
    private static MongoDbRunner? _mongoDbRunner;

    public static string GetConnectionString()
    {
        _mongoDbRunner ??= MongoDbRunner.Start();
        return _mongoDbRunner.ConnectionString;
    }
}
