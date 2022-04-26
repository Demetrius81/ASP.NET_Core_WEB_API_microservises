using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    /*
     V api/metrics/ram/available (размер свободной оперативной памяти в мегабайтах)
     */

    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase, IMetricsAgent
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        /// <summary>
        /// Метод возвращает количество свободной оперативной памяти
        /// </summary>
        /// <returns></returns>
        [HttpGet("available")]
        public IActionResult GetMetricAvailableRam()
        {
            return Ok();
        }
    }


}
