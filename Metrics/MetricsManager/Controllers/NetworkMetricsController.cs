using MetricsManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Source.Models.Requests;
using Source.Models.Responses;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase, IMetricsManager
    {
        private readonly IMetricsAgentClient _metricsAgentClient;

        public NetworkMetricsController(IMetricsAgentClient metricsAgentClient)
        {
            _metricsAgentClient = metricsAgentClient;
        }

        [HttpGet("getNetworkMetricsFromAgent")]
        [ProducesResponseType(typeof(NetworkAllMetricsResponse), StatusCodes.Status200OK)]
        public IActionResult GetMetricsFromAgent(
            [FromBody] NetworkMetricCreateRequest metricCreateRequest)
        {
            NetworkAllMetricsResponse networkAllMetricsResponse = _metricsAgentClient.GetNetworkAllMetrics(new NetworkMetricCreateRequest()
            {
                AgentId = metricCreateRequest.AgentId,
                FromTime = metricCreateRequest.FromTime,
                ToTime = metricCreateRequest.ToTime
            });
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
