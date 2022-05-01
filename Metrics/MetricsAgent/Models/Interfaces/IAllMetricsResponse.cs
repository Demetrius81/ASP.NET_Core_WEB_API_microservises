using System.Collections.Generic;

namespace MetricsAgent.Models.Interfaces
{
    public interface IAllMetricsResponse
    {
        List<IMetric> Metrics { get; set; }
    }
}