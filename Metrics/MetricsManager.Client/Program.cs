using System;
using System.Net.Http;

namespace MetricsManager.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MetricsManagerClient metricsManagerClient = new MetricsManagerClient("https://localhost:44325",
               new HttpClient());

            




            while (true)
            {
                Console.WriteLine("Задачи");
                Console.WriteLine("==============================================");
                Console.WriteLine("1 - Получить метрики за последнюю минуту (CPU)");
                Console.WriteLine("0 - Завершение работы приложения");
                Console.WriteLine("==============================================");
                Console.Write("Введите номер задачи: ");
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
