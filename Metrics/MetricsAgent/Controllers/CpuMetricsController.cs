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
        private ICpuMetricsRepository _metricsRepository;

        private ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger,
            ICpuMetricsRepository metricsRepository)
        {
            _metricsRepository = metricsRepository;

            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] IMetricCreateRequest requestData)
        {
            CpuMetricCreateRequest request = requestData as CpuMetricCreateRequest;

            IMetric metric = new CpuMetric
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

            CpuAllMetricsResponse response = new CpuAllMetricsResponse()
            {
                Metrics = new List<IMetric>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
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
            IList<IMetric> metrics = _metricsRepository.GetByTimePeriod(fromTime, toTime);

            CpuAllMetricsResponse response = new CpuAllMetricsResponse()
            {
                Metrics = new List<IMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto
                {
                    Time = metric.Time,

                    Value = metric.Value,

                    Id = metric.Id
                });
            }
            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно получили список всех метрик Cpu в интервале времени от {fromTime.TotalSeconds} секунды до {toTime.TotalSeconds} секунды");
            }

            return Ok(response);
        }

        #region sql-test

        //[HttpGet("sql-test")]
        public IActionResult TryToSqlLite()
        {
            string sql = "Data Source=:memory:";

            string request = "SELECT SQLITE_VERSION()";

            using (SQLiteConnection connection = new SQLiteConnection(sql))
            {
                connection.Open();

                using SQLiteCommand command = new SQLiteCommand(request, connection);

                string version = command.ExecuteScalar().ToString();

                return Ok(version);
            }
        }

        #endregion
    }
}
