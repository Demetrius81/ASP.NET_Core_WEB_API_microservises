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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase, IMetricsController
    {
        private IHddMetricsRepository _metricsRepository;

        private ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger,
            IHddMetricsRepository metricsRepository)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] IMetricCreateRequest requestData)
        {
            HddMetricCreateRequest request = requestData as HddMetricCreateRequest;

            IMetric metric = new HddMetric
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

            HddAllMetricsResponse response = new HddAllMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Hdd");
            }

            return Ok(response);
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<IMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            HddAllMetricsResponse response = new HddAllMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Hdd в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }

            return Ok(response);
        }        
    }
}
