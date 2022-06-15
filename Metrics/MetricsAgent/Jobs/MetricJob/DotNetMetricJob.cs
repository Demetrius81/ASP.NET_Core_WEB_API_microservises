using Source.Models;
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

            _performanceCounter = new PerformanceCounter(".NET CLR Exceptions", "# of Exceps Thrown", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float dotNet = (_performanceCounter.NextValue())/1024;

            TimeSpan time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            TimeSpan timeFrom = TimeSpan.FromSeconds(0);

            TimeSpan timeTo = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 1200);

            try
            {
                _metricRepository.DeleteByTimePeriod(timeFrom, timeTo);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            _metricRepository.Create(new DotNetMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)dotNet
            });

            return Task.CompletedTask;
        }
    }
}
