using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;

namespace MetricsAgent.Services.Interfaces
{
    public interface INetworkMetricsRepository : IRepository<IMetric>
    {
    }
}
