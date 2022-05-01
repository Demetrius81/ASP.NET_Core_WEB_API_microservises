using MetricsAgent.Models.Interfaces;
using System;

namespace MetricsAgent.Models.Requests
{
    public class CpuMetricCreateRequest : IMetricCreateRequest
    {
        public TimeSpan Time { get; set; }

        public int Value { get; set; }
    }
}
