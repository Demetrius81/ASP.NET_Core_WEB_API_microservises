using System.Collections.Generic;

namespace MetricsAgent.Models.Requests
{
    public class AllMetricsResponse
    {
        public List<MetricDto> Metrics { get; set; }
    }
}
