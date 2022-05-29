using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    internal class UserInterface
    {
        internal void BadNumberAnswer()
        {
            Console.WriteLine();
            Console.WriteLine("Вы ввели некорректную команду.\nЧтобы продолжить нажмите любую клавишу.");
            Console.ReadKey();
        }

        internal void PressAnyKey()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey(true);
        }

        internal void AgentsMenuOutput(AgentInfo agent)
        {
            Console.Clear();
            ShowCurrentAgent(agent);
            Console.WriteLine("Меню -> Операции с агентами");
            Console.WriteLine("==============================================");
            Console.WriteLine("1 - Зарегестрировать агента");
            Console.WriteLine("2 - Вывести список всех агентов");
            Console.WriteLine("0 - Выход в меню");
            Console.WriteLine("==============================================");
            Console.Write("Введите номер задачи: ");
        }

        private void AgentToConsole(AgentInfo agent)
        {
            string str = agent.Enable ? "Агент ожидает задание" : "Агент спит";

            Console.WriteLine($"Агент #{agent.AgentId}\tURL:{agent.AgentAddress.ToString()}\n{str}");
        }

        internal void MetricsMenuOutput(AgentInfo agent)
        {
            Console.Clear();
            ShowCurrentAgent(agent);
            Console.WriteLine("Меню -> Выбор агента -> Операции с метриками");
            Console.WriteLine("==================================================");
            Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU)");
            Console.WriteLine("2 - Получить метрики за последнюю минуту (DOT NET)");
            Console.WriteLine("3 - Получить метрики за последнюю минуту (HDD)");
            Console.WriteLine("4 - Получить метрики за последнюю минуту (NETWORK)");
            Console.WriteLine("5 - Получить метрики за последнюю минуту (RAM)");
            Console.WriteLine("6 - Получить все метрики за последнюю минуту");
            Console.WriteLine("0 - Выход в меню");
            Console.WriteLine("==================================================");
            Console.Write("Введите номер задачи: ");
        }

        internal void AgentChoiceMenuOutput(AgentInfo agent)
        {
            Console.Clear();
            ShowCurrentAgent(agent);
            Console.WriteLine("Меню -> Выбор агента");
            Console.WriteLine("==================================================");
            Console.WriteLine("1 - Выбрать агента");
            Console.WriteLine("2 - Изменить статус агента");
            Console.WriteLine("3 - Просмотреть метрики с агента");
            Console.WriteLine("0 - Выход в меню");
            Console.WriteLine("==================================================");
            Console.Write("Введите номер задачи: ");

        }

        internal void AgentIsSleepOutput(AgentInfo agent)
        {
            Console.WriteLine("Невозможно вывести данные метрик от этого агента.");

            AgentToConsole(agent);
        }

        internal void MenuOutput(AgentInfo agent)
        {
            Console.Clear();
            ShowCurrentAgent(agent);
            Console.WriteLine("Меню");
            Console.WriteLine("==================================================");
            Console.WriteLine("1 - Операции с агентами");
            Console.WriteLine("2 - Выбор агента");
            Console.WriteLine("0 - Выход из программы");
            Console.WriteLine("==================================================");
            Console.Write("Введите номер задачи: ");

        }

        internal AgentInfo RequestToSelectAgent()
        {
            bool run = true;

            Uri agentAddress = new Uri("");

            int agentId = -1;

            while (run)
            {
                Console.WriteLine("Введите ID агента: ");

                if (int.TryParse(Console.ReadLine(), out agentId) && agentId > 0)
                {
                    run = false;
                }
                else
                {
                    Console.WriteLine("ID агента введен некорректно. Нажмите любую клавишу.");

                    Console.ReadKey();
                }
            }

            run = true;

            while (run)
            {
                Console.WriteLine("Введите URL агента: ");

                try
                {
                    run = false;

                    agentAddress = new Uri(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("URL агента введен некорректно. Нажмите любую клавишу.");

                    Console.ReadKey();

                    run = true;
                }
            }

            AgentInfo agent = new AgentInfo()
            {
                AgentAddress = agentAddress,
                AgentId = agentId,
                Enable = true
            };

            return agent;
        }

        internal void AgentsListOutput(List<AgentInfo> agents)
        {
            Console.Clear();

            foreach (AgentInfo agent in agents)
            {
                AgentToConsole(agent);
            }
        }

        internal void ShowCurrentAgent(AgentInfo agent)
        {
            if (agent is not null)
            {
                AgentToConsole(agent);
            }
        }
    }
}
