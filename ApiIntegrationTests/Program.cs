using ApiIntegrationTests.Data;
using ApiIntegrationTests.Domain;
using ApiIntegrationTests.Infrastructure;
using Azure.Messaging.ServiceBus.Administration;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton((_) =>
{
    var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("MongoDB"));
    return mongoClient.GetDatabase(builder.Configuration["DatabaseName"]);
});

builder.Services.AddSingleton<IClientRepository, ClientRepository>();

builder.Services.AddSingleton<IPublisher<Client>, ServiceBusPublisher>();
builder.Services.AddSingleton((_) => new ServiceBusAdministrationClient(builder.Configuration.GetConnectionString("ServiceBus")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }