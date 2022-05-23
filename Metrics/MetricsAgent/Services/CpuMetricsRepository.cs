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
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CpuMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Create(CpuMetricDto item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;

            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            connection.Execute(
                "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
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
                "DELETE FROM cpumetrics WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(CpuMetricDto item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    id = item.Id
                });
        }

        public IList<CpuMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<CpuMetricDto> metrics = connection.Query<CpuMetricDto>(
                "SELECT * FROM cpumetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                }).ToList();

            return metrics;
        }

        public IList<CpuMetricDto> GetAll()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<CpuMetricDto> metrics = connection.Query<CpuMetricDto>(
                "SELECT * FROM cpumetrics").ToList();

            return metrics;
        }

        public CpuMetricDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            CpuMetricDto metric = connection.QuerySingle<CpuMetricDto>(
                "SELECT Id, Time, Value FROM cpumetrics WHERE id = @id",
                new
                {
                    id = id
                });

            return metric;
        }
    }
}
