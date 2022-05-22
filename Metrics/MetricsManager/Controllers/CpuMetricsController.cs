using MetricsManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase, IMetricsManager
    {

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly AgentPool _agentPool;

        private ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(
            IHttpClientFactory httpClientFactory,
            AgentPool agentPool,
            ILogger<CpuMetricsController> logger = null)
        {
            _httpClientFactory = httpClientFactory;

            _agentPool = agentPool;

            _logger = logger;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            HttpClient httpClient = new HttpClient();

            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Cpu от агента {agentId}");
            }

            return Ok();
        }

        [HttpGet("clister/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили все метрики Cpu от всех агентов кластера");
            }

            return Ok();
        }
    }
}
