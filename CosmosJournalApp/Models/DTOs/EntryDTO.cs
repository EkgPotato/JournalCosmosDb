namespace WebApi.Models.DTOs;

public class EntryDTO
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? Category { get; set; }
}
