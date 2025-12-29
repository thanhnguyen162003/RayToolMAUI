using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ILatencyService
    {
        Task<List<CloudRegion>> GetRegionsAsync();
        IAsyncEnumerable<LatencyResult> MeasureLatencyAsync(List<CloudRegion> regions);
    }

    public interface ISslService
    {
        Task<SslCertificateInfo> GetCertificateInfoAsync(string domain);
    }

    public interface IHeaderAnalyzerService
    {
        Task<List<HeaderAnalysisResult>> AnalyzeHeadersAsync(string url);
    }
}
