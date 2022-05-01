using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;
using MetricsAgent.Services.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Services
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string _tabName = "dotnetmetrics";

        private MetricsRepoOperations _operation;

        public DotNetMetricsRepository()
        {
            _operation = new MetricsRepoOperations(_tabName);
        }

        public void Create(IMetric item)
        {
            _operation.CreateOperation(item);
        }

        public void Delete(int id)
        {
            _operation.DeleteOperation(id);
        }

        public void Update(IMetric item)
        {
            _operation.UpdateOperation(item);
        }

        public IList<IMetric> GetByTimePeriod()
        {
            IMetric metric = new DotNetMetric();

            return _operation.GetAllOperation(metric);
        }

        public IMetric GetById(int id)
        {
            IMetric metric = new DotNetMetric();

            return _operation.GetByIdOperation(id, metric);
        }
    }
}
