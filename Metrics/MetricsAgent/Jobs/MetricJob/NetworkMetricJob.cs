using Source.Models;
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

            _performanceCounter = new PerformanceCounter(".NET CLR Networking", "Connections Established", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            float network = _performanceCounter.NextValue();

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

            _metricRepository.Create(new NetworkMetricDto
            {
                Time = time.TotalSeconds,
                Value = (int)network
            });

            return Task.CompletedTask;
        }
    }
}
