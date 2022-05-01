using MetricsManager.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MetricsManager.Models
{
    /// <summary>
    /// Класс репозиторий агентов
    /// </summary>
    public class AgentPool : IAgentPool<AgentInfo>
    {
        private Dictionary<int, AgentInfo> _agentsRepo;
                
        public Dictionary<int, AgentInfo> AgentsRepo { get => _agentsRepo; set => _agentsRepo = value; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public AgentPool()
        {
            _agentsRepo = new Dictionary<int, AgentInfo>();
        }
                
        public void Add(AgentInfo agent)
        {
            if (!_agentsRepo.ContainsKey(agent.AgentId))
            {
                _agentsRepo.Add(agent.AgentId, agent);
            }
        }
                
        public AgentInfo[] Get()
        {
            return _agentsRepo.Values.ToArray();
        }
    }
}
