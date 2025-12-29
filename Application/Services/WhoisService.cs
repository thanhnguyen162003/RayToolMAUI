using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class WhoisService : IWhoisService
    {
        private const int WhoisPort = 43;

        public async Task<WhoisRecord> LookupAsync(string domain)
        {
            var result = new WhoisRecord { Domain = domain };

            try
            {
                // 1. Query IANA to find the TLD server
                string tldServer = await QueryWhoisServer("whois.iana.org", domain);
                string referralServer = ParseReferralServer(tldServer);

                result.RawText = tldServer;

                // 2. Query the specific registrar server if found
                if (!string.IsNullOrEmpty(referralServer))
                {
                    string specificWhois = await QueryWhoisServer(referralServer, domain);
                    result.RawText = specificWhois;
                    result.Registrar = ParseRegistrar(specificWhois);
                    result.CreationDate = ParseDate(specificWhois, "Creation Date:");
                    result.ExpiryDate = ParseDate(specificWhois, "Registry Expiry Date:");
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        private async Task<string> QueryWhoisServer(string server, string domain)
        {
            using var client = new TcpClient();
            await client.ConnectAsync(server, WhoisPort);
            
            using var stream = client.GetStream();
            using var reader = new StreamReader(stream, Encoding.ASCII);
            using var writer = new StreamWriter(stream, Encoding.ASCII);

            await writer.WriteLineAsync(domain);
            await writer.FlushAsync();

            return await reader.ReadToEndAsync();
        }

        private string ParseReferralServer(string whoisText)
        {
            foreach (var line in whoisText.Split('\n'))
            {
                if (line.StartsWith("refer:", StringComparison.OrdinalIgnoreCase))
                {
                    return line.Substring(6).Trim();
                }
            }
            return string.Empty;
        }

        private string ParseRegistrar(string whoisText)
        {
             foreach (var line in whoisText.Split('\n'))
            {
                if (line.Trim().StartsWith("Registrar:", StringComparison.OrdinalIgnoreCase))
                {
                    return line.Substring(10).Trim();
                }
            }
            return string.Empty;
        }
        
        private DateTime? ParseDate(string text, string label)
        {
            foreach (var line in text.Split('\n'))
            {
                if (line.Trim().StartsWith(label, StringComparison.OrdinalIgnoreCase))
                {
                    var dateStr = line.Substring(label.Length).Trim();
                    if (DateTime.TryParse(dateStr, out var date))
                    {
                        return date;
                    }
                }
            }
            return null;
        }
    }
}
