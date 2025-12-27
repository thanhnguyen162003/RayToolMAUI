using System.Net;
using TestMauiApp.Core.Interfaces;
using TestMauiApp.Core.Models;

namespace TestMauiApp.Application.Services;

public class DnsLookupService : IDnsLookupService
{
    public async Task<DnsLookupResult> LookupAsync(string domainName)
    {
        var result = new DnsLookupResult
        {
            DomainName = domainName,
            Success = true
        };

        try
        {
            // Get IP addresses
            var hostEntry = await Dns.GetHostEntryAsync(domainName);
            result.IpAddresses = hostEntry.AddressList
                .Select(ip => ip.ToString())
                .ToList();

            // Note: .NET's Dns class doesn't provide NS and MX records directly
            // For a production app, you might want to use a library like DnsClient.NET
            // For now, we'll just show IP addresses
            result.RecordType = "A/AAAA";
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}
