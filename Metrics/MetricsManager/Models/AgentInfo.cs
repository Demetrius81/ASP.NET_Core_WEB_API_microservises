using System;

namespace MetricsManager.Models
{
    /// <summary>
    /// Класс модель агента
    /// </summary>
    public class AgentInfo : IAgentInfo
    {
        /// <summary>
        /// Идентификатор агента
        /// </summary>
        public int AgentId { get; set; }

        /// <summary>
        /// Адрес агента
        /// </summary>
        public Uri AgentAddress { get; set; }

        /// <summary>
        /// Состояние агента (вкл/выкл)
        /// </summary>
        public bool Enable { get; set; }

        public override string ToString()
        {
            if (Enable)
            {
                return $"Агент {AgentId} URL{AgentAddress} активен";
            }
            else
            {
                return $"Агент {AgentId} URL{AgentAddress} спит";
            }
        }
    }
}
