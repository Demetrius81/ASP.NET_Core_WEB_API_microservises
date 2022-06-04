using MetricsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Source.Models.Requests;
using Source.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace MetricsManager.Controllers
{
    /// <summary>
    /// Контроллер получения Network метрик
    /// </summary>
    [Route("api/network")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение Network метрик")]
    public class NetworkMetricsController : ControllerBase, IMetricsManager
    {
        private readonly IMetricsAgentClient _metricsAgentClient;

        public NetworkMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }
        /// <summary>
        /// Получение метрик Network
        /// </summary>
        /// <param name="metricCreateRequest"></param>
        /// <returns></returns>
        [HttpPost("getNetworkMetricsFromAgent")]
        [SwaggerOperation(description: "Получение метрик Network")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        [SwaggerResponse(404, description: "Связь с агентом не установлена")]
        [ProducesResponseType(typeof(NetworkAllMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent(
            [FromBody] NetworkMetricCreateRequest metricCreateRequest)
        {
            NetworkAllMetricsResponse networkAllMetricsResponse = _metricsAgentClient.GetNetworkAllMetrics(metricCreateRequest);

            return Ok(networkAllMetricsResponse);
        }

        #region GetMetricsFromAgent

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    NetworkAllMetricsResponse networkAllMetricsResponse = _metricsAgentClient.GetNetworkAllMetrics(new NetworkMetricCreateRequest()
        //    {
        //        AgentId = agentId,
        //        FromTime = fromTime,
        //        ToTime = toTime
        //    });
        //    return Ok(networkAllMetricsResponse);
        //}

        //[HttpGet("clister/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAllCluster(
        //    [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{           
        //    return Ok();
        //}

        #endregion
    }
}
