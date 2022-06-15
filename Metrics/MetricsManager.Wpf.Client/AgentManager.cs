using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        private static ObservableCollection<AgentInfo> _agents = new ObservableCollection<AgentInfo>();

        public static ObservableCollection<AgentInfo> Agents
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

        public static ObservableCollection<AgentInfo> ReadAgents()
        {
            var agents = _metricsManagerClient.GetAsync().Result;

            ObservableCollection<AgentInfo> agentCollections = new ObservableCollection<AgentInfo>();

            foreach (var agent in agents)
            {
                agentCollections.Add(agent);
            }
            return agentCollections;
        }
    }
}
