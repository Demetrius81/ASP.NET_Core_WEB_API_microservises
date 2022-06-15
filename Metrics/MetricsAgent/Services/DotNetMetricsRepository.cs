using Dapper;
using MetricsAgent.Models;
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
            //DataBaseModel.DotNetMetricDtoDB = new List<DotNetMetricDto>();

            _databaseOptions = databaseOptions;

            using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM dotnetmetrics");
        }

        public void Create(DotNetMetricDto item)
        {
            //DataBaseModel.DotNetMetricDtoDB.Add(item);

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
            //DataBaseModel.DotNetMetricDtoDB.Remove(DataBaseModel.DotNetMetricDtoDB.FirstOrDefault(x => x.Id == id));

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
            //DataBaseModel.DotNetMetricDtoDB.Remove(DataBaseModel.DotNetMetricDtoDB.FirstOrDefault(x => x == item));

            //DataBaseModel.DotNetMetricDtoDB.Add(item);

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
            //return DataBaseModel.DotNetMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds).ToList();

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
            //return DataBaseModel.DotNetMetricDtoDB.ToList();

            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<DotNetMetricDto> metrics = connection.Query<DotNetMetricDto>(
                "SELECT * FROM dotnetmetrics").ToList();

            return metrics;
        }

        public DotNetMetricDto GetById(int id)
        {
            //return DataBaseModel.DotNetMetricDtoDB.Where(x => x.Id == id).FirstOrDefault();

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
            //IEnumerable<DotNetMetricDto> todelete = DataBaseModel.DotNetMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds);

            //if (todelete is not null)
            //{
            //    foreach (var item in todelete)
            //    {
            //        DataBaseModel.DotNetMetricDtoDB.Remove(item);
            //    }
            //}


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
