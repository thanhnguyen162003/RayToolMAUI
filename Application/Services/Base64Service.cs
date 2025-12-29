using System;
using System.Text;
using Core.Interfaces;

namespace TestMauiApp.Application.Services
{
    public class Base64Service : IBase64Service
    {
        public string Encode(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public string Decode(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            try
            {
                byte[] bytes = Convert.FromBase64String(input);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
