namespace ApiIntegrationTests.Infrastructure;

public interface IPublisher<T> where T : class
{
    Task PublishAsync(T data);
}
