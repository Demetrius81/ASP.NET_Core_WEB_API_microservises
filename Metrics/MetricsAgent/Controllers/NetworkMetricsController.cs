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
    /// Контроллер сбора Network метрик
    /// </summary>
    [Route("api/metrics/network")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение Network метрик")]
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

        /// <summary>
        /// Получить метрики за весь период
        /// </summary>
        /// <returns>Результат операции</returns>
        [HttpGet("all")]
        [SwaggerOperation(description: "Получение метрик Network")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetAll()
        {
            IList<NetworkMetricDto> metrics = _metricsRepository.GetAll();

            NetworkAllMetricsResponse response = new NetworkAllMetricsResponse()
            {
                Metrics = new List<NetworkMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Network");
            }
            return Ok(response);
        }

        /// <summary>
        /// Получить метрики за указанный период
        /// </summary>
        /// <param name="fromTime">Время с...</param>
        /// <param name="toTime">Время по...</param>
        /// <returns>Результат операции</returns>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        [SwaggerOperation(description: "Получение метрик Network за указанный период")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<NetworkMetricDto> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            NetworkAllMetricsResponse response = new NetworkAllMetricsResponse()
            {
                Metrics = new List<NetworkMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetric>(metric));
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
