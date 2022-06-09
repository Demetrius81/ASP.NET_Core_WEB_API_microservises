using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetricsManager.Wpf.Client
{
    /// <summary>
    /// Interaction logic for CpuChartControl.xaml
    /// </summary>
    public partial class CpuChartControl : UserControl, INotifyPropertyChanged
    {
        private SeriesCollection _columnSeriesValues;

        private List<AgentInfo> _agentInfos;

        private AgentInfo _currentAgent;

        private MetricsManagerClient _metricsManagerClient;

        private string _percentText;

        private string _percentTextDescription;

        public event PropertyChangedEventHandler? PropertyChanged;

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


        public virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CpuChartControl()
        {
            InitializeComponent();

            _metricsManagerClient = new MetricsManagerClient(
              "https://localhost:44353/",
              new HttpClient());

            DataContext = this;

            _agentInfos = _metricsManagerClient.GetAsync().Result.ToList();

            _currentAgent = _agentInfos[0];  // TODO: Сделать выбор агента из списка
        }


        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => 
            {
                while (true)
                {
                    TimeSpan fromTime = GetTimePeriod(out TimeSpan toTime);

                    CpuMetricCreateRequest request = new CpuMetricCreateRequest()
                    {
                        AgentId = _currentAgent.AgentId,
                        FromTime = fromTime.ToString("dd\\.hh\\:mm\\:ss"),
                        ToTime = toTime.ToString("dd\\.hh\\:mm\\:ss")
                    };

                    try
                    {
                        CpuAllMetricsResponse metricsResponse = _metricsManagerClient.GetCpuMetricsFromAgentAsync(request).Result;

                        CpuMetric[] metrics = metricsResponse.Metrics.ToArray();

                        Dispatcher.Invoke(() =>
                        {
                            if (metricsResponse.Metrics.Count > 0)
                            {
                                TimeSpan del = TimeSpan.Parse(metrics[metrics.Count() - 1].Time) - TimeSpan.Parse(metrics[0].Time);

                                PersentTextDesciption = $"За последние {del.TotalSeconds} секунд средняя загрузка";

                                double sum = (double)metrics.Where(x => x != null).Select(x => x.Value).ToArray().Sum(x => x);

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
                    Thread.Sleep(5000);
                }
            });
        }

        private TimeSpan GetTimePeriod(out TimeSpan toTime)
        {
            toTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            return (toTime - TimeSpan.FromSeconds(60));
        }
    }
}
