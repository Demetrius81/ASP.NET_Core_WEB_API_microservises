using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    /*
     V api/metrics/cpu/from/{fromTime}/to/{toTime}/percentiles/{percentile}
     V api/metrics/cpu//from/{fromTime}/to/{toTime}/
    */

    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase, IMetricsAgent
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

        /// <summary>
        /// Метод возвращает загрузку CPU в процентах
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="percentile"></param>
        /// <returns></returns>
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsCpuPercentiles(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, int percentile)
        {
            return Ok();
        }
    }
}
