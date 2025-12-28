using System.Net.Sockets;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services;

public class PortScannerService : IPortScannerService
{
    public async Task<PortScanResult> ScanPortAsync(string ipAddress, int port)
    {
        var result = new PortScanResult
        {
            IpAddress = ipAddress,
            Port = port,
            Success = true
        };

        try
        {
            var startTime = DateTime.Now;
            using var tcpClient = new TcpClient();
            var connectTask = tcpClient.ConnectAsync(ipAddress, port);
            var timeoutTask = Task.Delay(2000); // 2 second timeout

            var completedTask = await Task.WhenAny(connectTask, timeoutTask);
            var endTime = DateTime.Now;

            if (completedTask == connectTask && tcpClient.Connected)
            {
                result.IsOpen = true;
                result.ResponseTimeMs = (long)(endTime - startTime).TotalMilliseconds;
                result.ServiceName = GetServiceName(port);
            }
            else
            {
                result.IsOpen = false;
            }
        }
        catch (Exception ex)
        {
            result.IsOpen = false;
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<PortScanBatchResult> ScanPortRangeAsync(string ipAddress, int startPort, int endPort)
    {
        var batchResult = new PortScanBatchResult
        {
            IpAddress = ipAddress,
            Success = true
        };

        try
        {
            var tasks = new List<Task<PortScanResult>>();
            
            for (int port = startPort; port <= endPort; port++)
            {
                tasks.Add(ScanPortAsync(ipAddress, port));
            }

            var results = await Task.WhenAll(tasks);
            batchResult.Results = results.Where(r => r.IsOpen).ToList();
            batchResult.TotalScanned = results.Length;
            batchResult.OpenPorts = batchResult.Results.Count;
        }
        catch (Exception ex)
        {
            batchResult.Success = false;
            batchResult.ErrorMessage = ex.Message;
        }

        return batchResult;
    }

    public async IAsyncEnumerable<PortScanResult> ScanPortsStreamAsync(string ipAddress, int startPort, int endPort, int concurrency = 100)
    {
        var semaphore = new SemaphoreSlim(concurrency);
        var tasks = new List<Task<PortScanResult>>();

        for (int port = startPort; port <= endPort; port++)
        {
            await semaphore.WaitAsync();

            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    return await ScanPortAsync(ipAddress, port);
                }
                finally
                {
                    semaphore.Release();
                }
            }));

            var completedTasks = tasks.Where(t => t.IsCompleted).ToList();
            foreach (var task in completedTasks)
            {
                tasks.Remove(task);
                yield return await task;
            }
        }

        await Task.WhenAll(tasks);
        foreach (var task in tasks)
        {
            yield return await task;
        }
    }

    private static string GetServiceName(int port)
    {
        return port switch
        {
            21 => "FTP",
            22 => "SSH",
            23 => "Telnet",
            25 => "SMTP",
            53 => "DNS",
            80 => "HTTP",
            110 => "POP3",
            143 => "IMAP",
            443 => "HTTPS",
            3306 => "MySQL",
            3389 => "RDP",
            5432 => "PostgreSQL",
            8080 => "HTTP Alternate",
            _ => "Unknown"
        };
    }
}
