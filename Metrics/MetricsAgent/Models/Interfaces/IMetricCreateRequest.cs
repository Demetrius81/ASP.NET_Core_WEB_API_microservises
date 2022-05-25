using System;

namespace MetricsAgent.Models.Interfaces
{
    public interface IMetricCreateRequest
    {
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