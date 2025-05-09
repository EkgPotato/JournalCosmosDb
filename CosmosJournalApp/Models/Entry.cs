﻿using Newtonsoft.Json;

namespace CosmosJournalApp.Models;
public class Entry
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt {  get; set; }
    public DateTime? UpdatedAt {  get; set; }
}
