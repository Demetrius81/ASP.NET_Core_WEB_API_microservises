namespace MetricsManager.Models
{
    public interface IAgentInfo
    {
        int AgentId { get; set; }
        bool Enable { get; set; }
    }
}