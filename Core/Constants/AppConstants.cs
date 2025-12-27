namespace TestMauiApp.Core.Constants;

public static class AppConstants
{
    public const string AppName = "RayTools";
    public const string Version = "1.0.0";
    
    public static class Routes
    {
        public const string Login = "/";
        public const string Dashboard = "/dashboard";
        public const string PortScanner = "/tools/port-scanner";
        public const string IpLookup = "/tools/ip-lookup";
        public const string PingTool = "/tools/ping";
        public const string DnsLookup = "/tools/dns-lookup";
        public const string MyPublicIp = "/tools/my-public-ip";
    }
    
    public static class StorageKeys
    {
        public const string AuthToken = "auth_token";
        public const string Username = "username";
    }
}
