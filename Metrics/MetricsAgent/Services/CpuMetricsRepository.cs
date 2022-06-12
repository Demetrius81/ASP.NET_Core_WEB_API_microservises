using Dapper;
using MetricsAgent.Models;
using MetricsAgent.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
            DataBaseModel.CpuMetricDtoDB = new List<CpuMetricDto>();

            _databaseOptions = databaseOptions;

            //using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            //connection.Execute("DELETE FROM cpumetrics");
        }

        public void Create(CpuMetricDto item)
        {
            DataBaseModel.CpuMetricDtoDB.Add(item);

            //DatabaseOptions databaseOptions = _databaseOptions.Value;

            //using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            //connection.Execute(
            //    "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
            //    new
            //    {
            //        value = item.Value,
            //        time = item.Time
            //    });
        }

        public void Delete(int id)
        {
            DataBaseModel.CpuMetricDtoDB.Remove(DataBaseModel.CpuMetricDtoDB.FirstOrDefault(x => x.Id == id));

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute(
            //    "DELETE FROM cpumetrics WHERE id=@id",
            //    new
            //    {
            //        id = id
            //    });
        }

        public void Update(CpuMetricDto item)
        {
            DataBaseModel.CpuMetricDtoDB.Remove(DataBaseModel.CpuMetricDtoDB.FirstOrDefault(x => x == item));

            DataBaseModel.CpuMetricDtoDB.Add(item);

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute(
            //    "UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id",
            //    new
            //    {
            //        value = item.Value,
            //        time = item.Time,
            //        id = item.Id
            //    });
        }

        public void DeleteByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            IEnumerable<CpuMetricDto> todelete = DataBaseModel.CpuMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds);

            if (todelete is not null)
            {
                foreach (var item in todelete)
                {
                    DataBaseModel.CpuMetricDtoDB.Remove(item);
                }
            }


            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute("DELETE FROM cpumetrics WHERE time BETWEEN @timeFrom AND @timeTo",
            //    new
            //    {
            //        timeFrom = fromTime.TotalSeconds,
            //        timeTo = toTime.TotalSeconds
            //    });
        }

        public IList<CpuMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            return DataBaseModel.CpuMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds).ToList();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //List<CpuMetricDto> metrics = connection.Query<CpuMetricDto>(
            //    "SELECT * FROM cpumetrics WHERE time BETWEEN @timeFrom AND @timeTo",
            //    new
            //    {
            //        timeFrom = fromTime.TotalSeconds,
            //        timeTo = toTime.TotalSeconds
            //    }).ToList();

            //return metrics;
        }

        public IList<CpuMetricDto> GetAll()
        {
            return DataBaseModel.CpuMetricDtoDB.ToList();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //List<CpuMetricDto> metrics = connection.Query<CpuMetricDto>(
            //    "SELECT * FROM cpumetrics").ToList();

            //return metrics;
        }

        public CpuMetricDto GetById(int id)
        {
            return DataBaseModel.CpuMetricDtoDB.Where(x => x.Id == id).FirstOrDefault();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //CpuMetricDto metric = connection.QuerySingle<CpuMetricDto>(
            //    "SELECT Id, Time, Value FROM cpumetrics WHERE id = @id",
            //    new
            //    {
            //        id = id
            //    });

            //return metric;
        }
    }
}
