using ShopProject.Core.Mvvm; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShopProject.ViewModel.HomePage.HomePageComponent
{
    internal class DeviceStatusViewModel :ViewModel<DeviceStatusViewModel>
    { 
        public DeviceStatusViewModel() {  }

        public Brush PrinterStatusColor { get; set; } = Brushes.LightGreen;
        public Brush ScannerStatusColor { get; set; } = Brushes.LightGreen;
        public Brush ServerStatusColor { get; set; } = Brushes.LightGreen;
        public Brush InternetStatusColor { get; set; } = Brushes.LightGreen;

        public string PrinterTooltip { get; set; } = "Принтер підключений";
        public string ScannerTooltip { get; set; } = "Сканер активний";
        public string ServerTooltip { get; set; } = "Сервер доступний";
        public string InternetTooltip { get; set; } = "Інтернет працює";
    }
}
