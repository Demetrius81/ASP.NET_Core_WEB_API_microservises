using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase, IMetricsManager
    {
        private ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger = null)
        {
            _logger = logger;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Hdd от агента {agentId}");
            }

            return Ok();
        }

        [HttpGet("clister/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Hdd от всех агентов кластера");
            }

            return Ok();
        }
    }
}
