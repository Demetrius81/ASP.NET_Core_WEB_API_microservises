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
using System.Windows.Media.Animation;

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

            DoubleAnimation animation = new DoubleAnimation();

            animation.From = 0;

            animation.To = 520;

            animation.Duration = TimeSpan.FromSeconds(2);

            buttonGet.BeginAnimation(Button.WidthProperty, animation);

            buttonDelete.BeginAnimation(Button.WidthProperty, animation);

            buttonAdd.BeginAnimation(Button.WidthProperty, animation);
        }

        /// <summary>
        /// Метод - обработчик события изменения текущего агента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AgentManager.CurrentAgent = (AgentInfo)agentsList.SelectedItem;
        }

        /// <summary>
        /// Метод - обработчик события нажатия на кнопку "добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddAgentWindow window = new AddAgentWindow();

            window.Show();

            this.Close();
        }

        /// <summary>
        /// Метод - обработчик события нажатия на кнопку "удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AgentManager.CurrentAgent == null)
            {
                MessageBox.Show("Агент не выбран");
            }
            else
            {
                AgentManager.MetricsManagerClient.RemoveAsync(AgentManager.CurrentAgent.AgentId);

                AgentManager.Agents.Remove(AgentManager.CurrentAgent);

                agentsList.SelectedItem = null;

                MessageBox.Show("Агент успешно удален");
            }
        }

        /// <summary>
        /// Метод - обработчик события нажатия на кнопку "получить метрики"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_GetMetrics_Click(object sender, RoutedEventArgs e)
        {
            cpuChartControl.OnClick();
            dotNetChartControl.OnClick();
            hddChartControl.OnClick();
            networkChartControl.OnClick();
            ramChartControl.OnClick();
        }
    }
}
