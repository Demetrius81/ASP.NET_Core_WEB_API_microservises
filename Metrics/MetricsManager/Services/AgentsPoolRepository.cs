using Dapper;
using MetricsManager.Models;
using MetricsManager.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Services
{
    public class AgentsPoolRepository : IAgentsPoolRepository
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public AgentsPoolRepository(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        public void Add(AgentInfoDto item)
        {
            DatabaseOptions databaseOptions = _databaseOptions.Value;

            using var connection = new SQLiteConnection(databaseOptions.ConnectionString);

            connection.Execute(
                "INSERT INTO agentsrepo(id, agentaddress, enable) VALUES(@id, @agentaddress, @enable)",
                new
                {
                    id = item.AgentId,
                    agentaddress = item.AgentAddress,
                    enable = item.Enable
                });
        }

        public void Remove(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "DELETE FROM agentsrepo WHERE id=@id",
                new
                {
                    id = id
                });
        }

        public void Update(AgentInfoDto item)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            connection.Execute(
                "UPDATE agentsrepo SET agentaddress = @agentaddress, enable = @enable WHERE id = @id",
                new
                {
                    agentaddress = item.AgentAddress.ToString(),
                    enable = item.Enable,
                    id = item.AgentId
                });
        }

        public IDictionary<int, AgentInfoDto> Get()
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            List<AgentInfoDto> agentsq = connection.Query<AgentInfoDto>(
                "SELECT * FROM agentsrepo").ToList();

            Dictionary<int, AgentInfoDto> agents = agentsq.ToDictionary(x => x.AgentId, x => x);

            //Dictionary<int, AgentInfoDto> agents = connection.Query<AgentInfoDto>(
            //    "SELECT * FROM agentsrepo").ToDictionary(x => x.AgentId, y => y);

            return agents;
        }

        public AgentInfoDto GetById(int id)
        {
            using var connection = new SQLiteConnection(_databaseOptions.Value.ConnectionString);

            AgentInfoDto agent = connection.QuerySingle<AgentInfoDto>(
                "SELECT id, agentaddress, enable FROM agentsrepo WHERE id = @id",
                new
                {
                    id = id
                });

            return agent;
        }
    }
}
