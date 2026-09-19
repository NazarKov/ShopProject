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
        private Brush _serverStatusColor = Brushes.IndianRed;
        public Brush ServerStatusColor { get { return _serverStatusColor; } set { _serverStatusColor = value;OnPropertyChanged(nameof(ServerStatusColor)); } }

        private Brush _dataBaseStatusColor = Brushes.IndianRed;
        public Brush DataBaseStatusColor { get { return _dataBaseStatusColor; } set { _dataBaseStatusColor = value; OnPropertyChanged(nameof(DataBaseStatusColor)); } }
        private Brush _internetStatusColor = Brushes.IndianRed;
        public Brush InternetStatusColor { get { return _internetStatusColor; } set { _internetStatusColor = value; OnPropertyChanged(nameof(InternetStatusColor)); } }
         
        public string ServerTooltip { get; set; } = "Сервер доступний";
        public string InternetTooltip { get; set; } = "Інтернет працює";
        public string DataBaseTooltip { get; set; } = "База даних підключена";
    }
}
