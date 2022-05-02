using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    public interface IMetricsAgent
    {


        /// <summary>
        /// Метод возвращает метрики по запросу
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);


    }
}
