using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MetricsManager.Wpf.Client
{
    /// <summary>
    /// Interaction logic for NetworkChartControl.xaml
    /// </summary>
    public partial class NetworkChartControl : UserControl, INotifyPropertyChanged, ICpuChartControl
    {
        private SeriesCollection _columnSeriesValues;

        private MetricsManagerClient _metricsManagerClient;

        private string _percentText;

        private string _percentTextDescription;

        private int _maxValue;

        object locker = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;

                OnPropertyChanged("MaxValue");
            }
        }

        /// <summary>
        /// Свойство "ColumnSeriesValues"
        /// </summary>
        public SeriesCollection ColumnSeriesValues
        {
            get
            {
                return _columnSeriesValues;
            }
            set
            {
                _columnSeriesValues = value;

                OnPropertyChanged("ColumnSeriesValues");
            }
        }

        /// <summary>
        /// Свойство "PersentText"
        /// </summary>
        public string PersentText
        {
            get
            {
                return _percentText;
            }
            set
            {
                _percentText = value;
                OnPropertyChanged("PersentText");
            }
        }

        /// <summary>
        /// Свойство "PersentTextDesciption"
        /// </summary>
        public string PersentTextDesciption
        {
            get
            {
                return _percentTextDescription;
            }
            set
            {
                _percentTextDescription = value;
                OnPropertyChanged("PersentTextDesciption");
            }
        }

        /// <summary>
        /// Реализация метода подписки на событие изменения свойства
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Конструктор класса с параметрами по умолчанию
        /// </summary>
        public NetworkChartControl()
        {
            InitializeComponent();

            _metricsManagerClient = AgentManager.MetricsManagerClient;

            DataContext = this;
        }

        public void OnClick()
        {
            UpdateOnСlick(null, new RoutedEventArgs());
        }

        /// <summary>
        /// Метод, который запускается реагируя на событие нажатия кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

                    NetworkMetricCreateRequest request = new NetworkMetricCreateRequest();

                    lock (locker)
                    {
                        AgentManager.Agents = AgentManager.ReadAgents();

                        AgentManager.CurrentAgent = AgentManager.Agents[0];

                        request.AgentId = AgentManager.CurrentAgent.AgentId;

                        request.FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss");

                        request.ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss");
                    }

                    try
                    {
                        NetworkAllMetricsResponse metricsResponse = _metricsManagerClient.GetNetworkMetricsFromAgentAsync(request).Result;

                        NetworkMetric[] metrics = metricsResponse.Metrics.ToArray();

                        Dispatcher.Invoke(() =>
                        {
                            if (metricsResponse.Metrics.Count > 0)
                            {
                                TimeSpan del = TimeSpan.Parse(metrics[metrics.Count() - 1].Time) - TimeSpan.Parse(metrics[0].Time);

                                PersentTextDesciption = $"За последние {del.TotalSeconds} секунд количество установленных соединений";

                                double sum = (double)metrics.Where(x => x != null).Select(x => x.Value).ToArray().Sum(x => x);

                                MaxValue = metricsResponse.Metrics.Max(x => x.Value);

                                PersentText = $"{sum / metrics.Count():F2}";
                            }

                            ColumnSeriesValues = new SeriesCollection
                            {
                                new ColumnSeries
                                {
                                    Values = new ChartValues<int>(metrics.Where(x => x != null).Select(x => x.Value).ToArray())
                                }
                            };
                            TimePowerChart.Update(true);
                        });

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        /// <summary>
        /// Метод вычисления периода времени
        /// </summary>
        /// <param name="toTime"></param>
        /// <returns></returns>
        private TimeSpan GetTimePeriod(out TimeSpan toTime)
        {
            toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            return (toTime - TimeSpan.FromSeconds(60));
        }
    }
}
