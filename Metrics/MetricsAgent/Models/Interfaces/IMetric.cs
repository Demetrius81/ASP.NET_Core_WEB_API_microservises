using System;

namespace MetricsAgent.Models.Interfaces
{
    public interface IMetric
    {
        int Id { get; set; }
        TimeSpan Time { get; set; }
        int Value { get; set; }
    }
}