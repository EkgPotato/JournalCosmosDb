using Microsoft.Azure.Cosmos;

namespace WebApi.Interfaces;
public interface ICosmosDbService
{
    Container? GetContainer(string name);
}
