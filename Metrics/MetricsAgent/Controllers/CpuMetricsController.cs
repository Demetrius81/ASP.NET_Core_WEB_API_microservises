using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    /*
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
    }
}
