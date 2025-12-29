namespace Core.Models
{
    public class LatencyResult
    {
        public CloudRegion Region { get; set; } = new();
        public long LatencyMs { get; set; } = -1;
        public bool IsSuccess { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
    }
}
