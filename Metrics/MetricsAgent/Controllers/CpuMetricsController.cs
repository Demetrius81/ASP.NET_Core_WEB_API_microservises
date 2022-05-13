using AutoMapper;
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
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
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

        /// <summary>
        /// Задать уровень метрики и время
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            CpuMetric metric = new CpuMetric
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
            IList<CpuMetric> metrics = _metricsRepository.GetAll();

            CpuAllMetricsResponse response = new CpuAllMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Cpu");
            }
            return Ok(response);
        }

        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            IList<CpuMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            CpuAllMetricsResponse response = new CpuAllMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Cpu " +
                    $"в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }
            return Ok(response);
        }

        #region SQL_test

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
    }
}
