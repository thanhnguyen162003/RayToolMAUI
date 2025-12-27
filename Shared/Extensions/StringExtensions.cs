using System.Net;
using System.Text.RegularExpressions;

namespace TestMauiApp.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsValidIpAddress(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return IPAddress.TryParse(input, out _);
    }

    public static bool IsValidDomainName(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // Basic domain name validation
        var domainPattern = @"^([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}$";
        return Regex.IsMatch(input, domainPattern);
    }

    public static bool IsValidHostName(this string input)
    {
        return IsValidIpAddress(input) || IsValidDomainName(input);
    }

    public static bool IsValidPortNumber(this string input)
    {
        if (int.TryParse(input, out int port))
        {
            return port >= 1 && port <= 65535;
        }
        return false;
    }
}
