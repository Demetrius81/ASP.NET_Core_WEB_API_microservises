using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
using MetricsAgent.Models.Responses;
using MetricsAgent.Services.Interfaces;
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
        private readonly IDotNetMetricsRepository _metricsRepository;

        private readonly ILogger<DotNetMetricsController> _logger;

        private readonly IMapper _mapper;

        public DotNetMetricsController(
            IDotNetMetricsRepository metricsRepository,
            IMapper mapper = null,
            ILogger<DotNetMetricsController> logger = null)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;

            _mapper = mapper;
        }

        /// <summary>
        /// Задать уровень метрики и время
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            DotNetMetric metric = new DotNetMetric
            {
                Time = request.Time.TotalSeconds,
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
            IList<DotNetMetric> metrics = _metricsRepository.GetAll();

            DotNetAllMetricsResponse response = new DotNetAllMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
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
            IList<DotNetMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            DotNetAllMetricsResponse response = new DotNetAllMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик DotNet " +
                    $"в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }
            return Ok(response);
        }
    }
}
