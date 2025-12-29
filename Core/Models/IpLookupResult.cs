namespace Core.Models;

public class IpLookupResult : IpToolResult
{
    public string IpAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public string Isp { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string Timezone { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Zip { get; set; } = string.Empty;
    public string Continent { get; set; } = string.Empty;
    public string ContinentCode { get; set; } = string.Empty;
    public string As { get; set; } = string.Empty;
    public string AsName { get; set; } = string.Empty;
    public bool IsMobile { get; set; }
    public bool IsProxy { get; set; }
    public bool IsHosting { get; set; }
}
