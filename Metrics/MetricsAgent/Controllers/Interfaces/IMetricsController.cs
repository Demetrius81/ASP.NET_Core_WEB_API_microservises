using MetricsAgent.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    public interface IMetricsController
    {
        /// <summary>
        /// Метод создает метрику и сохраняет ее в репозиторий
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        IActionResult Create([FromBody] IMetricCreateRequest requestData);

        /// <summary>
        /// Метод возвращает все метрики из репозитория по запросу
        /// </summary>
        /// <returns></returns>
        IActionResult GetAll();

        /// <summary>
        /// Метод возвращает метрики из репозитория по запросу в заданном интервале времени
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <returns></returns>
        IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime);


    }
}
