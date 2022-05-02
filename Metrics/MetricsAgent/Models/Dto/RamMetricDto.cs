using MetricsAgent.Models.Interfaces;
using System;

namespace MetricsAgent.Models
{
    public class RamMetricDto : IMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
