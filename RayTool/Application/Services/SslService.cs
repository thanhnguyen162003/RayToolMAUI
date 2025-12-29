using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class SslService : ISslService
    {
        public async Task<SslCertificateInfo> GetCertificateInfoAsync(string domain)
        {
            try
            {
                // Clean input
                domain = domain.Replace("https://", "").Replace("http://", "").TrimEnd('/');
                
                using var client = new TcpClient();
                await client.ConnectAsync(domain, 443);

                using var sslStream = new SslStream(client.GetStream(), false, 
                    new RemoteCertificateValidationCallback(ValidateServerCertificate));

                await sslStream.AuthenticateAsClientAsync(domain);

                var cert = sslStream.RemoteCertificate as X509Certificate2;
                if (cert == null)
                {
                    return new SslCertificateInfo { IsValid = false, ValidationErrors = "No certificate received." };
                }

                var info = new SslCertificateInfo
                {
                    Subject = cert.Subject,
                    Issuer = cert.Issuer,
                    NotBefore = cert.NotBefore,
                    NotAfter = cert.NotAfter,
                    SerialNumber = cert.SerialNumber,
                    Thumbprint = cert.Thumbprint,
                    IsValid = true, // Basic check, detailed validation is complex
                    Protocol = sslStream.SslProtocol.ToString(),
                    Algorithm = cert.GetKeyAlgorithm(),
                    HasPrivateKey = cert.HasPrivateKey
                };

                // Simple validity check based on dates
                if (DateTime.Now < info.NotBefore || DateTime.Now > info.NotAfter)
                {
                    info.IsValid = false;
                    info.ValidationErrors = "Certificate is expired or not yet valid.";
                }

                return info;
            }
            catch (Exception ex)
            {
                return new SslCertificateInfo 
                { 
                    IsValid = false, 
                    ValidationErrors = $"Error: {ex.Message}" 
                };
            }
        }

        private bool ValidateServerCertificate(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            // We want to return info even if it's invalid, so we return true here 
            // but we could capture errors to report them.
            // Real validation logic would happen here for strict security.
            return true;
        }
    }
}
