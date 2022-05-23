using Dapper;
using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.Services
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public HddMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(HddMetricDto item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;

            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            connection.Execute(
                "INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "DELETE FROM hddmetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(HddMetricDto item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<HddMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<HddMetricDto> metrics = connection.Query<HddMetricDto>(
                "SELECT * FROM hddmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                }).ToList();

            return metrics;
        }

        public IList<HddMetricDto> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<HddMetricDto> metrics = connection.Query<HddMetricDto>(
                "SELECT * FROM hddmetrics").ToList();

            return metrics;
        }

        public HddMetricDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            HddMetricDto metric = connection.QuerySingle<HddMetricDto>(
                "SELECT Id, Time, Value FROM hddmetrics WHERE id = @id",
                new
                {
                    id = id
                });

            return metric;
        }
    }
}
