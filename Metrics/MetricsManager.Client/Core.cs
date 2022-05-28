using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    internal class Core
    {
        private readonly MetricsManagerClient _metricsManagerClient;

        private readonly UserInterface _uI;

        private readonly Logic _logic;

        private readonly List<AgentInfo> _agentInfos;

        private AgentInfo _currentAgent;

        public Core()
        {
            _metricsManagerClient = new MetricsManagerClient(
              $"https://localhost:44353/",
              new HttpClient());

            _uI = new UserInterface();

            _logic = new Logic();

            //_agentInfos = _metricsManagerClient.GetAsync().Result.ToList();

            _agentInfos = new List<AgentInfo>()
            {
                new AgentInfo()
                {
                    AgentId = 0,
                    AgentAddress = new Uri("https://localhost:44325"),
                    Enable = true
                },
                new AgentInfo()
                {
                    AgentId = 1,
                    AgentAddress = new Uri("https://localhost:44339"),
                    Enable = true
                },
                new AgentInfo()
                {
                    AgentId = 2,
                    AgentAddress = new Uri("https://localhost:44335"),
                    Enable = true
                }
            };
        }

        internal void CoreMenuRunning()
        {
            bool run = true;

            while (run)
            {
                _uI.ShowCurrentAgent(_currentAgent);
                _uI.MenuOutput();

                if (int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    run = Menu(userChoice);
                }
                else
                {
                    _uI.BadNumberAnswer();
                }

            }
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        /// <param name="userChoice"></param>
        /// <returns></returns>
        private bool Menu(int userChoice)
        {
            switch (userChoice)
            {
                case 0:
                    return false;
                case 1:
                    {
                        CoreAgentsMenu();
                        break;
                    }
                case 2:
                    {
                        CoreAgentChoiceMenu();
                        break;
                    }
                default:
                    break;
            }
            return true;
        }


        private void CoreAgentsMenu()
        {
            bool run = true;

            while (run)
            {
                _uI.ShowCurrentAgent(_currentAgent);
                _uI.AgentsMenuOutput();

                if (int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    run = AgentsMenu(userChoice);
                }
                else
                {
                    _uI.BadNumberAnswer();
                }
            }
        }

        /// <summary>
        /// Подменю управления агентами
        /// </summary>
        /// <param name="userChoice"></param>
        /// <returns></returns>
        private bool AgentsMenu(int userChoice)
        {
            switch (userChoice)
            {
                case 0:
                    return false;
                case 1:
                    {                        
                        _metricsManagerClient.RegisterAsync(_uI.RequestToSelectAgent());
                        break;
                    }
                case 2:
                    {                        
                        _uI.AgentsListOutput(_agentInfos);
                        break;
                    }
                default:
                    break;
            }
            return true;
        }


        private void CoreAgentChoiceMenu()
        {
            bool run = true;

            while (run)
            {
                _uI.ShowCurrentAgent(_currentAgent);
                _uI.AgentChoiceMenuOutput();

                if (int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    run = AgentChoiceMenu(userChoice);
                }
                else
                {
                    _uI.BadNumberAnswer();
                }
            }
        }

        /// <summary>
        /// Меню выбора агента
        /// </summary>
        /// <param name="userChoice"></param>
        /// <returns></returns>
        private bool AgentChoiceMenu(int userChoice)
        {
            switch (userChoice)
            {
                case 0:
                    return false;
                case 1:
                    {
                        SelectAgent();

                        break;
                    }
                case 2:
                    {
                        _metricsManagerClient.SwitchAsync(_currentAgent.AgentId);
                        break;
                    }
                case 3:
                    {
                        CoreMetricsMenu();
                        break;
                    }
                default:
                    break;
            }
            return true;
        }

        private void CoreMetricsMenu()
        {
            bool run = true;

            while (run)
            {
                _uI.ShowCurrentAgent(_currentAgent);
                _uI.MetricsMenuOutput();

                if (int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    run = MetricsMenu(userChoice);
                }
                else
                {
                    _uI.BadNumberAnswer();
                }
            }
        }

        private bool MetricsMenu(int userChoice)
        {
            switch (userChoice)
            {
                case 0:
                    return false;
                case 1:
                    {
                        _logic.ShowCpuMetrics(_metricsManagerClient);
                        _uI.PressAnyKey();
                        break;
                    }
                case 2:
                    {
                        _logic.ShowDotNetMetrics(_metricsManagerClient);
                        _uI.PressAnyKey();
                        break;
                    }
                case 3:
                    {
                        _logic.ShowHddMetrics(_metricsManagerClient);
                        _uI.PressAnyKey();
                        break;
                    }
                case 4:
                    {
                        _logic.ShowNetworkMetrics(_metricsManagerClient);
                        _uI.PressAnyKey();
                        break;
                    }
                case 5:
                    {
                        _logic.ShowRamMetrics(_metricsManagerClient);
                        _uI.PressAnyKey();
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Cpu метрики:");
                        _logic.ShowCpuMetrics(_metricsManagerClient);
                        Console.WriteLine("DotNet метрики:");
                        _logic.ShowDotNetMetrics(_metricsManagerClient);
                        Console.WriteLine("Hdd метрики:");
                        _logic.ShowHddMetrics(_metricsManagerClient);
                        Console.WriteLine("Network метрики:");
                        _logic.ShowNetworkMetrics(_metricsManagerClient);
                        Console.WriteLine("Ram метрики:");
                        _logic.ShowRamMetrics(_metricsManagerClient);
                        _uI.PressAnyKey();
                        break;
                    }
                default:
                    break;
            }
            return true;
        }

        private void SelectAgent()
        {
            _uI.AgentsListOutput(_agentInfos);

            bool run = true;

            int agentId = -1;

            while (run)
            {
                Console.WriteLine("Введите ID агента из списка: ");

                if (int.TryParse(Console.ReadLine(), out agentId) && agentId > 0))
                {
                    run = false;


                }
                else
                {
                    Console.WriteLine("ID агента введен некорректно. Нажмите любую клавишу.");

                    Console.ReadKey();
                }
            }
        }

    }
}
