using System;
using System.Net.Http;

namespace MetricsManager.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MetricsManagerClient metricsManagerClient = new MetricsManagerClient(
               "https://localhost:44325",
               new HttpClient());
            UserInterface uI = new UserInterface();

            // 1. подпрограмма работы с агентами
            //      1. зарегестрировать агента
            //      2. список агентов

            // 2. запустить подпрограмму работы с метриками
            //      1. выбрать агента
            //      2. изменить статус агента
            //      3. выбрать какие метрики вывести
            //      4. выбрать все метрики




            Core core = new Core(metricsManagerClient);

            core.CoreMenuRunning();



            Console.WriteLine("Агенты");
            Console.WriteLine("==============================================");
            
            Console.WriteLine("==============================================");
            Console.Write("Введите номер задачи: ");




            Console.WriteLine("Задачи");
            Console.WriteLine("==============================================");
            Console.WriteLine("1 - Выбрать агента");
            Console.WriteLine("2 - Изменить статус агента");            
            Console.WriteLine("0 - Выход в меню");
            Console.WriteLine("==============================================");
            Console.Write("Введите номер задачи: ");





            Console.WriteLine("Задачи");
            Console.WriteLine("==============================================");
            Console.WriteLine("1 - Зарегестрировать агента");
            Console.WriteLine("2 - Вывести список всех агентов");
            Console.WriteLine("0 - Выход в меню");
            Console.WriteLine("==============================================");
            Console.Write("Введите номер задачи: ");

            while (true)
            {
                //uI.MetricsMenu();

                if (int.TryParse(Console.ReadLine(), out int taskNumber))
                {
                    switch (taskNumber)
                    {
                        case 0:
                            Console.WriteLine("Завершение работы приложения.");
                            return;
                        case 1:
                            TimeSpan toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()); // new
                            TimeSpan fromTime = toTime - TimeSpan.FromSeconds(60);

                            CpuMetricCreateRequest cpuMetricsRequest = new CpuMetricCreateRequest()
                            {
                                AgentId = 0,
                                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
                            };

                            try
                            {
                                CpuAllMetricsResponse cpuMetricsResponse =
                                metricsManagerClient.GetCpuMetricsFromAgentAsync(cpuMetricsRequest).Result;

                                foreach (CpuMetric cpuMetric in cpuMetricsResponse.Metrics)
                                {
                                    Console.WriteLine(
                                        $"{TimeSpan.Parse(cpuMetric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {cpuMetric.Value}");
                                }
                                Console.ReadKey(true);
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Произошла ошибка при попытке получить CPU метрики.\n{ex.Message}");
                            }

                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Номер задачи введен некорректно.\nПожалуйста, повторите ввод данных.");
                }

            }


        }
    }
}
