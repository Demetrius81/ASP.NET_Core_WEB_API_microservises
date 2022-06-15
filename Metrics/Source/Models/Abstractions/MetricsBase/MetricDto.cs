namespace Source.Models
{
    /// <summary>
    /// Метрики
    /// </summary>
    public abstract class MetricDto
    {
        /// <summary>
        /// Идетнтификатор метрики
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время снятия метрики
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// Значение метрики
        /// </summary>
        public int Value { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Value} - {Time}";
        }
    }
}