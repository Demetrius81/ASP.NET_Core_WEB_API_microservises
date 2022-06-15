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
    /// Контроллер получения .NET метрик
    /// </summary>
    [Route("api/dotnet")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение .NET метрик")]
    public class DotNetMetricsController : ControllerBase, IMetricsManager
    {
        private readonly IMetricsAgentClient _metricsAgentClient;

        public DotNetMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        /// <summary>
        /// Получение метрик .NET
        /// </summary>
        /// <param name="metricCreateRequest"></param>
        /// <returns></returns>
        [HttpPost("getDotNetMetricsFromAgent")]
        [SwaggerOperation(description: "Получение метрик .NET")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        [SwaggerResponse(404, description: "Связь с агентом не установлена")]
        [ProducesResponseType(typeof(DotNetAllMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent(
            [FromBody] DotNetMetricCreateRequest metricCreateRequest)
        {
            DotNetAllMetricsResponse dotNetAllMetricsResponse = _metricsAgentClient.GetDotNetAllMetrics(metricCreateRequest);

            return Ok(dotNetAllMetricsResponse);
        }

        #region GetMetricsFromAgent

        //[HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent(
        //    [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        //{
        //    DotNetAllMetricsResponse dotNetAllMetricsResponse = _metricsAgentClient.GetDotNetAllMetrics(new DotNetMetricCreateRequest()
        //    {
        //        AgentId = agentId,
        //        FromTime = fromTime,
        //        ToTime = toTime
        //    });
        //    return Ok(dotNetAllMetricsResponse);
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
