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
        private TimeSpan GetTimePeriod(out TimeSpan toTime)
        {
            toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

             return (toTime - TimeSpan.FromSeconds(60));
        }


               
        internal void ShowCpuMetrics(MetricsManagerClient metricsManagerClient, AgentInfo agent, UserInterface uI)
        {
            if (agent.Enable)
            {
                TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

                CpuMetricCreateRequest metricsRequest = new CpuMetricCreateRequest()
                {
                    AgentId = agent.AgentId,
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
            else
            {
                uI.AgentIsSleepOutput(agent);
            }

        }

        internal void ShowDotNetMetrics(MetricsManagerClient metricsManagerClient, AgentInfo agent, UserInterface uI)
        {
            TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

            DotNetMetricCreateRequest MetricsRequest = new DotNetMetricCreateRequest()
            {
                AgentId = agent.AgentId,
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

        internal void ShowHddMetrics(MetricsManagerClient metricsManagerClient, AgentInfo agent, UserInterface uI)
        {
            TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

            HddMetricCreateRequest metricsRequest = new HddMetricCreateRequest()
            {
                AgentId = agent.AgentId,
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

        internal void ShowNetworkMetrics(MetricsManagerClient metricsManagerClient, AgentInfo agent, UserInterface uI)
        {
            TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

            NetworkMetricCreateRequest metricsRequest = new NetworkMetricCreateRequest()
            {
                AgentId = agent.AgentId,
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

        internal void ShowRamMetrics(MetricsManagerClient metricsManagerClient, AgentInfo agent, UserInterface uI)
        {
            TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

            RamMetricCreateRequest metricsRequest = new RamMetricCreateRequest()
            {
                AgentId = agent.AgentId,
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
        }

        private void OutputRamMetrics(RamAllMetricsResponse metricsResponse)
        {
            foreach (RamMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }            
        }

        private void OutputHddMetrics(HddAllMetricsResponse metricsResponse)
        {
            foreach (HddMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }            
        }

        private void OutputDotNetMetrics(DotNetAllMetricsResponse metricsResponse)
        {
            foreach (DotNetMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }            
        }

        private void OutputNetworkMetrics(NetworkAllMetricsResponse metricsResponse)
        {
            foreach (NetworkMetric metric in metricsResponse.Metrics)
            {
                Console.WriteLine(
                    $"{TimeSpan.Parse(metric.Time).ToString("dd\\.hh\\:mm\\:ss")} > {metric.Value}");
            }           
        }


    }
}
