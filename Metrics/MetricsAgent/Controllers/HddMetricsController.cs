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
    /// Контроллер сбора HDD метрик
    /// </summary>
    [Route("api/metrics/hdd")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение HDD метрик")]
    public class HddMetricsController : ControllerBase, IMetricsController
    {
        private readonly IHddMetricsRepository _metricsRepository;

        private readonly ILogger<HddMetricsController> _logger;

        private readonly IMapper _mapper;

        public HddMetricsController(
            IHddMetricsRepository metricsRepository,
            IMapper mapper = null,
            ILogger<HddMetricsController> logger = null)
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
        //public IActionResult Create([FromBody] HddMetricCreateRequest request)
        //{
        //    HddMetric metric = new HddMetric
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
        [SwaggerOperation(description: "Получение метрик HDD")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetAll()
        {
            IList<HddMetricDto> metrics = _metricsRepository.GetAll();

            HddAllMetricsResponse response = new HddAllMetricsResponse()
            {
                Metrics = new List<HddMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Hdd");
            }
            return Ok(response);
        }

        /// <summary>
        /// Получить метрики за указанный период
        /// </summary>
        /// <param name="fromTime">Время с...</param>
        /// <param name="toTime">Время по...</param>
        /// <returns>Результат операции</returns>
        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        [SwaggerOperation(description: "Получение метрик HDD за указанный период")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<HddMetricDto> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            HddAllMetricsResponse response = new HddAllMetricsResponse()
            {
                Metrics = new List<HddMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Hdd " +
                    $"в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }
            return Ok(response);
        }
    }
}
