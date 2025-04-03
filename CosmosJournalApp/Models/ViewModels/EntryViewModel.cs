namespace WebApi.Models.ViewModels;
public class EntryViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
