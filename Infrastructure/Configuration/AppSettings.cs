using Core.Models;

namespace TestMauiApp.Infrastructure.Configuration;

public class AppSettings
{
    public LoginCredentials Authentication { get; set; } = new();
    public AppInfo AppInfo { get; set; } = new();
    public ExternalApis ExternalApis { get; set; } = new();
}

public class AppInfo
{
    public string ApplicationName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}

public class ExternalApis
{
    public string IpApiUrl { get; set; } = string.Empty;
    public string IpInfoUrl { get; set; } = string.Empty;
}
