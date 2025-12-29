namespace TestMauiApp.Shared.Helpers;

public static class ValidationHelper
{
    public static (bool IsValid, string ErrorMessage) ValidatePortRange(int startPort, int endPort)
    {
        if (startPort < 1 || startPort > 65535)
            return (false, "Start port must be between 1 and 65535");

        if (endPort < 1 || endPort > 65535)
            return (false, "End port must be between 1 and 65535");

        if (startPort > endPort)
            return (false, "Start port must be less than or equal to end port");

        if (endPort - startPort > 65535)
            return (false, "Port range too large (max 65535 ports)");

        return (true, string.Empty);
    }

    public static (bool IsValid, string ErrorMessage) ValidatePingCount(int count)
    {
        if (count < 1 || count > 100)
            return (false, "Ping count must be between 1 and 100");

        return (true, string.Empty);
    }
}
