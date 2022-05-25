using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class HddAllMetricsResponse
    {
        /// <summary>
        /// Коллекция метрик, сформированная для ответа контроллера
        /// </summary>
        public List<HddMetricDto> Metrics { get; set; }
    }
}
