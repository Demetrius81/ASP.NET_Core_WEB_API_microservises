using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Source.Models.Requests;
using Source.Models.Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MetricsManager.Services
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        HttpClient _httpClient;

        private readonly AgentPool _agentPool;

        private readonly ILogger<MetricsAgentClient> _logger;

        private readonly IMapper _mapper;

        public MetricsAgentClient(
            HttpClient httpClient,
            IAgentsPoolRepository agentsPoolRepository,
            IMapper mapper = null,
            ILogger<MetricsAgentClient> logger = null)
        {
            _httpClient = httpClient;

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

        public CpuAllMetricsResponse GetCpuAllMetrics(CpuMetricCreateRequest cpuMetricRequest)
        {
            try
            {

                IAgentInfo agent;

                if (!_agentPool.AgentsRepo.TryGetValue(cpuMetricRequest.AgentId, out agent))
                {
                    throw new Exception($"Agent Id# {cpuMetricRequest.AgentId} not found.");
                }
                AgentInfo agentInfo = agent as AgentInfo;

                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/cpu/from/" +
                    $"{cpuMetricRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                    $"{cpuMetricRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);

                httpRequestMessage.Headers.Add("Accept", "application/json");

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    CpuAllMetricsResponse cpuAllMetrics = JsonConvert.DeserializeObject<CpuAllMetricsResponse>(responseString);

                    cpuAllMetrics.AgentID = cpuMetricRequest.AgentId;

                    LoggingSituation($"Успешно получили все метрики Cpu от агента {cpuMetricRequest.AgentId}");

                    return cpuAllMetrics;
                }
                //HttpClient httpClient = new HttpClient();

                LoggingSituation($"От агента: {cpuMetricRequest.AgentId} пришел ответ {httpResponseMessage.StatusCode.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        private void LoggingSituation(string message)
        {
            if (_logger is not null)
            {
                _logger.LogDebug(message);
            }
        }

        public DotNetAllMetricsResponse GetDotNetAllMetrics(DotNetMetricCreateRequest dotNetMetricRequest)
        {
            try
            {

                IAgentInfo agent;

                if (!_agentPool.AgentsRepo.TryGetValue(dotNetMetricRequest.AgentId, out agent))
                {
                    throw new Exception($"Agent Id# {dotNetMetricRequest.AgentId} not found.");
                }
                AgentInfo agentInfo = agent as AgentInfo;

                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/dotnet/errors-count/from/" +
                    $"{dotNetMetricRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                    $"{dotNetMetricRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);

                httpRequestMessage.Headers.Add("Accept", "application/json");
                
                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    DotNetAllMetricsResponse dotNetAllMetrics = JsonConvert.DeserializeObject<DotNetAllMetricsResponse>(responseString);

                    dotNetAllMetrics.AgentID = dotNetMetricRequest.AgentId;

                    LoggingSituation($"Успешно получили все метрики DotNet от агента {dotNetMetricRequest.AgentId}");

                    return dotNetAllMetrics;
                }
                //HttpClient httpClient = new HttpClient();

                LoggingSituation($"От агента: {dotNetMetricRequest.AgentId} пришел ответ {httpResponseMessage.StatusCode.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public HddAllMetricsResponse GetHddAllMetrics(HddMetricCreateRequest HddMetricRequest)
        {
            try
            {

                IAgentInfo agent;

                if (!_agentPool.AgentsRepo.TryGetValue(HddMetricRequest.AgentId, out agent))
                {
                    throw new Exception($"Agent Id# {HddMetricRequest.AgentId} not found.");
                }
                AgentInfo agentInfo = agent as AgentInfo;

                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/hdd/left/from/" +
                    $"{HddMetricRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                    $"{HddMetricRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);

                httpRequestMessage.Headers.Add("Accept", "application/json");

                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    HddAllMetricsResponse hddAllMetrics = JsonConvert.DeserializeObject<HddAllMetricsResponse>(responseString);

                    hddAllMetrics.AgentID = HddMetricRequest.AgentId;

                    LoggingSituation($"Успешно получили все метрики Hdd от агента {HddMetricRequest.AgentId}");

                    return hddAllMetrics;
                }
                
                LoggingSituation($"От агента: {HddMetricRequest.AgentId} пришел ответ {httpResponseMessage.StatusCode.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public NetworkAllMetricsResponse GetNetworkAllMetrics(NetworkMetricCreateRequest NetworkMetricRequest)
        {
            try
            {

                IAgentInfo agent;

                if (!_agentPool.AgentsRepo.TryGetValue(NetworkMetricRequest.AgentId, out agent))
                {
                    throw new Exception($"Agent Id# {NetworkMetricRequest.AgentId} not found.");
                }
                AgentInfo agentInfo = agent as AgentInfo;

                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/network/from/" +
                    $"{NetworkMetricRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                    $"{NetworkMetricRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);

                httpRequestMessage.Headers.Add("Accept", "application/json");

                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    NetworkAllMetricsResponse networkAllMetrics = JsonConvert.DeserializeObject<NetworkAllMetricsResponse>(responseString);

                    networkAllMetrics.AgentID = NetworkMetricRequest.AgentId;

                    LoggingSituation($"Успешно получили все метрики Cpu от агента {NetworkMetricRequest.AgentId}");

                    return networkAllMetrics;
                }
                
                LoggingSituation($"От агента: {NetworkMetricRequest.AgentId} пришел ответ {httpResponseMessage.StatusCode.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public RamAllMetricsResponse GetRamAllMetrics(RamMetricCreateRequest ramMetricRequest)
        {
            try
            {

                IAgentInfo agent;

                if (!_agentPool.AgentsRepo.TryGetValue(ramMetricRequest.AgentId, out agent))
                {
                    throw new Exception($"Agent Id# {ramMetricRequest.AgentId} not found.");
                }
                AgentInfo agentInfo = agent as AgentInfo;

                string requestQuery = $"{agentInfo.AgentAddress}api/metrics/ram/available/from/" +
                    $"{ramMetricRequest.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/" +
                    $"{ramMetricRequest.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestQuery);

                httpRequestMessage.Headers.Add("Accept", "application/json");

                HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    RamAllMetricsResponse ramAllMetrics = JsonConvert.DeserializeObject<RamAllMetricsResponse>(responseString);

                    ramAllMetrics.AgentID = ramMetricRequest.AgentId;

                    LoggingSituation($"Успешно получили все метрики Cpu от агента {ramMetricRequest.AgentId}");

                    return ramAllMetrics;
                }
                
                LoggingSituation($"От агента: {ramMetricRequest.AgentId} пришел ответ {httpResponseMessage.StatusCode.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
