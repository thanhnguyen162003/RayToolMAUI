namespace Core.Models;

public abstract class IpToolResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.Now;
}
