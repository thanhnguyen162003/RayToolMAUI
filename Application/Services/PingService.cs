using System.Net.NetworkInformation;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services;

public class PingService : IPingService
{
    public async Task<PingResult> PingAsync(string hostNameOrAddress, int count = 4)
    {
        var result = new PingResult
        {
            HostName = hostNameOrAddress,
            PacketsSent = count,
            Success = true
        };

        try
        {
            using var ping = new Ping();
            var roundtripTimes = new List<long>();
            int received = 0;

            for (int i = 0; i < count; i++)
            {
                try
                {
                    var reply = await ping.SendPingAsync(hostNameOrAddress, 5000); // 5 second timeout
                    
                    if (reply.Status == IPStatus.Success)
                    {
                        received++;
                        roundtripTimes.Add(reply.RoundtripTime);
                        
                        if (string.IsNullOrEmpty(result.IpAddress))
                        {
                            result.IpAddress = reply.Address.ToString();
                            result.Ttl = reply.Options?.Ttl ?? 0;
                        }
                    }
                }
                catch
                {
                }

                // Small delay between pings
                if (i < count - 1)
                    await Task.Delay(100);
            }

            result.PacketsReceived = received;
            result.PacketsLost = count - received;
            result.IsReachable = received > 0;
            result.RoundtripTime = roundtripTimes.Any() ? (long)roundtripTimes.Average() : 0;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
            result.IsReachable = false;
        }

        return result;
    }
}
