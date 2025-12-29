using System;
using System.Text.Json;
using System.Xml.Linq;
using Core.Interfaces;

namespace TestMauiApp.Application.Services
{
    public class FormatterService : IFormatterService
    {
        public string FormatJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            try
            {
                using var jDoc = JsonDocument.Parse(input);
                return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception ex)
            {
                return $"Invalid JSON: {ex.Message}";
            }
        }

        public string FormatXml(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            try
            {
                var xDoc = XDocument.Parse(input);
                return xDoc.ToString();
            }
            catch (Exception ex)
            {
                return $"Invalid XML: {ex.Message}";
            }
        }

        public bool IsValidJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            try
            {
                JsonDocument.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidXml(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            try
            {
                XDocument.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
