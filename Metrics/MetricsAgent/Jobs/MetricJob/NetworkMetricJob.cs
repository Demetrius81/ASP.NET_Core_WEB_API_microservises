using MetricsAgent.Services.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _metricRepository;

        public NetworkMetricJob(INetworkMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {


            return Task.CompletedTask;
        }
    }
}
