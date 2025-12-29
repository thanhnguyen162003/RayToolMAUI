using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class HeaderAnalyzerService : IHeaderAnalyzerService
    {
        private readonly HttpClient _httpClient;

        public HeaderAnalyzerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HeaderAnalysisResult>> AnalyzeHeadersAsync(string url)
        {
            var results = new List<HeaderAnalysisResult>();

            if (!url.StartsWith("http"))
            {
                url = "https://" + url;
            }

            try
            {
                var response = await _httpClient.GetAsync(url);
                var headers = response.Headers;

                // Check for HSTS
                if (headers.Contains("Strict-Transport-Security"))
                {
                    results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "Strict-Transport-Security",
                        HeaderValue = headers.GetValues("Strict-Transport-Security").First(),
                        IsSecure = true,
                        Severity = "Info",
                        Description = "HSTS enforces HTTPS connections.",
                        Recommendation = "Keep it enabled."
                    });
                }
                else
                {
                     results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "Strict-Transport-Security",
                        HeaderValue = "Missing",
                        IsSecure = false,
                        Severity = "High",
                        Description = "Missing HSTS.",
                        Recommendation = "Enable HSTS to prevent man-in-the-middle attacks."
                    });
                }

                // Check for CSP
                if (headers.Contains("Content-Security-Policy"))
                {
                    results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "Content-Security-Policy",
                        HeaderValue = string.Join("; ", headers.GetValues("Content-Security-Policy")),
                        IsSecure = true,
                        Severity = "Info",
                        Description = "CSP helps prevent XSS attacks.",
                        Recommendation = "Review policy for strictness."
                    });
                }
                else
                {
                    results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "Content-Security-Policy",
                        HeaderValue = "Missing",
                        IsSecure = false,
                        Severity = "Medium",
                        Description = "Missing CSP.",
                        Recommendation = "Implement CSP to mitigate XSS and data injection attacks."
                    });
                }

                // Check for X-Frame-Options
                 if (headers.Contains("X-Frame-Options"))
                {
                    results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "X-Frame-Options",
                        HeaderValue = headers.GetValues("X-Frame-Options").First(),
                        IsSecure = true,
                        Severity = "Info",
                        Description = "Prevents clickjacking.",
                        Recommendation = "Good."
                    });
                }
                else
                {
                     results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "X-Frame-Options",
                        HeaderValue = "Missing",
                        IsSecure = false,
                        Severity = "Medium",
                        Description = "Missing X-Frame-Options.",
                        Recommendation = "Set to DENY or SAMEORIGIN to prevent clickjacking."
                    });
                }

                // Check for Server (Information Leakage)
                if (headers.Contains("Server"))
                {
                     results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "Server",
                        HeaderValue = headers.GetValues("Server").First(),
                        IsSecure = false,
                        Severity = "Low",
                        Description = "Server banner leaks version info.",
                        Recommendation = "Hide or obfuscate server version info."
                    });
                }

            }
            catch (Exception ex)
            {
                 results.Add(new HeaderAnalysisResult
                    {
                        HeaderName = "Error",
                        HeaderValue = ex.Message,
                        IsSecure = false,
                        Severity = "High",
                        Description = "Failed to fetch headers.",
                        Recommendation = "Check URL and connectivity."
                    });
            }

            return results;
        }
    }
}
