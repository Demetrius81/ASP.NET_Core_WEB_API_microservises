using System.Collections.Generic;

namespace Source.Models.Response
{
    /// <summary>
    /// Ответ агента
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AllMetricsResponse<T> where T : class
    {
        /// <summary>
        /// Коллекция метрик, сформированная для ответа контроллера
        /// </summary>
        public List<T> Metrics { get; set; }

        /// <summary>
        /// Идентификатор агента
        /// </summary>
        public int AgentID { get; set; }
    }
}
