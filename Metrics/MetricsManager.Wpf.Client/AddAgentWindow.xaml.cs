using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MetricsManager.Wpf.Client
{
    /// <summary>
    /// Interaction logic for AddAgentWindow.xaml
    /// </summary>
    public partial class AddAgentWindow : Window
    {
        public AddAgentWindow()
        {
            InitializeComponent();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            AgentInfo agentInfo = new AgentInfo();

            int id;

            string agentStringId = textBoxId.Text.Trim();

            string agentStringUri = textBoxUri.Text.Trim();

            bool agentStatus = checkBoxStatus.IsChecked == null ? true : checkBoxStatus.IsChecked.Value;
            if (agentStringId == "")
            {
                textBoxId.ToolTip = "Поле ID не может быть пустым";

                textBoxId.Background = Brushes.Yellow;

                MessageBox.Show("Это поле не может быть пустым");

                return;
            }
            else if (!int.TryParse(agentStringId, out id) || id <= 0)
            {
                textBoxId.ToolTip = "Это поле введено некорректно";

                textBoxId.Background = Brushes.Yellow;

                MessageBox.Show("Это поле введено некорректно");

                return;
            }
            else
            {
                foreach (var agent in AgentManager.Agents)
                {
                    if (agent.AgentId == id)
                    {
                        textBoxId.ToolTip = "Агени с таким ID уже существует";

                        textBoxId.Background = Brushes.Yellow;

                        MessageBox.Show("Агени с таким ID уже существует");

                        return;
                    }
                }
            }
            textBoxId.Background = Brushes.Transparent;

            textBoxId.ToolTip = "Поле ID введено корректно и значение принято";

            if (agentStringUri == "")
            {
                textBoxUri.ToolTip = "Поле URL не может быть пустым";

                textBoxUri.Background = Brushes.Yellow;

                MessageBox.Show("Это поле не может быть пустым");

                return;
            }
            else
            {
                try
                {
                    agentInfo.AgentAddress = new Uri(agentStringUri);
                }
                catch (Exception)
                {
                    textBoxUri.ToolTip = "Это поле введено некорректно";

                    textBoxUri.Background = Brushes.Yellow;

                    MessageBox.Show("Это поле введено некорректно");

                    return;
                }
            }
                       
            textBoxUri.Background = Brushes.Transparent;

            textBoxId.ToolTip = "Поле URL введено корректно и значение принято";

            textBoxId.Text = "";

            textBoxUri.Text = "";

            agentInfo.AgentId = id;

            agentInfo.Enable = agentStatus;

            AgentManager.RegisterAgent(agentInfo);

            AgentManager.Agents = AgentManager.ReadAgents();

            MessageBox.Show("Агент успешно добавлен");
        }

        private void Button_Canccel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();

            window.Show();

            this.Close();
        }
    }
}
