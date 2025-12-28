using Core.Interfaces;
using Core.Models;
using System.Net.Http.Json;

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
            // Using ip-api.com free API (http only for free tier)
            // fields=66846719 request all relevant fields 
            //(status,message,continent,continentCode,country,countryCode,region,regionName,city,district,zip,lat,lon,timezone,offset,currency,isp,org,as,asname,mobile,proxy,hosting,query)
            var response = await _httpClient.GetFromJsonAsync<IpApiResponse>($"http://ip-api.com/json/{ipAddress}?fields=66846719");
            
            if (response != null && response.Status == "success")
            {
                result.Success = true;
                result.City = response.City ?? string.Empty;
                result.Region = response.RegionName ?? string.Empty;
                result.Country = response.Country ?? string.Empty;
                result.CountryCode = response.CountryCode ?? string.Empty;
                result.Isp = response.Isp ?? string.Empty;
                result.Organization = response.Org ?? string.Empty;
                result.Timezone = response.Timezone ?? string.Empty;
                result.Latitude = response.Lat;
                result.Longitude = response.Lon;
                
                result.Zip = response.Zip ?? string.Empty;
                result.Continent = response.Continent ?? string.Empty;
                result.ContinentCode = response.ContinentCode ?? string.Empty;
                result.As = response.As ?? string.Empty;
                result.AsName = response.AsName ?? string.Empty;
                result.IsMobile = response.Mobile;
                result.IsProxy = response.Proxy;
                result.IsHosting = response.Hosting;
            }
            else
            {
                result.Success = false;
                result.ErrorMessage = response?.Message ?? "No data returned from API";
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
            var response = await _httpClient.GetFromJsonAsync<IpApiResponse>("http://ip-api.com/json/?fields=query");
            return response?.Query ?? "Unable to determine";
        }
        catch
        {
            return "Unable to determine";
        }
    }

    private class IpApiResponse
    {
        public string? Query { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string? Continent { get; set; }
        public string? ContinentCode { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }
        public string? Region { get; set; }
        public string? RegionName { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string? Timezone { get; set; }
        public string? Isp { get; set; }
        public string? Org { get; set; }
        public string? As { get; set; }
        public string? AsName { get; set; }
        public bool Mobile { get; set; }
        public bool Proxy { get; set; }
        public bool Hosting { get; set; }
    }
}
