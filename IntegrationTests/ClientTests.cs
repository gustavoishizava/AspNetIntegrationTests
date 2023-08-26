using ApiIntegrationTests.Domain;
using ApiIntegrationTests.Responses;
using FluentAssertions;
using IntegrationTests.Settings;

namespace IntegrationTests;

[TestClass]
public class ClientTests
{
    private readonly IntegrationTestFixture _integationTestFixture;

    public ClientTests()
    {
        _integationTestFixture = new();
    }

    [TestInitialize]
    public async Task InitAsync()
    {
        await _integationTestFixture.ClearCollectionAsync<Client>("clients");
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await _integationTestFixture.ClearCollectionAsync<Client>("clients");
    }

    [TestMethod]
    public async Task GetById_ClientNotFound_Should_Return_404()
    {
        using var httpClient = _integationTestFixture.ApiFactory.CreateClient();
        var response = await httpClient.GetAsync("api/clients/1");

        response.Should().Be404NotFound();
    }

    [TestMethod]
    public async Task GetById_ClientExists_Should_Return_200()
    {
        var client = new Client(1, "test");
        await _integationTestFixture.InsertDataAsync(client, "clients");

        using var httpClient = _integationTestFixture.ApiFactory.CreateClient();

        var response = await httpClient.GetAsync($"api/clients/{client.Id}");

        response.Should().Be200Ok();
        response.Should().BeAs(client);
    }

    [TestMethod]
    public async Task Post_IdAlreadyExists_Should_Return_400()
    {
        var client = new Client(1, "test");
        await _integationTestFixture.InsertDataAsync(client, "clients");

        using var httpClient = _integationTestFixture.ApiFactory.CreateClient();

        var response = await httpClient.PostAsJsonAsync($"api/clients", client);

        response.Should().Be400BadRequest();
        response.Should().BeAs(new ErrorResponse { Error = "Id=1 already exists." });
    }

    [TestMethod]
    public async Task Post_CreateClient_Should_Return_201()
    {
        var client = new Client(1, "test");

        using var httpClient = _integationTestFixture.ApiFactory.CreateClient();

        var response = await httpClient.PostAsJsonAsync($"api/clients", client);

        response.Should().Be201Created();
        response.Headers.Location.Should().NotBeNull();
        response.Headers.Location!.OriginalString.Should().Be("api/clients/1");
    }
}
