using System.Collections.Generic;

namespace MetricsManager.Models.Interfaces
{
    public interface IAgentPool<TKey, TValue>
    {
        /// <summary>
        /// Репозиторий агентов
        /// </summary>
        Dictionary<TKey, TValue> AgentsRepo { get; set; }
    }
}
