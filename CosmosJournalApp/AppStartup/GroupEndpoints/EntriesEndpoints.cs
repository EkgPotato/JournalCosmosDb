using WebApi.Common.Request;
using WebApi.Interfaces;
using WebApi.Models.DTOs;

namespace WebApi.AppStartup.GroupEndpoints
{
    public static class EntriesEndpoints
    {
        public static RouteGroupBuilder MapEntriesApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IEntryService service, GridRequest request) => await service.GetEntries(request));
            group.MapGet("/{id}", async (IEntryService service, string id) => await service.GetEntry(id));
            group.MapPost("/", async (IEntryService service, EntryDTO entry) => await service.CreateEntry(entry));
            group.MapPut("/{id}", async (IEntryService service, string id, EntryDTO entry) => await service.UpdateEntry(id, entry));
            group.MapDelete("/{id}", async (IEntryService service, string id) => await service.DeleteEntry(id));

            return group;
        }
    }
}
