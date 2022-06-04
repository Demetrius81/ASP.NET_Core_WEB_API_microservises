using AutoMapper;
using Source.Models;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Source.Models.Responses;
using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace MetricsAgent.Controllers
{
    /// <summary>
    /// Контроллер сбора .NET метрик
    /// </summary>
    [Route("api/metrics/dotnet")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение .NET метрик")]
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

        #region For delete

        ///// <summary>
        ///// Задать уровень метрики и время
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost("create")]
        //public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        //{
        //    DotNetMetric metric = new DotNetMetric
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

        /// <summary>
        /// Получить метрики за весь период
        /// </summary>
        /// <returns>Результат операции</returns>
        [HttpGet("all")]
        [SwaggerOperation(description: "Получение метрик .NET")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetAll()
        {
            IList<DotNetMetricDto> metrics = _metricsRepository.GetAll();

            DotNetAllMetricsResponse response = new DotNetAllMetricsResponse()
            {
                Metrics = new List<DotNetMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик DotNet");
            }
            return Ok(response);
        }

        /// <summary>
        /// Получить метрики за указанный период
        /// </summary>
        /// <param name="fromTime">Время с...</param>
        /// <param name="toTime">Время по...</param>
        /// <returns>Результат операции</returns>
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        [SwaggerOperation(description: "Получение метрик .NET за указанный период")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<DotNetMetricDto> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            DotNetAllMetricsResponse response = new DotNetAllMetricsResponse()
            {
                Metrics = new List<DotNetMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetric>(metric));
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
