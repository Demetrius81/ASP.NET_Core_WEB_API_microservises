using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _metricRepository;

        private PerformanceCounter _performanceCounter;

        public CpuMetricJob(ICpuMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;

            _performanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float cpuUsageInPercent = _performanceCounter.NextValue();

            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _metricRepository.Create(new CpuMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)cpuUsageInPercent
            });

            return Task.CompletedTask;
        }
    }
}
