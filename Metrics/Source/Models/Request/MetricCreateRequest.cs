using System;

namespace Source.Models.Request
{
    public abstract class MetricCreateRequest
    {
        /// <summary>
        /// Время (тип данных TimeSpan)
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Значение метрики
        /// </summary>
        public int Value { get; set; }
    }
}