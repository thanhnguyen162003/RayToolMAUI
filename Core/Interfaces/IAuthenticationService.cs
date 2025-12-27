namespace TestMauiApp.Core.Interfaces;

public interface IAuthenticationService
{
    Task<bool> LoginAsync(string username, string password);
    Task LogoutAsync();
    Task<bool> IsAuthenticatedAsync();
    Task<string?> GetCurrentUserAsync();
}
