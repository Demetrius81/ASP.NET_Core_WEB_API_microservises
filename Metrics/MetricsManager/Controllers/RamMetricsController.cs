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
    /// Контроллер получения RAM метрик
    /// </summary>
    [Route("api/ram")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение RAM метрик")]
    public class RamMetricsController : ControllerBase, IMetricsManager
    {
        private readonly IMetricsAgentClient _metricsAgentClient;

        public RamMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение метрик RAM
        /// </summary>
        /// <param name="metricCreateRequest"></param>
        /// <returns></returns>
        [HttpPost("getRamMetricsFromAgent")]
        [SwaggerOperation(description: "Получение метрик RAM")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        [SwaggerResponse(404, description: "Связь с агентом не установлена")]
        [ProducesResponseType(typeof(RamAllMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent(
            [FromBody] RamMetricCreateRequest metricCreateRequest)
        {
            RamAllMetricsResponse ramAllMetricsResponse = _metricsAgentClient.GetRamAllMetrics(metricCreateRequest);

            return Ok(ramAllMetricsResponse);
        }

        #region GetMetricsFromAgent

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    RamAllMetricsResponse ramAllMetricsResponse = _metricsAgentClient.GetRamAllMetrics(new RamMetricCreateRequest()
        //    {
        //        AgentId = agentId,
        //        FromTime = fromTime,
        //        ToTime = toTime
        //    });
        //    return Ok(ramAllMetricsResponse);
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
