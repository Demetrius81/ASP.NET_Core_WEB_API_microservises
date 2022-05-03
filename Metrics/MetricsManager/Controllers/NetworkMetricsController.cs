using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase, IMetricsManager
    {
        private ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Network от агента {agentId}");
            }

            return Ok();
        }

        [HttpGet("clister/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Network от всех агентов кластера");
            }

            return Ok();
        }
    }
}
