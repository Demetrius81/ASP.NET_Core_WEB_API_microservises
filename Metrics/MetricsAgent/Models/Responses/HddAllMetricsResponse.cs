using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class HddAllMetricsResponse : IAllMetricsResponse
    {
        public List<IMetric> Metrics { get; set; }
    }
}
