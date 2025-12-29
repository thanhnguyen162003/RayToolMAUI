namespace Core.Models;

public class DnsLookupResult : IpToolResult
{
    public string DomainName { get; set; } = string.Empty;
    public List<string> IpAddresses { get; set; } = new();
    public List<string> NameServers { get; set; } = new();
    public List<string> MailServers { get; set; } = new();
    public string RecordType { get; set; } = "A";
}
