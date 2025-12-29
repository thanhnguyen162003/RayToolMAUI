using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestMauiApp.Application.Services;

namespace TestMauiApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPortScannerService, Services.PortScannerService>();
        services.AddScoped<IIpLookupService, Services.IpLookupService>();
        services.AddScoped<IPingService, Services.PingService>();
        services.AddScoped<IDnsLookupService, Services.DnsLookupService>();
        services.AddScoped<ILatencyService, Application.Services.CloudLatencyService>();
        services.AddScoped<ISslService, Application.Services.SslService>();
        services.AddScoped<IHeaderAnalyzerService, Application.Services.HeaderAnalyzerService>();
        
        // Phase 2: Enhanced Networking
        services.AddScoped<ISubnetCalculator, Application.Services.SubnetCalculatorService>();
        services.AddScoped<ITraceRouteService, Application.Services.TraceRouteService>();
        services.AddScoped<IWhoisService, Application.Services.WhoisService>();
        services.AddScoped<IMacAddressService, Application.Services.MacAddressService>();

        // Developer Utilities
        services.AddScoped<IJwtService, Application.Services.JwtService>();
        services.AddScoped<IFormatterService, Application.Services.FormatterService>();
        services.AddScoped<IBase64Service, Application.Services.Base64Service>();

        return services;
    }
}
