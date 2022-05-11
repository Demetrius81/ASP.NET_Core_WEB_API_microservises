using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class DotNetAllMetricsResponse : IAllMetricsResponse
    {
        public List<IMetric> Metrics { get; set; }
    }
}
