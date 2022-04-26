using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsManager.Controllers
{
    /// <summary>
    /// CPU контроллер
    /// </summary>
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("clister/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
