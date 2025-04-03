using AutoMapper;
using FluentValidation;
using Microsoft.Azure.Cosmos;
using WebApi.Common.Request;
using WebApi.Common.Response;
using WebApi.Interfaces;
using WebApi.Models.DTOs;
using WebApi.Models.Entities;
using WebApi.Models.ViewModels;

namespace WebApi.Services;

public class EntryService : IEntryService
{
    private readonly Container _container;
    private readonly IValidator<EntryDTO> _validator;
    private readonly IMapper _mapper;
    public EntryService(IConfiguration configuration, ICosmosDbService cosmosDbService, IValidator<EntryDTO> validator, IMapper mapper)
    {
        _container = cosmosDbService.GetContainer(configuration.GetValue<string>("CosmosDb:Containers:Entry")!)!;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task CreateEntry(EntryDTO entryDTO)
    {
        await _validator.ValidateAndThrowAsync(entryDTO);

        var entry = _mapper.Map<Entry>(entryDTO);

        entry.CreatedAt = DateTime.UtcNow;

        await _container.CreateItemAsync(entry);
    }

    public async Task UpdateEntry(string id, EntryDTO entryDTO)
    {
        await _validator.ValidateAndThrowAsync(entryDTO);

        var response = await _container.ReadItemAsync<Entry>(id, new PartitionKey(id));
        if (response.Resource == null)
            throw new ArgumentException("Invalid id");

        var entry = response.Resource;

        entry.Title = entryDTO.Title;
        entry.Content = entryDTO.Content;
        entry.Category = entryDTO.Category;
        entry.UpdatedAt = DateTime.UtcNow;

        await _container.ReplaceItemAsync(entry, id, new PartitionKey(id));
    }

    public async Task DeleteEntry(string id)
    {
        await _container.DeleteItemAsync<Entry>(id, new PartitionKey(id));
    }

    public async Task<PagedResponse<EntryViewModel>> GetEntries(GridRequest request)
    {
        List<Entry> entries = new List<Entry>();
        FeedIterator<Entry> feedIterator = _container.GetItemQueryIterator<Entry>();

        while (feedIterator.HasMoreResults)
        {
            FeedResponse<Entry> response = await feedIterator.ReadNextAsync();
            entries.AddRange(response);
        }

        var pagedResponse = new PagedResponse<EntryViewModel>()
        {
            Items = entries.Select(_mapper.Map<EntryViewModel>),
            TotalItemsCount = entries.Count()
        };

        return pagedResponse;
    }

    public async Task<EntryViewModel> GetEntry(string id)
    {
        var response = await _container.ReadItemAsync<EntryViewModel>(id, new PartitionKey(id));
        return _mapper.Map<EntryViewModel>(response.Resource);
    }
}
