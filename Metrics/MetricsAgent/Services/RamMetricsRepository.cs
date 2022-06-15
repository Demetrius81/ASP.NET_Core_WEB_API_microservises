using Dapper;
using Source.Models;
using MetricsAgent.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using MetricsAgent.Models;

namespace MetricsAgent.Services
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public RamMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            //DataBaseModel.RamMetricDtoDB = new List<RamMetricDto>();

            _databaseOptions = databaseOptions;

            using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM rammetrics");
        }

        public void Create(RamMetricDto item)
        {
            //DataBaseModel.RamMetricDtoDB.Add(item);

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
            //DataBaseModel.RamMetricDtoDB.Remove(DataBaseModel.RamMetricDtoDB.FirstOrDefault(x => x.Id == id));

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
            //DataBaseModel.RamMetricDtoDB.Remove(DataBaseModel.RamMetricDtoDB.FirstOrDefault(x => x == item));

            //DataBaseModel.RamMetricDtoDB.Add(item);

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
            //return DataBaseModel.RamMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds).ToList();

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
            //return DataBaseModel.RamMetricDtoDB.ToList();

            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<RamMetricDto> metrics = connection.Query<RamMetricDto>(
                "SELECT * FROM rammetrics").ToList();

            return metrics;
        }

        public RamMetricDto GetById(int id)
        {
            //return DataBaseModel.RamMetricDtoDB.Where(x => x.Id == id).FirstOrDefault();

            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            RamMetricDto metric = connection.QuerySingle<RamMetricDto>(
                "SELECT Id, Time, Value FROM rammetrics WHERE id = @id",
                new
                {
                    id = id
                });

            return metric;
        }

        public void DeleteByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            //IEnumerable<RamMetricDto> todelete = DataBaseModel.RamMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds);

            //if (todelete is not null)
            //{
            //    foreach (var item in todelete)
            //    {
            //        DataBaseModel.RamMetricDtoDB.Remove(item);
            //    }
            //}


            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute("DELETE FROM rammetrics WHERE time BETWEEN @timeFrom AND @timeTo",
                new
                {
                    timeFrom = fromTime.TotalSeconds,
                    timeTo = toTime.TotalSeconds
                });
        }
    }
}
