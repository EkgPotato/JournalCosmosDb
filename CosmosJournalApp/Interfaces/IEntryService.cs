using WebApi.Common.Request;
using WebApi.Common.Response;
using WebApi.Models.DTOs;
using WebApi.Models.ViewModels;

namespace WebApi.Interfaces;
public interface IEntryService
{
    Task<PagedResponse<EntryViewModel>> GetEntries(GridRequest request);
    Task<EntryViewModel> GetEntry(string id);
    Task CreateEntry(EntryDTO entryDTO);
    Task UpdateEntry(string id, EntryDTO entryDTO);
    Task DeleteEntry(string id);
}