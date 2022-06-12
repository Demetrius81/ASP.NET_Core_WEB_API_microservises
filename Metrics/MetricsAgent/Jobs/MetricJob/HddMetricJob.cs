using Source.Models;
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

            TimeSpan timeFrom = TimeSpan.FromSeconds(0);

            TimeSpan timeTo = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 120);

            try
            {
                _metricRepository.DeleteByTimePeriod(timeFrom, timeTo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            _metricRepository.Create(new HddMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)hddSpeed
            });

            return Task.CompletedTask;
        }
    }
}
