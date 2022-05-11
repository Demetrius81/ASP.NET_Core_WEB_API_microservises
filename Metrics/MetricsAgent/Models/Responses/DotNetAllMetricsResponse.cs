using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class DotNetAllMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
