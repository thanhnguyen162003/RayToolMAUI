using System;
using System.Security.Cryptography.X509Certificates;

namespace Core.Models
{
    public class SslCertificateInfo
    {
        public string Subject { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public DateTime NotBefore { get; set; }
        public DateTime NotAfter { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string Thumbprint { get; set; } = string.Empty;
        public string Algorithm { get; set; } = string.Empty;
        public bool IsValid { get; set; }
        public string ValidationErrors { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty; // TLS 1.2, 1.3 etc.
        public bool HasPrivateKey { get; set; }
    }
}
