using Microsoft.Extensions.Options;
using TestMauiApp.Core.Interfaces;
using TestMauiApp.Core.Constants;
using TestMauiApp.Infrastructure.Configuration;

namespace TestMauiApp.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppSettings _appSettings;
    private bool _isAuthenticated;
    private string? _currentUser;

    public AuthenticationService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public Task<bool> LoginAsync(string username, string password)
    {
        var isValid = username == _appSettings.Authentication.Username &&
                     password == _appSettings.Authentication.Password;

        if (isValid)
        {
            _isAuthenticated = true;
            _currentUser = username;
            
            // In a real app, you might store tokens in secure storage
            // For now, we'll just keep it in memory
        }
        else
        {
            _isAuthenticated = false;
            _currentUser = null;
        }

        return Task.FromResult(isValid);
    }

    public Task LogoutAsync()
    {
        _isAuthenticated = false;
        _currentUser = null;
        return Task.CompletedTask;
    }

    public Task<bool> IsAuthenticatedAsync()
    {
        return Task.FromResult(_isAuthenticated);
    }

    public Task<string?> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }
}
