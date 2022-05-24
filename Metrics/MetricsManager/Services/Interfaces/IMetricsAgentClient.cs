using Source.Models.Requests;
using Source.Models.Responses;

namespace MetricsManager.Services.Interfaces
{
    public interface IMetricsAgentClient
    {
        CpuAllMetricsResponse GetCpuAllMetrics(CpuMetricCreateRequest request);
    }
}
