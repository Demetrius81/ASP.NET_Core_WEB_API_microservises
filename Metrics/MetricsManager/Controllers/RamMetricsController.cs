using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase, IMetricsManager
    {
        private ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger = null)
        {
            _logger = logger;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Ram от агента {agentId}");
            }

            return Ok();
        }

        [HttpGet("clister/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Ram от всех агентов кластера");
            }

            return Ok();
        }
    }
}
