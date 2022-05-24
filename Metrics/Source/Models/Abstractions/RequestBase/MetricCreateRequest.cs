using System;

namespace Source.Models.Request
{
    public abstract class MetricCreateRequest
    {
        ///// <summary>
        ///// Время (тип данных TimeSpan)
        ///// </summary>
        //public TimeSpan Time { get; set; }

        ///// <summary>
        ///// Значение метрики
        ///// </summary>
        //public int Value { get; set; }

        /// <summary>
        /// Идентификатор агента
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// Время с...
        /// </summary>
        public TimeSpan FromTime { get; set; }

        /// <summary>
        /// Время по...
        /// </summary>
        public TimeSpan ToTime { get; set; }
    }
}