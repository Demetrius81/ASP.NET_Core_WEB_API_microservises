using MetricsAgent.Models.Interfaces;
using System;

namespace MetricsAgent.Models
{
    public class RamMetric : IMetric
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Value} - {Time}";
        }
    }
}
