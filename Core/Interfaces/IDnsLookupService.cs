using TestMauiApp.Core.Models;

namespace TestMauiApp.Core.Interfaces;

public interface IDnsLookupService
{
    Task<DnsLookupResult> LookupAsync(string domainName);
}
