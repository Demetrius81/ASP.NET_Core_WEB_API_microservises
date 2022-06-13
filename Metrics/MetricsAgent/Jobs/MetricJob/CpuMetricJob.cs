using Source.Models;
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

            TimeSpan timeFrom = TimeSpan.FromSeconds(0);

            TimeSpan timeTo = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 600);
            try
            {
                _metricRepository.DeleteByTimePeriod(timeFrom, timeTo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            _metricRepository.Create(new CpuMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)cpuUsageInPercent
            });

            return Task.CompletedTask;
        }
    }
}
