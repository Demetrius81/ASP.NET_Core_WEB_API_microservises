using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class NetworkAllMetricsResponse
    {
        /// <summary>
        /// Коллекция метрик, сформированная для ответа контроллера
        /// </summary>
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
