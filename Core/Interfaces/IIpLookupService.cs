using TestMauiApp.Core.Models;

namespace TestMauiApp.Core.Interfaces;

public interface IIpLookupService
{
    Task<IpLookupResult> LookupIpAsync(string ipAddress);
    Task<string> GetMyPublicIpAsync();
}
