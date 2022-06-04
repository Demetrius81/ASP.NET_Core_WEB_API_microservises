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
    /// Контроллер получения HDD метрик
    /// </summary>
    [Route("api/hdd")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение HDD метрик")]
    public class HddMetricsController : ControllerBase, IMetricsManager
    {
        private readonly IMetricsAgentClient _metricsAgentClient;

        public HddMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение метрик HDD
        /// </summary>
        /// <param name="metricCreateRequest"></param>
        /// <returns></returns>
        [HttpPost("getHddMetricsFromAgent")]
        [SwaggerOperation(description: "Получение метрик HDD")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        [SwaggerResponse(404, description: "Связь с агентом не установлена")]
        [ProducesResponseType(typeof(HddAllMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent(
            [FromBody] HddMetricCreateRequest metricCreateRequest)
        {
            HddAllMetricsResponse hddAllMetricsResponse = _metricsAgentClient.GetHddAllMetrics(metricCreateRequest);

            return Ok(hddAllMetricsResponse);
        }

        #region GetMetricsFromAgent

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    HddAllMetricsResponse hddAllMetricsResponse = _metricsAgentClient.GetHddAllMetrics(new HddMetricCreateRequest()
        //    {
        //        AgentId = agentId,
        //        FromTime = fromTime,
        //        ToTime = toTime
        //    });
        //    return Ok(hddAllMetricsResponse);
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
