using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class RamAllMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
