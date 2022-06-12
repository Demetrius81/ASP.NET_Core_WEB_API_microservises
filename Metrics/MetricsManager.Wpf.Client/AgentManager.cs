using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Wpf.Client
{
    internal static class AgentManager
    {
        private static MetricsManagerClient _metricsManagerClient = new MetricsManagerClient(
              "https://localhost:44353/",
              new HttpClient());

        private static AgentInfo _currentAgent;

        public static AgentInfo CurrentAgent { get => _currentAgent; set => _currentAgent = value; }


        private static List<AgentInfo> _agents = new List<AgentInfo>();

        public static List<AgentInfo> Agents
        { 
            get
            {
                return _agents;
            }
            set
            {
                _agents = value;
            }
        }

        public static MetricsManagerClient MetricsManagerClient { get => _metricsManagerClient; set => _metricsManagerClient = value; }

        public static void RegisterAgent(AgentInfo agent)
        {
            MetricsManagerClient.RegisterAsync(agent);
        }

        public static List<AgentInfo> ReadAgents()
        {
            return _agents = MetricsManagerClient.GetAsync().Result.ToList();
        }
    }
}
