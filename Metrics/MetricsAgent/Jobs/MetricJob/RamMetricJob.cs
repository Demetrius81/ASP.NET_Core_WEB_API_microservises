using MetricsAgent.Services.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _metricRepository;

        public RamMetricJob(IRamMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {


            return Task.CompletedTask;
        }
    }
}
