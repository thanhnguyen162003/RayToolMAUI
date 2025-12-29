using Core.Models;

namespace Core.Interfaces;

public interface IPortScannerService
{
    Task<PortScanResult> ScanPortAsync(string ipAddress, int port);
    Task<PortScanBatchResult> ScanPortRangeAsync(string ipAddress, int startPort, int endPort);
    IAsyncEnumerable<PortScanResult> ScanPortsStreamAsync(string ipAddress, int startPort, int endPort, int concurrency = 100);
}
