using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Core.Interfaces;
using Core.Models;

namespace TestMauiApp.Application.Services
{
    public class JwtService : IJwtService
    {
        public JwtTokenInfo Decode(string token)
        {
            var info = new JwtTokenInfo();

            if (string.IsNullOrWhiteSpace(token))
            {
                info.Error = "Token is empty";
                return info;
            }

            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                info.Error = "Invalid JWT token format. Expected 3 parts (Header.Payload.Signature).";
                return info;
            }

            try
            {
                // Decode Header
                var headerJson = DecodeBase64Url(parts[0]);
                info.Header = FormatJson(headerJson);

                // Decode Payload
                var payloadJson = DecodeBase64Url(parts[1]);
                info.Payload = FormatJson(payloadJson);

                // Signature (Just raw)
                info.Signature = parts[2];

                // Parse Payload Claims
                var claims = JsonSerializer.Deserialize<Dictionary<string, object>>(payloadJson);
                if (claims != null)
                {
                    foreach (var claim in claims)
                    {
                        info.Claims.Add(claim.Key, claim.Value?.ToString() ?? "null");
                    }
                }

                info.IsValid = true;
            }
            catch (Exception ex)
            {
                info.IsValid = false;
                info.Error = $"Decoding error: {ex.Message}";
            }

            return info;
        }

        private string DecodeBase64Url(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return Encoding.UTF8.GetString(converted);
        }

        private string FormatJson(string json)
        {
            try
            {
                using var jDoc = JsonDocument.Parse(json);
                return JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return json;
            }
        }
    }
}
