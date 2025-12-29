using Core.Models;

namespace Core.Interfaces;

public interface IIpLookupService
{
    Task<IpLookupResult> LookupIpAsync(string ipAddress);
    Task<string> GetMyPublicIpAsync();
}
