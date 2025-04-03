using Microsoft.Azure.Cosmos;
using WebApi.Interfaces;

namespace WebApi.Services;

public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Database _database;
    public CosmosDbService(IConfiguration configuration)
    {
        _cosmosClient = new(
           configuration.GetValue<string>("CosmosDb:Endpoint"),
           configuration.GetValue<string>("CosmosDb:PrimaryKey")
         );

        _database = _cosmosClient.GetDatabase(configuration.GetValue<string>("CosmosDb:Database"));
    }

    public Container? GetContainer(string name) => _database.GetContainer(name);
}
