using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    /*
     V api/metrics/hdd/left (размер оставшегося свободного дискового пространства в мегабайтах)
     */

    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase, IMetricsAgent
    {
        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }        
    }
}
