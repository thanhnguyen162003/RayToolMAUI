using Core.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TestMauiApp.Infrastructure.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IAuthenticationService _authService;

    public AuthStateProvider(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var isAuthenticated = await _authService.IsAuthenticatedAsync();
        
        if (isAuthenticated)
        {
            var username = await _authService.GetCurrentUserAsync();
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username ?? "User")
            }, "custom");
            
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
        }
        
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
