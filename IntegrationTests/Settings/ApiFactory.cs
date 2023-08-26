using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

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

        });
    }
}
