using MetricsAgent.Services.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _metricRepository;

        public HddMetricJob(IHddMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {


            return Task.CompletedTask;
        }
    }
}
