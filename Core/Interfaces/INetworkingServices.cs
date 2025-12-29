using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ISubnetCalculator
    {
        SubnetInfo Calculate(string ipAddress, int cidr);
        bool IsValidIp(string ipAddress);
    }

    public interface ITraceRouteService
    {
        IAsyncEnumerable<TraceHop> TraceAsync(string hostNameOrIp, int maxHops = 30, int timeout = 1000);
    }

    public interface IWhoisService
    {
        Task<WhoisRecord> LookupAsync(string domain);
    }

    public interface IMacAddressService
    {
        Task<MacVendorInfo> LookupVendorAsync(string macAddress);
    }
}
