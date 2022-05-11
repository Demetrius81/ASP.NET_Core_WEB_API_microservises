using System;

namespace MetricsAgent.Models.Interfaces
{
    public interface IMetricCreateRequest
    {
        TimeSpan Time { get; set; }
        int Value { get; set; }
    }
}