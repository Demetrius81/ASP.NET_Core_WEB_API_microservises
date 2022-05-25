using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    public interface IMetricsController
    {
        /// <summary>
        /// Получить статистику по метрике за весь период
        /// </summary>
        /// <returns></returns>
        IActionResult GetAll();

        /// <summary>
        /// Получить статистику по метрике за период
        /// </summary>
        /// <param name="fromTime">Время начала периода</param>
        /// <param name="toTime">Время окончания периода</param>
        /// <returns></returns>
        IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);
    }
}