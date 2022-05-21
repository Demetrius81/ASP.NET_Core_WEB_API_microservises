using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Responses;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase, IMetricsController
    {
        private readonly INetworkMetricsRepository _metricsRepository;

        private readonly ILogger<NetworkMetricsController> _logger;

        private readonly IMapper _mapper;

        public NetworkMetricsController(
            INetworkMetricsRepository metricsRepository,
            IMapper mapper = null,
            ILogger<NetworkMetricsController> logger = null)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;

            _mapper = mapper;
        }

        #region For delete

        ///// <summary>
        ///// Задать уровень метрики и время
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost("create")]
        //public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        //{
        //    NetworkMetric metric = new NetworkMetric
        //    {
        //        Time = request.Time.TotalSeconds,
        //        Value = request.Value
        //    };
        //    _metricsRepository.Create(metric);

        //    if (_logger is not null)
        //    {
        //        _logger.LogDebug($"Успешно добавили новую метрику: {metric}");
        //    }
        //    return Ok();
        //}

        #endregion

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IList<NetworkMetric> metrics = _metricsRepository.GetAll();

            NetworkAllMetricsResponse response = new NetworkAllMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Network");
            }
            return Ok(response);
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<NetworkMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            NetworkAllMetricsResponse response = new NetworkAllMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Network " +
                    $"в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }
            return Ok(response);
        }
    }
}
