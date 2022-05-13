namespace MetricsAgent.Models
{
    public interface IMetric
    {
        /// <summary>
        /// Идентификатор метрики
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Время в секундах (тип данных double)
        /// </summary>
        double Time { get; set; }

        /// <summary>
        /// Значение метрики
        /// </summary>
        int Value { get; set; }
    }
}