using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class CloudLatencyService : ILatencyService
    {
        private readonly HttpClient _httpClient;

        public CloudLatencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<CloudRegion>> GetRegionsAsync()
        {
            var regions = new List<CloudRegion>
            {
                // AWS
                new CloudRegion { Name = "US East (N. Virginia)", Provider = "AWS", Code = "us-east-1", Endpoint = "https://dynamodb.us-east-1.amazonaws.com" },
                new CloudRegion { Name = "US West (Oregon)", Provider = "AWS", Code = "us-west-2", Endpoint = "https://dynamodb.us-west-2.amazonaws.com" },
                new CloudRegion { Name = "EU (London)", Provider = "AWS", Code = "eu-west-2", Endpoint = "https://dynamodb.eu-west-2.amazonaws.com" },
                new CloudRegion { Name = "Asia Pacific (Singapore)", Provider = "AWS", Code = "ap-southeast-1", Endpoint = "https://dynamodb.ap-southeast-1.amazonaws.com" },
                
                // Azure
                new CloudRegion { Name = "East US", Provider = "Azure", Code = "eastus", Endpoint = "https://annotation.eastus.cloudapp.azure.com" }, // Using a generic endpoint or known service
                new CloudRegion { Name = "West Europe", Provider = "Azure", Code = "westeurope", Endpoint = "https://westeuropestorage.blob.core.windows.net" },
                
                // GCP
                new CloudRegion { Name = "us-central1 (Iowa)", Provider = "GCP", Code = "us-central1", Endpoint = "https://storage.googleapis.com" }, // Global, but good for connectivity check
                new CloudRegion { Name = "europe-west1 (Belgium)", Provider = "GCP", Code = "europe-west1", Endpoint = "https://europe-west1-run.googleapis.com" },
            };

            return Task.FromResult(regions);
        }

        public async IAsyncEnumerable<LatencyResult> MeasureLatencyAsync(List<CloudRegion> regions)
        {
            foreach (var region in regions)
            {
                var result = new LatencyResult { Region = region };
                var stopwatch = new Stopwatch();

                try
                {
                    stopwatch.Start();
                    // Just check connectivity, don't need full content
                    var response = await _httpClient.GetAsync(region.Endpoint, HttpCompletionOption.ResponseHeadersRead);
                    stopwatch.Stop();

                    result.LatencyMs = stopwatch.ElapsedMilliseconds;
                    result.IsSuccess = true;
                    result.StatusMessage = "OK";
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    result.IsSuccess = false;
                    result.StatusMessage = ex.Message;
                }

                yield return result;
            }
        }
    }
}
