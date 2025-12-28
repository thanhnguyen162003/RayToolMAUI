using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using TestMauiApp.Application.Extensions;
using Core.Interfaces;
using TestMauiApp.Infrastructure.Configuration;
using Infrastructure.Authentication;
using TestMauiApp.Infrastructure.Authentication;

namespace TestMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("TestMauiApp.appsettings.json");
            
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream!)
                .Build();

            builder.Configuration.AddConfiguration(config);

            // Register configuration with Options pattern
            builder.Services.Configure<AppSettings>(config);

            // Add Blazor WebView
            builder.Services.AddMauiBlazorWebView();

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Add HttpClient for external APIs
            builder.Services.AddHttpClient<IIpLookupService, Application.Services.IpLookupService>();

            // Register application services
            builder.Services.AddApplicationServices();

            // Register authentication services
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.AddSingleton<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddAuthorizationCore();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
