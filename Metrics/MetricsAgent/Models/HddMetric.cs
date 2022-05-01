using MetricsAgent.Models.Interfaces;
using System;

namespace MetricsAgent.Models
{
    public class HddMetric : IMetric
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
    }
}
