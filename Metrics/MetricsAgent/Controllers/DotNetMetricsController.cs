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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase, IMetricsController
    {
        private IDotNetMetricsRepository _metricsRepository;

        private ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger,
            IDotNetMetricsRepository metricsRepository)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] IMetricCreateRequest requestData)
        {
            DotNetMetricCreateRequest request = requestData as DotNetMetricCreateRequest;

            IMetric metric = new DotNetMetric
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

            DotNetAllMetricsResponse response = new DotNetAllMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик DotNet");
            }
                        
            return Ok(response);
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<IMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            DotNetAllMetricsResponse response = new DotNetAllMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик DotNet в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }

            return Ok(response);
        }
    }
}
