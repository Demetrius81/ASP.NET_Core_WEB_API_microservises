using AutoMapper;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Source.Models;
using Source.Models.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    /// <summary>
    /// Контроллер сбора RAM метрик
    /// </summary>
    [Route("api/metrics/ram")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение RAM метрик")]
    public class RamMetricsController : ControllerBase, IMetricsController
    {
        private readonly IRamMetricsRepository _metricsRepository;

        private readonly ILogger<RamMetricsController> _logger;

        private readonly IMapper _mapper;

        public RamMetricsController(
            IRamMetricsRepository metricsRepository,
            IMapper mapper = null,
            ILogger<RamMetricsController> logger = null)
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
        //public IActionResult Create([FromBody] RamMetricCreateRequest request)
        //{
        //    RamMetric metric = new RamMetric
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
        [SwaggerOperation(description: "Получение метрик CPU")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetAll()
        {
            IList<RamMetricDto> metrics = _metricsRepository.GetAll();

            RamAllMetricsResponse response = new RamAllMetricsResponse()
            {
                Metrics = new List<RamMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Ram");
            }
            return Ok(response);
        }

        /// <summary>
        /// Получить метрики за указанный период
        /// </summary>
        /// <param name="fromTime">Время с...</param>
        /// <param name="toTime">Время по...</param>
        /// <returns>Результат операции</returns>
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        [SwaggerOperation(description: "Получение метрик RAM за указанный период")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<RamMetricDto> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            RamAllMetricsResponse response = new RamAllMetricsResponse()
            {
                Metrics = new List<RamMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Ram " +
                    $"в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }
            return Ok(response);
        }
    }
}
