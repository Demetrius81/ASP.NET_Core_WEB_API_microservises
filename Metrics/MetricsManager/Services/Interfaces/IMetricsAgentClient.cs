using Source.Models.Requests;
using Source.Models.Responses;

namespace MetricsManager.Services.Interfaces
{
    public interface IMetricsAgentClient
    {
        CpuAllMetricsResponse GetCpuAllMetrics(CpuMetricCreateRequest request);

        DotNetAllMetricsResponse GetDotNetAllMetrics(DotNetMetricCreateRequest request);

        HddAllMetricsResponse GetHddAllMetrics(HddMetricCreateRequest request);

        NetworkAllMetricsResponse GetNetworkAllMetrics(NetworkMetricCreateRequest request);

        RamAllMetricsResponse GetRamAllMetrics(RamMetricCreateRequest request);
    }
}
