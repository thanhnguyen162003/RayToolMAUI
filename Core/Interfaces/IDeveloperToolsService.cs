using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IJwtService
    {
        JwtTokenInfo Decode(string token);
    }

    public interface IFormatterService
    {
        string FormatJson(string input);
        string FormatXml(string input);
        bool IsValidJson(string input);
        bool IsValidXml(string input);
    }

    public interface IBase64Service
    {
        string Encode(string input);
        string Decode(string input);
    }
}
