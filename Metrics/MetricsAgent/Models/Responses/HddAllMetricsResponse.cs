using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class HddAllMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
