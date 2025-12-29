using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class JwtTokenInfo
    {
        public string Header { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public string Signature { get; set; } = string.Empty;
        public Dictionary<string, string> Claims { get; set; } = new();
        public bool IsValid { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
