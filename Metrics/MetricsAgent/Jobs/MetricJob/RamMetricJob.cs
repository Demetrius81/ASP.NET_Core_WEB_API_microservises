using Source.Models;
using MetricsAgent.Services.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _metricRepository;

        private PerformanceCounter _performanceCounter;

        public RamMetricJob(IRamMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;

            _performanceCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float RamAvailableSpace = _performanceCounter.NextValue();

            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _metricRepository.Create(new RamMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)RamAvailableSpace
            });

            return Task.CompletedTask;
        }
    }
}
