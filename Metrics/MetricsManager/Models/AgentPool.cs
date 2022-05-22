using MetricsManager.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MetricsManager.Models
{
    /// <summary>
    /// Класс репозиторий агентов
    /// </summary>
    public class AgentPool : IAgentPool<int, IAgentInfo>
    {
        private Dictionary<int, IAgentInfo> _agentsRepo;
                
        public Dictionary<int, IAgentInfo> AgentsRepo { get => _agentsRepo; set => _agentsRepo = value; }        
    }
}
