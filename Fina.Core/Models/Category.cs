namespace Fina.Core.Models;

public record Category 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string UserId { get; set; }
}