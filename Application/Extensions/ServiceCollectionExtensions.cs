using Microsoft.Extensions.DependencyInjection;
using TestMauiApp.Core.Interfaces;

namespace TestMauiApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register IP tool services
        services.AddScoped<IPortScannerService, Services.PortScannerService>();
        services.AddScoped<IIpLookupService, Services.IpLookupService>();
        services.AddScoped<IPingService, Services.PingService>();
        services.AddScoped<IDnsLookupService, Services.DnsLookupService>();

        return services;
    }
}
