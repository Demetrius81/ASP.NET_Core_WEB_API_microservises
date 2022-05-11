using MetricsAgent.Models;
using MetricsAgent.Models.Interfaces;
using MetricsAgent.Models.Requests;
using MetricsAgent.Models.Responses;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        private INetworkMetricsRepository _metricsRepository;

        private ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger,
            INetworkMetricsRepository metricsRepository)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] IMetricCreateRequest requestData)
        {
            NetworkMetricCreateRequest request = requestData as NetworkMetricCreateRequest;

            IMetric metric = new NetworkMetric
            {
                Time = request.Time,

                Value = request.Value
            };
            _metricsRepository.Create(metric);

            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно добавили новую метрику: {metric}");
            }

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IList<IMetric> metrics = _metricsRepository.GetAll();

            NetworkAllMetricsResponse response = new NetworkAllMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
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
            IList<IMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            NetworkAllMetricsResponse response = new NetworkAllMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Network в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }

            return Ok(response);
        }        
    }
}
