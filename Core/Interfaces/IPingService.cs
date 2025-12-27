using TestMauiApp.Core.Models;

namespace TestMauiApp.Core.Interfaces;

public interface IPingService
{
    Task<PingResult> PingAsync(string hostNameOrAddress, int count = 4);
}
