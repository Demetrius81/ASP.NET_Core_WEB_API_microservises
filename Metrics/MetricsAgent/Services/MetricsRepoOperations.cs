using MetricsAgent.Models;
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

        public void CreateOperation(Metric item)
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

        public void UpdateOperation(Metric item)
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

        public IList<Metric> GetAllOperation()
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"SELECT * FROM {TabName}";

            var returnList = new List<Metric>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new Metric
                    {
                        Id = reader.GetInt32(0),

                        Value = reader.GetInt32(1),

                        Time = TimeSpan.FromSeconds(reader.GetInt32(2))
                    });
                }
            }
            return returnList;
        }

        public Metric GetByIdOperation(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(ConnectionString);

            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(connection);

            command.CommandText = $"SELECT * FROM {TabName} WHERE id=@id";

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Metric
                    {
                        Id = reader.GetInt32(0),

                        Value = reader.GetInt32(1),

                        Time = TimeSpan.FromSeconds(reader.GetInt32(1))
                    };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
