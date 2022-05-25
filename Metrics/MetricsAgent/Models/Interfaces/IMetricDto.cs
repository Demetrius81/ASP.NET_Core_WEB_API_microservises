using System;

namespace MetricsAgent.Models
{
    public interface IMetricDto
    {
        /// <summary>
        /// Идентификатор метрики
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Время (тип данных TimeSpan)
        /// </summary>
        TimeSpan Time { get; set; }

        /// <summary>
        /// Значение метрики
        /// </summary>
        int Value { get; set; }
    }
}