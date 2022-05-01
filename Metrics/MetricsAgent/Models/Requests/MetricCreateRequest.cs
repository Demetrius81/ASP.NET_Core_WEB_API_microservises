using System;

namespace MetricsAgent.Models.Requests
{
    public class MetricCreateRequest
    {
        public TimeSpan Time { get; set; }

        public int Value { get; set; }
    }
}
