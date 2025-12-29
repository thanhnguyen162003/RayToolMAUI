namespace Core.Models;

public class PingResult : IpToolResult
{
    public string HostName { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public long RoundtripTime { get; set; }
    public int Ttl { get; set; }
    public bool IsReachable { get; set; }
    public int PacketsSent { get; set; }
    public int PacketsReceived { get; set; }
    public int PacketsLost { get; set; }
}
