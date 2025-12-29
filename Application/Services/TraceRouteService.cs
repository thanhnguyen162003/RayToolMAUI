using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class TraceRouteService : ITraceRouteService
    {
        public async IAsyncEnumerable<TraceHop> TraceAsync(string hostNameOrIp, int maxHops = 30, int timeout = 1000)
        {
            using var ping = new Ping();
            
            // Resolve IP first to ensure we have a valid target
            IPAddress targetIp;
            try 
            {
                var addresses = await Dns.GetHostAddressesAsync(hostNameOrIp);
                targetIp = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork) ?? addresses.First();
            }
            catch
            {
                yield break;
            }

            for (int ttl = 1; ttl <= maxHops; ttl++)
            {
                var options = new PingOptions(ttl, true);
                var buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                
                PingReply reply = null;
                var sw = System.Diagnostics.Stopwatch.StartNew();
                
                try
                {
                    reply = await ping.SendPingAsync(targetIp, timeout, buffer, options);
                }
                catch
                {
                    // Ignore ping errors, treat as timeout if no reply
                }

                sw.Stop();

                var hop = new TraceHop
                {
                    HopNumber = ttl,
                    RoundTripTime = sw.ElapsedMilliseconds
                };

                if (reply != null)
                {
                    if (reply.Status == IPStatus.Success || reply.Status == IPStatus.TtlExpired)
                    {
                        hop.IpAddress = reply.Address?.ToString() ?? "*";
                        hop.Status = "Success";
                        
                        // Try resolve hostname
                        if (!string.IsNullOrEmpty(hop.IpAddress) && hop.IpAddress != "*")
                        {
                            try
                            {
                                var hostEntry = await Dns.GetHostEntryAsync(reply.Address);
                                hop.Hostname = hostEntry.HostName;
                            }
                            catch
                            {
                                hop.Hostname = hop.IpAddress;
                            }
                        }
                    }
                    else
                    {
                        hop.IpAddress = "*";
                        hop.Status = reply.Status.ToString();
                    }
                }
                else
                {
                    hop.IpAddress = "*";
                    hop.Status = "TimedOut";
                }

                yield return hop;

                if (reply?.Status == IPStatus.Success)
                {
                    break;
                }
            }
        }
    }
}
