using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

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

        public void Create(Metric item)
        {
            _operation.CreateOperation(item);
        }

        public void Delete(int id)
        {
            _operation.DeleteOperation(id);
        }

        public void Update(Metric item)
        {
            _operation.UpdateOperation(item);
        }

        public IList<Metric> GetAll()
        {
            return _operation.GetAllOperation();
        }

        public Metric GetById(int id)
        {
            return _operation.GetByIdOperation(id);
        }
    }
}
