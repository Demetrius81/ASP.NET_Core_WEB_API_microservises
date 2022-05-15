using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _metricRepository;

        private PerformanceCounter _performanceCounter;

        public NetworkMetricJob(INetworkMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;

            _performanceCounter = new PerformanceCounter("Network Segment", "% Network Utilization");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float networkUtilization = _performanceCounter.NextValue();

            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _metricRepository.Create(new NetworkMetric
            {
                Time = time.TotalSeconds,
                Value = (int)networkUtilization
            });

            return Task.CompletedTask;
        }
    }
}
