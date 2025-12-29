namespace Core.Models
{
    public class CloudRegion
    {
        public string Name { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty; // AWS, Azure, GCP
        public string Endpoint { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty; // us-east-1, etc.
    }
}
