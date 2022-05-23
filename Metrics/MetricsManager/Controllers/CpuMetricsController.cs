using MetricsManager.Models;
using MetricsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Source.Models.Responses;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase, IMetricsManager
    {

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly AgentPool _agentPool;

        private readonly ILogger<CpuMetricsController> _logger;

        private readonly IMapper _mapper;

        public CpuMetricsController(
            IHttpClientFactory httpClientFactory,
            IAgentsPoolRepository agentsPoolRepository,
            IMapper mapper = null,
            ILogger<CpuMetricsController> logger = null)
        {
            _httpClientFactory = httpClientFactory;

            List<AgentInfo> agents = new List<AgentInfo>();

            foreach (AgentInfoDto agent in agentsPoolRepository.Get().Values)
            {
                agents.Add(mapper.Map<AgentInfo>(agent));
            }
            Dictionary<int, IAgentInfo> agentDict = new();

            foreach (AgentInfo agent in agents)
            {
                agentDict.Add(agent.AgentId, agent);
            }
            _agentPool = new AgentPool();

            _agentPool.AgentsRepo = agentDict;

            _logger = logger;

            _mapper = mapper;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IAgentInfo agent;

            if (!_agentPool.AgentsRepo.TryGetValue(agentId, out agent))
            {
                LoggingSituation($"Нет такого агента: {agentId}");

                return BadRequest();
            }
            AgentInfo agentInfo = agent as AgentInfo;

            string requestQuery = $"{agentInfo.AgentAddress}api/metrics/cpu/from/" +
                $"{fromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                $"{toTime.ToString("dd\\.hh\\:mm\\:ss")}";

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);

            httpRequestMessage.Headers.Add("Accept", "application/json");

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage httpResponseMessage = httpClient.SendAsync(httpRequestMessage).Result;

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                CpuAllMetricsResponse cpuAllMetrics = JsonConvert.DeserializeObject<CpuAllMetricsResponse>(responseString);

                cpuAllMetrics.AgentID = agentId;

                return Ok();

                LoggingSituation($"Успешно получили все метрики Cpu от агента {agentId}");
            }

            //HttpClient httpClient = new HttpClient();

            LoggingSituation($"От агента: {agentId} пришел ответ {httpResponseMessage.StatusCode.ToString()}");

            return BadRequest();
        }

        [HttpGet("clister/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            LoggingSituation($"Успешно получили все метрики Cpu от всех агентов кластера");

            return Ok();
        }

        private void LoggingSituation(string message)
        {
            if (_logger is not null)
            {
                _logger.LogDebug(message);
            }
        }
    }
}
