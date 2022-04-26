using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    public interface IMetricsManager
    {
        /// <summary>
        /// Метод получает метрики от агента
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);

        /// <summary>
        /// Метод получают метрики от всего кластера агентов
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);
    }
}
