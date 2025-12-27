using TestMauiApp.Core.Models;

namespace TestMauiApp.Core.Interfaces;

public interface IPortScannerService
{
    Task<PortScanResult> ScanPortAsync(string ipAddress, int port);
    Task<PortScanBatchResult> ScanPortRangeAsync(string ipAddress, int startPort, int endPort);
}
