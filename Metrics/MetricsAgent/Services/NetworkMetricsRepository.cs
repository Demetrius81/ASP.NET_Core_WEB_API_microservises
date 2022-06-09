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
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public NetworkMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;

            using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM networkmetrics");
        }

        public void Create(NetworkMetricDto item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;

            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            connection.Execute(
                "INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
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
                "DELETE FROM networkmetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(NetworkMetricDto item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<NetworkMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<NetworkMetricDto> metrics = connection.Query<NetworkMetricDto>(
                "SELECT * FROM networkmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                }).ToList();

            return metrics;
        }

        public IList<NetworkMetricDto> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<NetworkMetricDto> metrics = connection.Query<NetworkMetricDto>(
                "SELECT * FROM networkmetrics").ToList();

            return metrics;
        }

        public NetworkMetricDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            NetworkMetricDto metric = connection.QuerySingle<NetworkMetricDto>(
                "SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
                new
                {
                    id = id
                });

            return metric;
        }
    }
}
