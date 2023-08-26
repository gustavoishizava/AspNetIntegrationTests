using ApiIntegrationTests.Domain;
using ApiIntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Moq.AutoMock;

namespace IntegrationTests.Settings;

public sealed class ApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Alterar ambiente para testing
        builder.UseEnvironment("Testing");

        // Adicionar/Remover dependências
        builder.ConfigureServices(services =>
        {
            #region Mock do publisher do SB

            var serviceBusPublisherDescriptor = services.First(x => x.ServiceType == typeof(IPublisher<Client>));
            services.Remove(serviceBusPublisherDescriptor);

            var autoMocker = new AutoMocker();
            var serviceBusPublisher = autoMocker.CreateInstance<ServiceBusPublisher>();
            services.AddSingleton<IPublisher<Client>>(serviceBusPublisher);

            #endregion

            #region Trocando o MongoDB pelo Mongo2Go

            var mongoDbDescriptor = services.First(x => x.ServiceType == typeof(IMongoDatabase));
            services.Remove(mongoDbDescriptor);

            services.AddSingleton((_) =>
            {
                var mongoClient = new MongoClient(MongoDbIntegrationTest.GetConnectionString());
                return mongoClient.GetDatabase("api_db_integration_test_m2g");
            });

            #endregion
        });
    }
}
