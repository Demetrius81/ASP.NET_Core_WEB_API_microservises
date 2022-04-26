using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MetricsManager.Models
{
    /// <summary>
    /// Класс репозиторий агентов
    /// </summary>
    public class AgentPool
    {        
        private Dictionary<int, AgentInfo> _agentsRepo;

        /// <summary>
        /// Репозиторий агентов
        /// </summary>
        public Dictionary<int, AgentInfo> AgentsRepo { get => _agentsRepo; set => _agentsRepo = value; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public AgentPool()
        {
            _agentsRepo = new Dictionary<int, AgentInfo>();
        }

        /// <summary>
        /// Метод добавления агента в репозиторий
        /// </summary>
        public void Add(AgentInfo agent)
        {
            if (!_agentsRepo.ContainsKey(agent.AgentId))
            {
                _agentsRepo.Add(agent.AgentId, agent);
            }
        }

        /// <summary>
        /// Метод получения массива агентов
        /// </summary>
        /// <returns></returns>
        public AgentInfo[] Get()
        {
            return _agentsRepo.Values.ToArray();
        }
    }
}
