namespace TestMauiApp.Core.Models;

public class PortScanResult : IpToolResult
{
    public string IpAddress { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool IsOpen { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public long ResponseTimeMs { get; set; }
}

public class PortScanBatchResult : IpToolResult
{
    public string IpAddress { get; set; } = string.Empty;
    public List<PortScanResult> Results { get; set; } = new();
    public int TotalScanned { get; set; }
    public int OpenPorts { get; set; }
}
