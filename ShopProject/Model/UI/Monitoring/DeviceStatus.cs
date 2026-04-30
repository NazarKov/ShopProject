using ShopProject.Core.Mvvm;
using ShopProject.Model.UI.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShopProject.Model.UI.Monitoring
{
    internal class DeviceStatus : Model<DeviceStatus>
    {
       
        public Brush PrinterStatusColor { get; set; } = Brushes.LightGreen;
        public Brush ScannerStatusColor { get; set; } = Brushes.LightGreen;

        private Brush _serverStatusColor = Brushes.IndianRed;
        public Brush ServerStatusColor { get { return _serverStatusColor; } set { _serverStatusColor = value;OnPropertyChanged(nameof(ServerStatusColor)); } }

        private Brush _internetStatusColor = Brushes.IndianRed;
        public Brush DataBaseStatusColor { get { return _internetStatusColor; } set { _internetStatusColor = value; OnPropertyChanged(nameof(DataBaseStatusColor)); } }

        public string PrinterTooltip { get; set; } = "Принтер підключений";
        public string ScannerTooltip { get; set; } = "Сканер активний";
        public string ServerTooltip { get; set; } = "Сервер доступний";
        public string InternetTooltip { get; set; } = "Інтернет працює";
    }
}
