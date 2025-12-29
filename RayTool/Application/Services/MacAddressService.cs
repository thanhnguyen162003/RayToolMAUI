using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class MacAddressService : IMacAddressService
    {
        private readonly HttpClient _httpClient;

        public MacAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MacVendorInfo> LookupVendorAsync(string macAddress)
        {
            var result = new MacVendorInfo { Address = macAddress };

            try
            {
                // Using macvendors.com free API (simple string return)
                var response = await _httpClient.GetStringAsync($"https://api.macvendors.com/{macAddress}");
                
                result.Found = true;
                result.VendorName = response;
                result.MacPrefix = macAddress.Substring(0, Math.Min(macAddress.Length, 8)); // Approx prefix
            }
            catch (HttpRequestException ex)
            {
                result.Found = false;
                result.Error = "Vendor not found or API limit reached.";
            }
            catch (Exception ex)
            {
                result.Found = false;
                result.Error = ex.Message;
            }

            return result;
        }
    }
}
