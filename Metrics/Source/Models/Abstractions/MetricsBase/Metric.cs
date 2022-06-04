using System;

namespace Source.Models
{
    /// <summary>
    /// Метрики
    /// </summary>
    public abstract class Metric
    {
        /// <summary>
        /// Идетнтификатор метрики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Значение метрики
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Время снятия метрики
        /// </summary>
        public TimeSpan Time { get; set; }
    }
}
