using System.Net.Http.Json;
using TestMauiApp.Core.Interfaces;
using TestMauiApp.Core.Models;

namespace TestMauiApp.Application.Services;

public class IpLookupService : IIpLookupService
{
    private readonly HttpClient _httpClient;

    public IpLookupService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IpLookupResult> LookupIpAsync(string ipAddress)
    {
        var result = new IpLookupResult { IpAddress = ipAddress };

        try
        {
            // Using ipapi.co free API
            var response = await _httpClient.GetFromJsonAsync<IpApiResponse>($"https://ipapi.co/{ipAddress}/json/");
            
            if (response != null)
            {
                result.Success = true;
                result.City = response.City ?? string.Empty;
                result.Region = response.Region ?? string.Empty;
                result.Country = response.CountryName ?? string.Empty;
                result.CountryCode = response.CountryCode ?? string.Empty;
                result.Isp = response.Org ?? string.Empty;
                result.Organization = response.Org ?? string.Empty;
                result.Timezone = response.Timezone ?? string.Empty;
                result.Latitude = response.Latitude;
                result.Longitude = response.Longitude;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = "No data returned from API";
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
    }

    public async Task<string> GetMyPublicIpAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IpApiResponse>("https://ipapi.co/json/");
            return response?.Ip ?? "Unable to determine";
        }
        catch
        {
            return "Unable to determine";
        }
    }

    // DTO for ipapi.co response
    private class IpApiResponse
    {
        public string? Ip { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public string? Org { get; set; }
        public string? Timezone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
