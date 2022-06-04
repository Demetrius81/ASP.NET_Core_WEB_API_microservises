using AutoMapper;
using MetricsAgent.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
    /// Контроллер сбора CPU метрик
    /// </summary>
    [Route("api/metrics/cpu")]
    [ApiController]
    [SwaggerTag("Предоставляет возможность получение CPU метрик")]
    public class CpuMetricsController : ControllerBase, IMetricsController
    {
        private readonly ICpuMetricsRepository _metricsRepository;

        private readonly ILogger<CpuMetricsController> _logger;

        private readonly IMapper _mapper;

        public CpuMetricsController(
            ICpuMetricsRepository metricsRepository,
            IMapper mapper = null,
            ILogger<CpuMetricsController> logger = null)
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
        //public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        //{
        //    CpuMetric metric = new CpuMetric
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

        //[HttpGet("sql-test")]
        //public IActionResult TryToSqlLite()
        //{
        //    string sql = "Data Source=:memory:";

        //    string request = "SELECT SQLITE_VERSION()";

        //    using (SQLiteConnection connection = new SQLiteConnection(sql))
        //    {
        //        connection.Open();

        //        using SQLiteCommand command = new SQLiteCommand(request, connection);

        //        string version = command.ExecuteScalar().ToString();

        //        return Ok(version);
        //    }
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
            IList<CpuMetricDto> metrics = _metricsRepository.GetAll();

            CpuAllMetricsResponse response = new CpuAllMetricsResponse()
            {
                Metrics = new List<CpuMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Cpu");
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
        [SwaggerOperation(description: "Получение метрик CPU за указанный период")]
        [SwaggerResponse(200, description: "Метрики успешно получены")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<CpuMetricDto> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            CpuAllMetricsResponse response = new CpuAllMetricsResponse()
            {
                Metrics = new List<CpuMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetric>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Cpu " +
                    $"в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }
            return Ok(response);
        }
    }
}
