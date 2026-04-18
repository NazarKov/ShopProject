using Microsoft.Win32;
using ShopProject.Core.Mvvm;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Integration.Windows.WindowsService.Helper;
using ShopProject.Services.Integration.Windows.WindowsService.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.Integration.Windows.Service
{
    internal class RegisterWindowsServiceViewModel : ViewModel<RegisterWindowsServiceViewModel>
    {
        private ICommand _createWindowsService;
        private ICommand _deleteWindowsService;
        private ICommand _startWindowsService;
        private ICommand _stopWindwosService;
        private ICommand _redirectToStartPage;
        private ICommand _redirectToConnectServicePage;

        private string _pahtService = string.Empty;

        private IWindowsServiceManager _windowsServiceManager;

        public RegisterWindowsServiceViewModel(IWindowsServiceManager windowsServiceManager)
        {
            _windowsServiceManager = windowsServiceManager;

            _createWindowsService = CreateCommand(CreateService, SetError);
            _deleteWindowsService = CreateCommand(()=> { _windowsServiceManager.DeleteService(); StatusService = _windowsServiceManager.GetStatus();
                Success = "Сервіс успішно видалено";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible;
            }, SetError);
            _startWindowsService = CreateCommand(()=> { _windowsServiceManager.StartService(); StatusService = _windowsServiceManager.GetStatus();
                Success = "Сервіс успішно запущено";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible; 
            }, SetError);
            _stopWindwosService = CreateCommand(()=> { _windowsServiceManager.StopService(); StatusService = _windowsServiceManager.GetStatus();
                Success = "Сервіс успішно зупинено";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible;
            }, SetError);
            _redirectToStartPage = CreateCommand(() => { MediatorService.ExecuteNavigation(ShopProject.Model.Navigation.NavigationButton.RedirectStartPage); }, SetError);
            _redirectToConnectServicePage = CreateCommand(() => 
            {
                if (StatusService == StatusService.ContinuePending || StatusService == StatusService.Running)
                {
                    MediatorService.ExecuteNavigation(ShopProject.Model.Navigation.NavigationButton.RedirectServerSelectionPage);
                }
                else
                {
                    SetError("Сервіс не створено або незапущено");
                }
            }, SetError); 

            _error = string.Empty;
            _success = string.Empty;
            _errorTextBlockVisibiliti = Visibility.Collapsed;
            _successTextBlockVisibiliti = Visibility.Collapsed;
            _messegeBlockVisibiliti = Visibility.Collapsed;

            _statusService = _windowsServiceManager.GetStatus();
        }

        private StatusService _statusService;
        public StatusService StatusService
        {
            get { return _statusService; }
            set { _statusService = value; OnPropertyChanged(nameof(StatusService)); }
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


        public ICommand CreateWindowsService => _createWindowsService;
        private void CreateService()
        {
            OpenFileDialog dialogWindow = new OpenFileDialog(); 
            var result = dialogWindow.ShowDialog();
            if (result.HasValue)
            {
                _pahtService = dialogWindow.FileName;
                var process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = dialogWindow.FileName,
                    WorkingDirectory = Path.GetDirectoryName(dialogWindow.FileName),
                    UseShellExecute = false,
                    CreateNoWindow = true
                }; 
                process.EnableRaisingEvents = true;
                process.Exited += Process_Exited;
                process.Start();
                
                Task.Delay(1500).ContinueWith(_ =>
                {
                    OpenBrowser("http://localhost:5000");
                }); 
            } 
        }
        private void OpenBrowser(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void Process_Exited(object sender, EventArgs e)
        { 
            App.Current.Dispatcher.Invoke(() =>
            {
                _windowsServiceManager.CreateService(_pahtService, _pahtService.Split('\\').Last());
                StatusService = _windowsServiceManager.GetStatus();

                Success = "Сервіс успішно сторено";
                SuccessTextBlockVisibiliti = Visibility.Visible;
                ErrorTextBlockVisibiliti = Visibility.Collapsed;
                MessegeBlockVisibiliti = Visibility.Visible;
            });
        }

        public ICommand DeleteWindowsService => _deleteWindowsService;
        public ICommand StartWindowsService => _startWindowsService;
        public ICommand StopWinodwsService => _stopWindwosService;
        public ICommand RedirectToStartPage=> _redirectToStartPage;
        public ICommand RedirectToConnectServicePage => _redirectToConnectServicePage;

        private void SetError(string error)
        {
            Error = error;
            SuccessTextBlockVisibiliti = Visibility.Collapsed;
            ErrorTextBlockVisibiliti = Visibility.Visible;
            MessegeBlockVisibiliti = Visibility.Visible;
        }
    }
}
