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
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public RamMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(RamMetricDto item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;

            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            connection.Execute(
                "INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
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
                "DELETE FROM rammetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(RamMetricDto item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "UPDATE rammetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<RamMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<RamMetricDto> metrics = connection.Query<RamMetricDto>(
                "SELECT * FROM rammetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                }).ToList();

            return metrics;
        }

        public IList<RamMetricDto> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<RamMetricDto> metrics = connection.Query<RamMetricDto>(
                "SELECT * FROM rammetrics").ToList();

            return metrics;
        }

        public RamMetricDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            RamMetricDto metric = connection.QuerySingle<RamMetricDto>(
                "SELECT Id, Time, Value FROM rammetrics WHERE id = @id",
                new
                {
                    id = id
                });

            return metric;
        }
    }
}
