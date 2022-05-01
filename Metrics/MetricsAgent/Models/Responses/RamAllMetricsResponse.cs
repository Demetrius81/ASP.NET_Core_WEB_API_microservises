using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class RamAllMetricsResponse : IAllMetricsResponse
    {
        public List<IMetric> Metrics { get; set; }
    }
}
