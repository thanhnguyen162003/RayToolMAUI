namespace Core.Constants;

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
        
        // Phase 2
        public const string SubnetCalculator = "/tools/subnet-calculator";
        public const string TraceRoute = "/tools/trace-route";
        public const string WhoisLookup = "/tools/whois-lookup";
        public const string MacLookup = "/tools/mac-lookup";

        // Developer Utilities
        public const string JwtDecoder = "/tools/jwt-decoder";
        public const string JsonFormatter = "/tools/json-formatter";
        public const string Base64Converter = "/tools/base64-converter";
        public const string CloudLatency = "/tools/cloud-latency";
        public const string SslAuditor = "/tools/ssl-auditor";
        public const string HeaderAnalyzer = "/tools/header-analyzer";
    }
    
    public static class StorageKeys
    {
        public const string AuthToken = "auth_token";
        public const string Username = "username";
    }
}
