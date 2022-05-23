using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _metricRepository;

        private PerformanceCounter _performanceCounter;

        public HddMetricJob(IHddMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;

            _performanceCounter = new PerformanceCounter("LogicalDisk", "% Disk Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float hddSpeed = _performanceCounter.NextValue();

            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _metricRepository.Create(new HddMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)hddSpeed
            });

            return Task.CompletedTask;
        }
    }
}
