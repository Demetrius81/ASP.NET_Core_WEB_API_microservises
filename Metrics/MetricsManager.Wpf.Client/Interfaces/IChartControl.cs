using System.Windows;

namespace MetricsManager.Wpf.Client
{
    public interface ICpuChartControl
    {
        /// <summary>
        /// Инкапсуляция метода обработчика события нвжатия на кнопку обновления
        /// </summary>
        void OnClick();
    }
}