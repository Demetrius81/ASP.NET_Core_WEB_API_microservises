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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase, IMetricsController
    {
        private IRamMetricsRepository _metricsRepository;

        private ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger,
            IRamMetricsRepository metricsRepository)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] IMetricCreateRequest requestData)
        {
            RamMetricCreateRequest request = requestData as RamMetricCreateRequest;

            IMetric metric = new RamMetric
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

            RamAllMetricsResponse response = new RamAllMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Ram");
            }

            return Ok(response);
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<IMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            RamAllMetricsResponse response = new RamAllMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Ram в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }

            return Ok(response);
        }       
    }
}
