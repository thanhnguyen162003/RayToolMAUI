using System.Collections.Generic;

namespace Core.Models
{
    public class HeaderAnalysisResult
    {
        public string HeaderName { get; set; } = string.Empty;
        public string HeaderValue { get; set; } = string.Empty;
        public bool IsSecure { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public string Severity { get; set; } = "Info"; // Info, Low, Medium, High, Critical
    }
}
