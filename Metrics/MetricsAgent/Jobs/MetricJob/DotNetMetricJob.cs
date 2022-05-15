using MetricsAgent.Services.Interfaces;
using Quartz;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _metricRepository;

        public DotNetMetricJob(IDotNetMetricsRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {


            return Task.CompletedTask;
        }
    }
}
