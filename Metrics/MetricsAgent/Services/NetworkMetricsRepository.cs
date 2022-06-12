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
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public NetworkMetricsRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            DataBaseModel.NetworkMetricDtoDB = new List<NetworkMetricDto>();

            _databaseOptions = databaseOptions;

            //using var connection = new SQLiteConnection(databaseOptions.Value.ConnectionString);

            //connection.Execute("DELETE FROM networkmetrics");
        }

        public void Create(NetworkMetricDto item)
        {
            DataBaseModel.NetworkMetricDtoDB.Add(item);

            //DatabaseOptions databaseOptions = _databaseOptions.Value;

            //using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            //connection.Execute(
            //    "INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
            //    new
            //    {
            //        value = item.Value,
            //        time = item.Time
            //    });
        }

        public void Delete(int id)
        {
            DataBaseModel.NetworkMetricDtoDB.Remove(DataBaseModel.NetworkMetricDtoDB.FirstOrDefault(x => x.Id == id));

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute(
            //    "DELETE FROM networkmetrics WHERE id=@id",
            //    new
            //    {
            //        id = id
            //    });
        }

        public void Update(NetworkMetricDto item)
        {
            DataBaseModel.NetworkMetricDtoDB.Remove(DataBaseModel.NetworkMetricDtoDB.FirstOrDefault(x => x == item));

            DataBaseModel.NetworkMetricDtoDB.Add(item);

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute(
            //    "UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id",
            //    new
            //    {
            //        value = item.Value,
            //        time = item.Time,
            //        id = item.Id
            //    });
        }

        public IList<NetworkMetricDto> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            return DataBaseModel.NetworkMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds).ToList();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //List<NetworkMetricDto> metrics = connection.Query<NetworkMetricDto>(
            //    "SELECT * FROM networkmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
            //    new
            //    {
            //        timeFrom = fromTime.TotalSeconds,
            //        timeTo = toTime.TotalSeconds
            //    }).ToList();

            //return metrics;
        }

        public IList<NetworkMetricDto> GetAll()
        {
            return DataBaseModel.NetworkMetricDtoDB.ToList();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //List<NetworkMetricDto> metrics = connection.Query<NetworkMetricDto>(
            //    "SELECT * FROM networkmetrics").ToList();

            //return metrics;
        }

        public NetworkMetricDto GetById(int id)
        {
            return DataBaseModel.NetworkMetricDtoDB.Where(x => x.Id == id).FirstOrDefault();

            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //NetworkMetricDto metric = connection.QuerySingle<NetworkMetricDto>(
            //    "SELECT Id, Time, Value FROM networkmetrics WHERE id = @id",
            //    new
            //    {
            //        id = id
            //    });

            //return metric;
        }

        public void DeleteByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            IEnumerable<NetworkMetricDto> todelete = DataBaseModel.NetworkMetricDtoDB.Where(x => x.Time < toTime.TotalSeconds && x.Time > fromTime.TotalSeconds);

            if (todelete is not null)
            {
                foreach (var item in todelete)
                {
                    DataBaseModel.NetworkMetricDtoDB.Remove(item);
                }
            }


            //using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            //connection.Execute("DELETE FROM networkmetrics WHERE time BETWEEN @timeFrom AND @timeTo",
            //    new
            //    {
            //        timeFrom = fromTime.TotalSeconds,
            //        timeTo = toTime.TotalSeconds
            //    });
        }
    }
}
