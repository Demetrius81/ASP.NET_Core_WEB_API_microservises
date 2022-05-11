﻿using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;
using MetricsAgent.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Services
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string _tabName = "cpumetrics";

        private MetricsRepoOperations _operation;

        public CpuMetricsRepository()
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

        public IList<IMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            IMetric metric = new CpuMetric();

            return _operation.GetByTimePeriodOperation(fromTime, toTime, metric);
        }

        public IList<IMetric> GetAll()
        {
            IMetric metric = new CpuMetric();

            return _operation.GetAllOperation(metric);
        }

        public IMetric GetById(int id)
        {
            CpuMetric metric = new CpuMetric();

            return _operation.GetByIdOperation(id, metric);
        }
    }
}
