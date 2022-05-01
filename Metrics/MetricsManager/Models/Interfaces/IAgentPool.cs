using System.Collections.Generic;

namespace MetricsManager.Models.Interfaces
{
    public interface IAgentPool<T>
    {
        /// <summary>
        /// Репозиторий агентов
        /// </summary>
        Dictionary<int, T> AgentsRepo { get; set; }

        /// <summary>
        /// Метод добавления агента в репозиторий
        /// </summary>
        void Add(T agent);

        /// <summary>
        /// Метод получения массива агентов
        /// </summary>
        /// <returns></returns>
        T[] Get();
    }
}
