using MetricsAgent.Models.Interfaces;
using System.Collections.Generic;

namespace MetricsAgent.Models.Responses
{
    public class CpuAllMetricsResponse
    {
        /// <summary>
        /// Коллекция метрик, сформированная для ответа контроллера
        /// </summary>
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
