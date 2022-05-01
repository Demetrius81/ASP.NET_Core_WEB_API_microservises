using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Services
{
    public class MetricsRepoOperations
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        private string TabName { get; init; }

        public MetricsRepoOperations(string tabName)
        {
            TabName = tabName;
        }

        public void CreateOperation(IMetric item)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"INSERT INTO {TabName}(value, time) VALUES(@value, @time)";

            command.Parameters.AddWithValue("@value", item.Value);

            command.Parameters.AddWithValue("@time", item.Time.TotalSeconds);

            command.Prepare();

            command.ExecuteNonQuery();
        }

        public void DeleteOperation(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"DELETE FROM {TabName} WHERE id=@id";

            command.Parameters.AddWithValue("@id", id);

            command.Prepare();

            command.ExecuteNonQuery();
        }

        public void UpdateOperation(IMetric item)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"UPDATE {TabName} SET value = @value, time = @time WHERE id = @id; ";

            command.Parameters.AddWithValue("@id", item.Id);

            command.Parameters.AddWithValue("@value", item.Value);

            command.Parameters.AddWithValue("@time", item.Time.TotalSeconds);

            command.Prepare();

            command.ExecuteNonQuery();
        }

        public IList<IMetric> GetAllOperation(IMetric metric)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"SELECT * FROM {TabName}";

            var returnList = new List<IMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    metric.Id = reader.GetInt32(0);

                    metric.Value = reader.GetInt32(1);

                    metric.Time = TimeSpan.FromSeconds(reader.GetInt32(2));

                    returnList.Add(metric);
                }
            }
            return returnList;
        }

        public IList<IMetric> GetByTimePeriodOperation(TimeSpan fromTime, TimeSpan toTime, IMetric metric)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"SELECT * FROM {TabName} WHERE time BETWEEN {fromTime.TotalSeconds} AND {toTime.TotalSeconds}; ";

            var returnList = new List<IMetric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    metric.Id = reader.GetInt32(0);

                    metric.Value = reader.GetInt32(1);

                    metric.Time = TimeSpan.FromSeconds(reader.GetInt32(2));

                    returnList.Add(metric);
                }
            }
            return returnList;
        }

        public IMetric GetByIdOperation(int id, IMetric metric)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"SELECT * FROM {TabName} WHERE id=@id";

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    metric.Id = reader.GetInt32(0);

                    metric.Value = reader.GetInt32(1);

                    metric.Time = TimeSpan.FromSeconds(reader.GetInt32(1));

                    return metric;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
