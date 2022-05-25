using MetricsAgent.Models.Interfaces;
using System;

namespace MetricsAgent.Models
{
    public class DotNetMetric : IMetric
    {
        public int Id { get; set; }
        public double Time { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Value} - {Time}";
        }
    }
}
