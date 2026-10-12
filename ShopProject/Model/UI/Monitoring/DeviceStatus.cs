using ShopProject.Infrastructure.Mvvm; 
using System.Windows.Media;

namespace ShopProject.Model.UI.Monitoring
{
    internal class DeviceStatus : Model<DeviceStatus>
    {
        private Brush _serverStatusColor = Brushes.IndianRed;
        public Brush ServerStatusColor { get { return _serverStatusColor; } set { _serverStatusColor = value; OnPropertyChanged(nameof(ServerStatusColor)); } }

        private Brush _dataBaseStatusColor = Brushes.IndianRed;
        public Brush DataBaseStatusColor { get { return _dataBaseStatusColor; } set { _dataBaseStatusColor = value; OnPropertyChanged(nameof(DataBaseStatusColor)); } }
        private Brush _internetStatusColor = Brushes.IndianRed;
        public Brush InternetStatusColor { get { return _internetStatusColor; } set { _internetStatusColor = value; OnPropertyChanged(nameof(InternetStatusColor)); } }

        public string ServerTooltip { get; set; } = "Сервер доступний";
        public string InternetTooltip { get; set; } = "Інтернет працює";
        public string DataBaseTooltip { get; set; } = "База даних підключена";
        public bool ServerAvailable;
        public bool DataBaseAvailable;
        public bool InternetAvailable;

        public void ChangeStatus(bool serverStatus , bool databaseStatus,bool internetStatus)
        {
            ServerAvailable = serverStatus;
            DataBaseAvailable = databaseStatus;
            InternetAvailable = internetStatus;
            if (serverStatus)
            {
                ServerStatusColor = Brushes.LightGreen;
            }
            else
            {
                ServerStatusColor = Brushes.IndianRed;
            }
            if (databaseStatus) 
            {
                DataBaseStatusColor = Brushes.LightGreen;
            }
            else
            {
                DataBaseStatusColor = Brushes.IndianRed;
            }
            if (internetStatus)
            {
                InternetStatusColor = Brushes.LightGreen;
            }
            else
            {
                InternetStatusColor= Brushes.IndianRed;
            }

        }

    }
}
