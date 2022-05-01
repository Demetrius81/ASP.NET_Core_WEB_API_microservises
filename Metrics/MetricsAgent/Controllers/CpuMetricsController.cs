using MetricsAgent.Models;
using MetricsAgent.Models.Requests;
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
    public class CpuMetricsController : ControllerBase, IMetricsAgent
    {
        private ICpuMetricsRepository _cpuMetricsRepository;

        private ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger,
            ICpuMetricsRepository cpuMetricsRepository)
        {
            _cpuMetricsRepository = cpuMetricsRepository;

            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            CpuMetric cpuMetric = new CpuMetric
            {
                Time = request.Time,

                Value = request.Value
            };
            _cpuMetricsRepository.Create(cpuMetric);

            if (_logger is not null)
            {
                _logger.LogDebug($"Успешно добавили новую метрику: {cpuMetric}");
            }

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _cpuMetricsRepository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
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
            return Ok(response);
        }

        [HttpGet("sql-test")]
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


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
