using System;

namespace Source.Models.Request
{
    /// <summary>
    /// Запрос сервиса
    /// </summary>
    public abstract class MetricCreateRequest
    {
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