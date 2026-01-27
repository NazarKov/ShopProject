using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Helpers.Command;
using ShopProject.Model.HomePage; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.HomePage
{
    internal class StartViewModel : ViewModel<StartViewModel>
    {
        private ICommand _showSettingAutoFindCommand;
        private ICommand _autofindUrlCommand;
        private ICommand _connectCommand;

        private StartModel _model;

        public StartViewModel()
        {
            _model = new StartModel();

            _showSettingAutoFindCommand = new DelegateCommand(ShowSettingAutoFind);
            _autofindUrlCommand = new DelegateCommand(AutoFindUrl);
            _connectCommand = new DelegateCommand(Connect);


            _heightGrid = 0;
            _ipRouter = string.Empty;
            _port = 0;
            _minIPAddress = 0;
            _maxIPAddress = 0;
            _url = string.Empty;

            SetFieldPage();
        }

        private void SetFieldPage()
        {
            HeightGrid = 170;
            VisibilitySettingAutoFind = Visibility.Collapsed; 
            IpRouter = "192.168.0.";
            Port = 5000;
            MinIPAddress = 1;
            MaxIPadress = 255;
        }
         
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; OnPropertyChanged(nameof(Url)); }
        }

        private string _ipRouter;
        public string IpRouter
        {
            get { return _ipRouter; }
            set { _ipRouter = value; }
        }
        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private int _minIPAddress;
        public int MinIPAddress
        {
            get { return _minIPAddress; }
            set { _minIPAddress = value; }
        }
        private int _maxIPAddress;
        public int MaxIPadress
        {
            get { return _maxIPAddress; }
            set { _maxIPAddress = value; }
        }
        private int _heightGrid;
        public int HeightGrid
        {
            get { return _heightGrid; }
            set { _heightGrid = value; OnPropertyChanged(nameof(HeightGrid)); }
        }
        private Visibility _visibilitySettingAutoFind;
        public Visibility VisibilitySettingAutoFind 
        {
            get { return _visibilitySettingAutoFind;}
            set { _visibilitySettingAutoFind = value; OnPropertyChanged(nameof(VisibilitySettingAutoFind));}
        }

        public ICommand ShowSettingAutoFindCommand => _showSettingAutoFindCommand;
        public void ShowSettingAutoFind()
        {
            if(VisibilitySettingAutoFind == Visibility.Collapsed)
            {
                HeightGrid = 370;
                VisibilitySettingAutoFind = Visibility.Visible;
            }
            else if(VisibilitySettingAutoFind == Visibility.Visible)
            {
                HeightGrid = 170;
                VisibilitySettingAutoFind = Visibility.Collapsed;
            }
        }

        public ICommand AutoFindUrlCommand => _autofindUrlCommand;
        public void AutoFindUrl() 
        {
            Task.Run(async () =>
            {
                Url = await _model.GetUrl(IpRouter, Port, MinIPAddress, MaxIPadress);

            }); 
        }

        public ICommand ConnectCommand => _connectCommand;
        public void Connect()
        { 
            string result = string.Empty;
            Task t = Task.Run(async () => { 
                result = await _model.ConnectWebServer();
            }); 
            t.ContinueWith(t =>
            {
                if (result != string.Empty)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Дата підключення: " + result, "Підключення успішно", MessageBoxButton.OK);
                        MediatorService.ExecuteEvent(NavigationButton.RedirectToAuthorizationView.ToString()); 
                    });
                }
                else
                {
                    MessageBox.Show("Невдалося підключитися");
                }
            });
        }
    }
}
