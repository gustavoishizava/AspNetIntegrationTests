using ApiIntegrationTests.Domain;
using Azure.Messaging.ServiceBus.Administration;

namespace ApiIntegrationTests.Infrastructure;

public sealed class ServiceBusPublisher : IPublisher<Client>
{
    private readonly ServiceBusAdministrationClient _serviceBusAdminClient;

    public ServiceBusPublisher(ServiceBusAdministrationClient serviceBusAdminClient)
    {
        _serviceBusAdminClient = serviceBusAdminClient;
    }

    public async Task PublishAsync(Client data)
    {
        await _serviceBusAdminClient.TopicExistsAsync("client");
    }
}
