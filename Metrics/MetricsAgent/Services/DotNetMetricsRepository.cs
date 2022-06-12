using Dapper;
using MetricsAgent.Services.Interfaces;
using Microsoft.Extensions.Options;
using Source.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.Services
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public DotNetMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;

            using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM dotnetmetrics");
        }

        public void Create(DotNetMetricDto item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;

            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            connection.Execute(
                "INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
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
                "DELETE FROM dotnetmetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(DotNetMetricDto item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<DotNetMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<DotNetMetricDto> metrics = connection.Query<DotNetMetricDto>(
                "SELECT * FROM dotnetmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                }).ToList();

            return metrics;
        }

        public IList<DotNetMetricDto> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<DotNetMetricDto> metrics = connection.Query<DotNetMetricDto>(
                "SELECT * FROM dotnetmetrics").ToList();

            return metrics;
        }

        public DotNetMetricDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            DotNetMetricDto metric = connection.QuerySingle<DotNetMetricDto>(
                "SELECT Id, Time, Value FROM dotnetmetrics WHERE id = @id",
                new
                {
                    id = id
                });

            return metric;
        }

        public void DeleteByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM dotnetmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                });
        }
    }
}
