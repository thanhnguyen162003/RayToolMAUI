using Core.Models;

namespace Core.Interfaces;

public interface IDnsLookupService
{
    Task<DnsLookupResult> LookupAsync(string domainName);
}
