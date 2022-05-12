using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;
using MetricsAgent.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Services
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    { 
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CpuMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(CpuMetric item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;
            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);
            // Запрос на добавление данных с плейсхолдерами для параметров
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
            // Анонимный объект с параметрами запроса
            new
            {
                // Value подставится на место "@value" в строке запроса
                // Значение запишется из поля Value объекта item
                value = item.Value,
                // Записываем в поле time количество секунд
                time = item.Time
            });
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
