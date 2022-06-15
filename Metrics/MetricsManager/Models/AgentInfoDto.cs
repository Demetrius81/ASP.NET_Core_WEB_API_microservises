using System;

namespace MetricsManager.Models
{
    /// <summary>
    /// Класс модель агента
    /// </summary>
    public class AgentInfoDto : IAgentInfo
    {
        /// <summary>
        /// Идентификатор агента
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// Адрес агента
        /// </summary>
        public string AgentAddress { get; set; }

        /// <summary>
        /// Состояние агента (вкл/выкл)
        /// </summary>
        public bool Enable { get; set; }
    }
}
