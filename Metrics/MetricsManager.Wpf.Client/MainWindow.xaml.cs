using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AgentManager.Agents = AgentManager.ReadAgents();

            agentsList.ItemsSource = AgentManager.Agents;

            agentsList.Text = agentsList.ItemsSource.ToString();
        }

        private void agentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AgentManager.CurrentAgent = (AgentInfo)agentsList.SelectedItem;
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow window = new AddAgentWindow();

            window.Show();

            this.Close();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            AgentManager.MetricsManagerClient.RemoveAsync(AgentManager.CurrentAgent.AgentId);

            AgentManager.Agents.Remove(AgentManager.CurrentAgent);

            agentsList.SelectedItem = null;

            MessageBox.Show("Агент успешно удален");
        }

        private void Button_GetMetrics_Click(object sender, RoutedEventArgs e)
        {
            CpuChartControl cpuChartControl = new CpuChartControl();
            cpuChartControl.OnClick(cpuChartControl, new RoutedEventArgs());
        }
    }
}
