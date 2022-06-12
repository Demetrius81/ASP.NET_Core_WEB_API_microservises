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
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public HddMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            DataBaseModel.HddMetricDtoDB = new List<HddMetricDto>();

            _databaseOptions = databaseOptions;

            //using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            //connection.Execute("DELETE FROM hddmetrics");
        }

        public void Create(HddMetricDto item)
        {
            DataBaseModel.HddMetricDtoDB.Add(item);

            //DatabaseOptions databaseOptions = _databaseOptions.Value;

            //using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            //connection.Execute(
            //    "INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
            //    new
            //    {
            //        value = item.Value,
            //        time = item.Time
            //    });
        }

        public void Delete(int id)
        {
            DataBaseModel.HddMetricDtoDB.Remove(DataBaseModel.HddMetricDtoDB.FirstOrDefault(x => x.Id == id));

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute(
            //    "DELETE FROM hddmetrics WHERE id=@id",
            //    new
            //    {
            //        id = id
            //    });
        }

        public void Update(HddMetricDto item)
        {
            DataBaseModel.HddMetricDtoDB.Remove(DataBaseModel.HddMetricDtoDB.FirstOrDefault(x => x == item));

            DataBaseModel.HddMetricDtoDB.Add(item);

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute(
            //    "UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id",
            //    new
            //    {
            //        value = item.Value,
            //        time = item.Time,
            //        id = item.Id
            //    });
        }

        public IList<HddMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            return DataBaseModel.HddMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds).ToList();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //List<HddMetricDto> metrics = connection.Query<HddMetricDto>(
            //    "SELECT * FROM hddmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
            //    new
            //    {
            //        timeFrom = fromTime.TotalSeconds,
            //        timeTo = toTime.TotalSeconds
            //    }).ToList();

            //return metrics;
        }

        public IList<HddMetricDto> GetAll()
        {
            return DataBaseModel.HddMetricDtoDB.ToList();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //List<HddMetricDto> metrics = connection.Query<HddMetricDto>(
            //    "SELECT * FROM hddmetrics").ToList();

            //return metrics;
        }

        public HddMetricDto GetById(int id)
        {
            return DataBaseModel.HddMetricDtoDB.Where(x => x.Id == id).FirstOrDefault();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //HddMetricDto metric = connection.QuerySingle<HddMetricDto>(
            //    "SELECT Id, Time, Value FROM hddmetrics WHERE id = @id",
            //    new
            //    {
            //        id = id
            //    });

            //return metric;
        }

        public void DeleteByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            IEnumerable<HddMetricDto> todelete = DataBaseModel.HddMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds);

            if (todelete is not null)
            {
                foreach (var item in todelete)
                {
                    DataBaseModel.HddMetricDtoDB.Remove(item);
                }
            }


            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute("DELETE FROM hddmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
            //    new
            //    {
            //        timeFrom = fromTime.TotalSeconds,
            //        timeTo = toTime.TotalSeconds
            //    });
        }
    }
}
