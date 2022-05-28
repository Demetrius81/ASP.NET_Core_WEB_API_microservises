using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    internal class Logic
    {        
        private TimeSpan GetFromTime(ref TimeSpan toTime) => toTime - TimeSpan.FromSeconds(60);

        private TimeSpan GetToTime() => TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        internal void ShowCpuMetrics(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = GetToTime(); // new

            TimeSpan fromTime = GetFromTime(ref toTime);

            CpuMetricCreateRequest metricsRequest = new CpuMetricCreateRequest()
            {
                AgentId = 0,
                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
            };

            try
            {
                CpuAllMetricsResponse metricsResponse =
                metricsManagerClient.GetCpuMetricsFromAgentAsync(metricsRequest).Result;

                OutputCpuMetrics(metricsResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить CPU метрики.\n{ex.Message}");
            }
        }

        internal void ShowDotNetMetrics(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = GetToTime(); // new

            TimeSpan fromTime = GetFromTime(ref toTime);

            DotNetMetricCreateRequest MetricsRequest = new DotNetMetricCreateRequest()
            {
                AgentId = 0,
                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
            };

            try
            {
                DotNetAllMetricsResponse MetricsResponse =
                metricsManagerClient.GetDotNetMetricsFromAgentAsync(MetricsRequest).Result;

                OutputDotNetMetrics(MetricsResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить Dot Net метрики.\n{ex.Message}");
            }
        }

        internal void ShowHddMetrics(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = GetToTime(); // new

            TimeSpan fromTime = GetFromTime(ref toTime);

            HddMetricCreateRequest metricsRequest = new HddMetricCreateRequest()
            {
                AgentId = 0,
                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
            };

            try
            {
                HddAllMetricsResponse metricsResponse =
                metricsManagerClient.GetHddMetricsFromAgentAsync(metricsRequest).Result;

                OutputHddMetrics(metricsResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить Hdd метрики.\n{ex.Message}");
            }
        }

        internal void ShowNetworkMetrics(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = GetToTime(); // new

            TimeSpan fromTime = GetFromTime(ref toTime);

            NetworkMetricCreateRequest metricsRequest = new NetworkMetricCreateRequest()
            {
                AgentId = 0,
                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
            };

            try
            {
                NetworkAllMetricsResponse metricsResponse =
                metricsManagerClient.GetNetworkMetricsFromAgentAsync(metricsRequest).Result;

                OutputNetworkMetrics(metricsResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить Network метрики.\n{ex.Message}");
            }
        }

        internal void ShowRamMetrics(MetricsManagerClient metricsManagerClient)
        {
            TimeSpan toTime = GetToTime(); // new

            TimeSpan fromTime = GetFromTime(ref toTime);

            RamMetricCreateRequest metricsRequest = new RamMetricCreateRequest()
            {
                AgentId = 0,
                FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
            };

            try
            {
                RamAllMetricsResponse metricsResponse =
                metricsManagerClient.GetRamMetricsFromAgentAsync(metricsRequest).Result;

                OutputRamMetrics(metricsResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при попытке получить Ram метрики.\n{ex.Message}");
            }
        }

        private void OutputCpuMetrics(CpuAllMetricsResponse metricsResponse)
        {
            foreach (CpuMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        private void OutputRamMetrics(RamAllMetricsResponse metricsResponse)
        {
            foreach (RamMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        private void OutputHddMetrics(HddAllMetricsResponse metricsResponse)
        {
            foreach (HddMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        private void OutputDotNetMetrics(DotNetAllMetricsResponse metricsResponse)
        {
            foreach (DotNetMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        private void OutputNetworkMetrics(NetworkAllMetricsResponse metricsResponse)
        {
            foreach (NetworkMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }
            Console.ReadKey(true);
            Console.Clear();
        }


    }
}
