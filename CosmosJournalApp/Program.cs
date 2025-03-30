using CosmosJournalApp.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddUserSecrets<Program>();

builder.Services.AddSwaggerGen();

CosmosClient client = new(
    builder.Configuration.GetValue<string>("CosmosDb:Endpoint"),
    builder.Configuration.GetValue<string>("CosmosDb:PrimaryKey")
    );
Database database = client.GetDatabase(builder.Configuration.GetValue<string>("CosmosDb:Database"));
Container container = database.GetContainer("Entries");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/entries", async (string title, string content, string category) =>
{
    var entry = new Entry()
    {
        Title = title,
        Content = content,
        Category = category,
        CreatedAt = DateTime.UtcNow
    };

    await container.CreateItemAsync(entry);
});

app.MapPut("/entries/{id}", async (string id, string title, string content, string category) =>
{
    var response = await container.ReadItemAsync<Entry>(id, new(id));

    var entry = response.Resource;

    entry.Title = title;
    entry.Content = content;
    entry.Category = category;
    entry.UpdatedAt = DateTime.UtcNow;

    await container.ReplaceItemAsync(entry, new(id));
});

app.MapDelete("/entries/{id}", async (string id) =>
{
    await container.DeleteItemAsync<Entry>(id, new(id));
});

app.MapGet("/entries", async () =>
{
    List<Entry> entries = new List<Entry>();
    FeedIterator<Entry> feedIterator = container.GetItemQueryIterator<Entry>();

    while (feedIterator.HasMoreResults)
    {
        FeedResponse<Entry> response = await feedIterator.ReadNextAsync();
        entries.AddRange(response.Resource);
    }

    return entries;
});

app.UseHttpsRedirection();

app.Run();
