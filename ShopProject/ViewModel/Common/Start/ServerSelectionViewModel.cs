using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Navigation;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.NetworkUrlScanner.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Start
{
    internal class ServerSelectionViewModel : ViewModel<ServerSelectionViewModel>, IViewModelLoadResourse
    {
        private ICommand _showSettingAutoFindCommand;
        private ICommand _autoFindUrlCommand;
        private ICommand _connectCommand;
        private ICommand _connectOldUrlCommand;
        private ICommand _redirectStartPageCommad;

        private INetworkUrlManagerService _networkUrlManagerService;

        public ServerSelectionViewModel(INetworkUrlManagerService networkUrlManagerService)
        {
            _networkUrlManagerService = networkUrlManagerService; 

            _showSettingAutoFindCommand = CreateCommand(ShowSettingAutoFind);
            _autoFindUrlCommand = CreateCommandAsync(AutoFindUrl,SetError);
            _connectCommand = CreateCommandAsync(Connect,SetError);
            _connectOldUrlCommand = CreateCommandParameterAsync<object>(ConnectOldUrl,SetError);
            _redirectStartPageCommad = CreateCommand(() => { MediatorService.ExecuteNavigation(NavigationButton.RedirectStartPage); });
             
            _heightGrid = 0;
            _ipRouter = string.Empty;
            _port = 0;
            _minIPAddress = 0;
            _maxIPAddress = 0;
            _url = string.Empty; 
            _oldUrls = new List<string>();
            _nameServer = string.Empty;

            _error = string.Empty;
            _success = string.Empty;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            _messegeBlockVisibiliti = Visibility.Collapsed;
        }

        public Task LoadResourse()
        {
            SafeExecute(SetFieldPage);

            return Task.CompletedTask;
        }

        private void SetFieldPage()
        {
            HeightGrid = 200;
            VisibilitySettingAutoFind = Visibility.Collapsed;
            MessegeBlockVisibiliti = Visibility.Collapsed;
            VisibilityOldUrl = Visibility.Visible;
            IpRouter = "192.168.0.";
            Port = 5000;
            MinIPAddress = 1;
            MaxIPadress = 255; 
            NameServer = string.Empty;

            if(OldUrls.Count == 0)
            {
                var oldUrls = _networkUrlManagerService.GetSettingUrl();
                foreach(var url in oldUrls.Servers)
                {
                    OldUrls.Add(url.NameServer+" : "+url.UrlServer);
                }
            } 

            if (OldUrls.Count == 0)
            {
                VisibilityOldUrl = Visibility.Collapsed;
            }
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

        private string _nameServer;
        public string NameServer
        {
            get { return _nameServer; }
            set { _nameServer = value; OnPropertyChanged(nameof(NameServer));   }
        } 

        private Visibility _visibilitySettingAutoFind;
        public Visibility VisibilitySettingAutoFind
        {
            get { return _visibilitySettingAutoFind; }
            set { _visibilitySettingAutoFind = value; OnPropertyChanged(nameof(VisibilitySettingAutoFind)); }
        }

        private List<string> _oldUrls;
        public List<string> OldUrls
        {
            get { return _oldUrls; }
            set { _oldUrls = value; OnPropertyChanged(nameof(OldUrls)); }
        }

        private Visibility _visibilityOldUrl;
        public Visibility VisibilityOldUrl
        {
            get { return _visibilityOldUrl; }
            set { _visibilityOldUrl = value; OnPropertyChanged(nameof(VisibilityOldUrl)); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }

        private string _success;
        public string Success
        {
            get { return _success; }
            set { _success = value; OnPropertyChanged(nameof(Success)); }
        }

        private Visibility _successTextBlockVisibiliti;
        public Visibility SuccessTextBlockVisibiliti
        {
            get { return _successTextBlockVisibiliti; }
            set { _successTextBlockVisibiliti = value; OnPropertyChanged(nameof(SuccessTextBlockVisibiliti)); }
        }

        private Visibility _errorTextBlockVisibiliti;
        public Visibility ErrorTextBlockVisibiliti
        {
            get { return _errorTextBlockVisibiliti; }
            set { _errorTextBlockVisibiliti = value; OnPropertyChanged(nameof(ErrorTextBlockVisibiliti)); }
        }

        private Visibility _messegeBlockVisibiliti;
        public Visibility MessegeBlockVisibiliti
        {
            get { return _messegeBlockVisibiliti; }
            set { _messegeBlockVisibiliti = value; OnPropertyChanged(nameof(MessegeBlockVisibiliti)); }
        }

        public ICommand ShowSettingAutoFindCommand => _showSettingAutoFindCommand;
        public void ShowSettingAutoFind()
        {
            if (VisibilitySettingAutoFind == Visibility.Collapsed)
            {
                HeightGrid = 400;
                VisibilitySettingAutoFind = Visibility.Visible;
                VisibilityOldUrl = Visibility.Collapsed;
            }
            else if (VisibilitySettingAutoFind == Visibility.Visible)
            {
                HeightGrid = 200;
                VisibilitySettingAutoFind = Visibility.Collapsed;
                VisibilityOldUrl = Visibility.Visible;
            }
        }

        public ICommand AutoFindUrlCommand => _autoFindUrlCommand;
        public async Task AutoFindUrl()
        {
            var result = await _networkUrlManagerService.Search(IpRouter, Port, MinIPAddress, MaxIPadress);
            if(result != string.Empty)
            { 
                Url = result;
            }
        }

        public ICommand ConnectCommand => _connectCommand;
        public async Task Connect()
        {  
            if (await _networkUrlManagerService.Ping(Url, NameServer))
            {
                Success = "Підключення успішно";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible;
                MediatorService.ExecuteNavigation(NavigationButton.RedirectToAuthorizationPage);
            }
            else
            {
                SetError("Невдалося підключитися"); 
            }
        }

        public ICommand ConnectOldUrlCommand => _connectOldUrlCommand;
        public async Task ConnectOldUrl(object parameter)
        {
            var temp = (string)parameter; 
            var tempsplit = temp.Split(" : "); 
            string result = string.Empty; 
            if (await _networkUrlManagerService.Ping(tempsplit[1], tempsplit[0], false))
            {
                Success = "Підключення успішно";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible;
                MediatorService.ExecuteNavigation(NavigationButton.RedirectToAuthorizationPage);
            }
            else
            {
                SetError("Невдалося підключитися");
            }

        }

        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
            MessegeBlockVisibiliti = Visibility.Visible;
        }

        public ICommand RediredtStartCommand => _redirectStartPageCommad;
    }
}
