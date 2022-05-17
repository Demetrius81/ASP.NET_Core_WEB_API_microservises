using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _metricRepository;

        private PerformanceCounter _performanceCounter;

        public DotNetMetricJob(IDotNetMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;

            _performanceCounter = new PerformanceCounter(".NET CLR Exceptions", "# Exceps Thrown", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float dotNet = _performanceCounter.NextValue();

            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _metricRepository.Create(new DotNetMetric
            {
                Time = time.TotalSeconds,
                Value = (int)dotNet
            });

            return Task.CompletedTask;
        }
    }
}
