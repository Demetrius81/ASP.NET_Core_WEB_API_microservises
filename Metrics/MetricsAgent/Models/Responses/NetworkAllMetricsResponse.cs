using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class NetworkAllMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
