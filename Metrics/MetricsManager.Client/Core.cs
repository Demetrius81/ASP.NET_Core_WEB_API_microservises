using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    internal class Core
    {
        private readonly MetricsManagerClient _metricsManagerClient;

        private readonly UserInterface _uI;

        public Core(MetricsManagerClient metricsManagerClient)
        {
            _metricsManagerClient = metricsManagerClient;

            _uI = new UserInterface();
        }

        internal void CoreMenuRunning()
        {
            bool run = true;

            while (run)
            {
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

        private bool AgentsMenu(int userChoice)
        {
            switch (userChoice)
            {
                case 0:
                    return false;
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
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

        private bool AgentChoiceMenu(int userChoice)
        {
            switch (userChoice)
            {
                case 0:
                    return false;
                case 1:
                    {

                        break;
                    }
                case 2:
                    {
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

                        break;
                    }
                case 2:
                    {
                        break;
                    }
                default:
                    break;
            }
            return true;
        }

    }
}
