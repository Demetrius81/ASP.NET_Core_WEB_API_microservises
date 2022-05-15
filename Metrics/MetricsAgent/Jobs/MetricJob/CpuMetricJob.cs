using MetricsAgent.Services.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _metricRepository;

        public CpuMetricJob(ICpuMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {


            return Task.CompletedTask;
        }
    }
}
