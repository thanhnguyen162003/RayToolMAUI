using Core.Models;

namespace Core.Interfaces;

public interface IPingService
{
    Task<PingResult> PingAsync(string hostNameOrAddress, int count = 4);
}
