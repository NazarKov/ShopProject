using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Exceptions;
using ShopProject.Model.Navigation;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.View.AdminPage.Dashboard;
using ShopProject.View.AdminPage.Storage;
using ShopProject.View.Authorization;
using ShopProject.View.Common.Setting;
using ShopProject.View.Common.Start;
using ShopProject.View.GiftCertificatesPage;
using ShopProject.View.HomePage.HomePageComponent;
using ShopProject.View.Integration.Printing;
using ShopProject.View.Integration.Windows.Service;
using ShopProject.View.StatisticsPage;
using ShopProject.View.StoragePage;
using ShopProject.View.StoragePage.ExcelPage.ExportExcelPage;
using ShopProject.View.StoragePage.ExcelPage.ImportExcelPage;
using ShopProject.View.TemplatePage;
using ShopProject.View.ToolsPage;
using ShopProject.View.UserPage.SaleMenu;
using ShopProject.ViewModel.AdminPage.Dashboard;
using ShopProject.ViewModel.Authorization;
using ShopProject.ViewModel.Common.Setting;
using ShopProject.ViewModel.Common.Start;
using ShopProject.ViewModel.HomePage.HomePageComponent;
using ShopProject.ViewModel.Integration.Printing;
using ShopProject.ViewModel.Integration.Windows.Service;
using ShopProject.ViewModel.StoragePage;
using ShopProject.ViewModel.UserPage;
using ShopProject.ViewModel.UserPage.SaleMenu;
using ShopProject.Views.AdminPage;
using ShopProject.Views.UserPage;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Home
{
    internal class MainViewModel : ViewModel<MainViewModel> , IViewModelLoadResourse
    {
        private ICommand _exitAppCommand;
        private ICommand _openSettingCommand;
        private ICommand _openStorageCommand;
        private ICommand _openExportProductCommand;
        private ICommand _openImportProductCommand;
        private ICommand _openCreateStikerCommand;
        private ICommand _openSaleMenuCommand;
        private ICommand _openDeliveryOfGoodsCommand;
        private ICommand _openUsersPageCommand;
        private ICommand _openObjectOwnerPageCommand;
        private ICommand _openSoftwareDeviceSettlementOperationsPageCommand;
        private ICommand _openStatisticsPageCommand; 
        private ICommand _openUserPageCommand;
        private ICommand _openGiftCertificatesPageCommand;
        private ICommand _openNotificationPanelCommand; 

        private ICommand _exitUserCommand;

        private IMainAppServise _mainAppServise;
        private ISessionService _sessionService;


        public MainViewModel(IMainAppServise mainAppServise, ISessionService sessionService)
        {
            _sessionService = sessionService;
            _mainAppServise = mainAppServise;
            _userName = string.Empty;
            _page = new Page();
            _statusMenu = new System.Windows.Controls.UserControl(); 

            _exitAppCommand = CreateCommand(() => { ExitApp(); });
            _openSettingCommand = CreateCommand(() => { Page = App.Container.GetViewWithViewModel<SettingView,SettingViewModel>(); });
            _openStorageCommand = CreateCommand(() => { Page = App.Container.GetViewWithViewModel<StorageView, StorageViewModel>(); });
            _openExportProductCommand = CreateCommand(() => { new ExportExcelProductView().Show(); });
            _openImportProductCommand = CreateCommand(() => { new ImportProductExcelView().Show(); });
            _openCreateStikerCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<StickerPrintView, StickerPrintViewModel>().Show(); });
            _openSaleMenuCommand = CreateCommand(OpenSaleMenu);
            _openDeliveryOfGoodsCommand = CreateCommand(() => { new DeliveryProductView().Show(); });
            _openUsersPageCommand = CreateCommand(() => { Page = new UsersView(); });
            _openObjectOwnerPageCommand = CreateCommand(() => { Page = new ObjectOwnerShip(); });
            _openSoftwareDeviceSettlementOperationsPageCommand = CreateCommand(() => { Page = new OperationsRecorder(); });
            _openStatisticsPageCommand = CreateCommand(() => { Page = new StatisticsView(); }); 
            _exitUserCommand = CreateCommand(RemoveSession); 
            _openGiftCertificatesPageCommand = CreateCommand(() => { Page = new GiftCertificatesView(); });
            _openNotificationPanelCommand = CreateCommandAsync(OpenNotificationPanel);

            _visibilitiMenu = Visibility.Collapsed;
            _visibilitiNotification = Visibility.Collapsed;
            _notificationValue = "🔔 0";

            _windowState = WindowState.Normal;
            _resizeModeWindow = ResizeMode.NoResize; 

            StatusMenu = new DeviceStatusView();
            Notification = App.Container.GetViewWithViewModel<NotificationView, NotificationViewModel>(); 
            Page = new LoadingView();  
            MediatorService.AddEventAsync("VisibilitiNotification", async ()=> await ShowNotificationPanel());
            MediatorService.AddEventAsync<int>("AddNotificationCount", async count => await ShowNotificationCount(count));
        } 

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(UserName)); }

        }
        private Visibility _visibilitiMenu;
        public Visibility VisibilityMenu
        {
            get { return _visibilitiMenu; }
            set { _visibilitiMenu = value; OnPropertyChanged(nameof(VisibilityMenu)); }

        } 

        private Page _page;
        public Page Page
        {
            get { return _page; }
            set {_page = value; OnPropertyChanged(nameof(Page));}
        }

        private System.Windows.Controls.UserControl _statusMenu;
        public System.Windows.Controls.UserControl StatusMenu
        {
            get { return _statusMenu; }
            set { _statusMenu = value; OnPropertyChanged(nameof(StatusMenu)); }
        }

        private System.Windows.Controls.UserControl _notification;
        public System.Windows.Controls.UserControl Notification
        {
            get { return _notification; }
            set {  _notification=value; OnPropertyChanged(nameof(Notification));}
        }

        private Visibility _visibilitiNotification;
        public Visibility VisibilityNotification
        {
            get{ return _visibilitiNotification; }
            set { _visibilitiNotification = value; OnPropertyChanged(nameof(VisibilityNotification));}
        }
         
        private string _notificationValue;
        public string NotificationValue
        {
            get { return _notificationValue; }
            set { _notificationValue =  value; OnPropertyChanged(nameof(NotificationValue));}
        }

        private WindowState _windowState;
        public WindowState WindowState
        {
            get { return _windowState;}
            set { _windowState = value; OnPropertyChanged(nameof(WindowState)); }
        }

        private ResizeMode _resizeModeWindow;
        public  ResizeMode ResizeModeWindow
        {
            get { return _resizeModeWindow;}
            set { _resizeModeWindow = value; OnPropertyChanged(nameof(ResizeModeWindow));}
        } 
        public async Task LoadResourse()
        {
            await SafeExecuteAsync(InitResourse);
        }

        private async Task InitResourse()
        {

            InitStartViewButton();
            if (await _mainAppServise.IsConnectServer())
            {
                if (_sessionService.CheckingSession())
                {
                    await SetFieldWindow();
                }
                else
                {
                    Page = App.Container.GetViewWithViewModel<AuthorizationView, AuthorizationViewModel>();
                }

            }
            else
            { 
                WindowState = WindowState.Normal;
                ResizeModeWindow = ResizeMode.NoResize;
                Page = App.Container.GetViewWithViewModel<StartView, StartViewModel>();
            }
        }

        private void InitStartViewButton()
        {
            InitNavigationButton(); 
            //MediatorService.AddNavigation(NavigationButton.RedirectToChangePassword, (object obj) => { Page = new ChangePassword(); }); 
            //MediatorService.AddNavigation(NavigationButton.RedirectToTitleView, (object obg) => { VisibilityMenu = Visibility.Visible; SetName(); Page = new TitleView(); });
            //MediatorService.AddNavigation(NavigationButton.ExitApp, ExitApp);
        }

        private void InitNavigationButton()
        {
            MediatorService.AddNavigation(NavigationButton.RedirectToDashBoadPage,async ()=> { await SetFieldWindow(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToOperationsRecorderPage, () => { Page = Page = App.Container.GetViewWithViewModel<OperationRecorderView, OperationRecorderViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToWorkShiftMenuPage, () => { Page = App.Container.GetViewWithViewModel<WorkShiftMenuView, WorkShiftMenuViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToAuthorizationPage, () => { Page = App.Container.GetViewWithViewModel<AuthorizationView,AuthorizationViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectServerSelectionPage, () => { Page = App.Container.GetViewWithViewModel<ServerSelectionView, ServerSelectionViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectStartPage, () => { Page = App.Container.GetViewWithViewModel<StartView, StartViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToRegisterWindwoServicePage, () => { Page = App.Container.GetViewWithViewModel<RegisterWindowsServiceView, RegisterWindowsServiceViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.ExitApp, ExitApp);
        } 

        private async Task SetFieldWindow()
        { 
            WindowState = WindowState.Maximized;
            ResizeModeWindow = ResizeMode.CanResize;  
            SetName();
            Page = new DashBoardView();
            VisibilityMenu = Visibility.Visible; 
            await _mainAppServise.LoadResourse(); 
        } 

        private void RemoveSession()
        { 
            VisibilityMenu = Visibility.Hidden;
            WindowState = WindowState.Normal;
            ResizeModeWindow = ResizeMode.NoResize; 
            _sessionService.RemoveSession();
            Page = App.Container.GetViewWithViewModel<AuthorizationView, AuthorizationViewModel>(); 
        }

        private void SetName()
        {
            var item = _sessionService.User;
            if (item != null)
            {
                if (item.FullName != null && item.FullName != string.Empty)
                {
                    UserName = item.FullName;
                }
                else
                {
                    UserName = item.Login;
                }
            }
        } 
        private void OpenSaleMenu()
        {
            if (_sessionService.CheckingWorkingShiftStatus())
            {
                Page = App.Container.GetViewWithViewModel<WorkShiftMenuView,WorkShiftMenuViewModel>();
            }
            else
            {
                Page = App.Container.GetViewWithViewModel<OperationRecorderView, OperationRecorderViewModel>();
            }
        }  
        private void ExitApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        public ICommand OpenNotificationPanelCommand => _openNotificationPanelCommand;
        private async Task OpenNotificationPanel()
        {
            if (VisibilityNotification == Visibility.Collapsed)
            {
                await MediatorService.ExecuteEventAsync("ShowNotifications");
                VisibilityNotification = Visibility.Visible;
            }
            else
            {
                VisibilityNotification = Visibility.Collapsed;
            }
        }

        private async Task ShowNotificationPanel()
        {
            VisibilityNotification = Visibility.Visible; 
            await Task.Delay(1000);
            VisibilityNotification = Visibility.Collapsed; 
        }

        public async Task ShowNotificationCount(int count)
        {  
            NotificationValue = "🔔 "+ count;
        }

        public ICommand OpenSettingCommand => _openSettingCommand;
        public ICommand ExitAppCommand => _exitAppCommand;
        public ICommand OpenStorageCommand => _openStorageCommand;
        public ICommand OpenExportProductCommand => _openExportProductCommand;
        public ICommand OpenImportProductCommnad => _openImportProductCommand;
        public ICommand OpenCreateStikerCommnad => _openCreateStikerCommand;
        public ICommand OpenSaleMenuCommand => _openSaleMenuCommand;
        public ICommand OpenDeliveryOfGoodsCommnad => _openDeliveryOfGoodsCommand;
        public ICommand OpenUsersCommand => _openUsersPageCommand;
        public ICommand OpenUserCommand => _openUserPageCommand;
        public ICommand OpenObjectOwnerPageCommand => _openObjectOwnerPageCommand;
        public ICommand OpenSoftwareDeviceSettlementOperationsPageCommand => _openSoftwareDeviceSettlementOperationsPageCommand;
        public ICommand ExitUserCommand => _exitUserCommand;
        public ICommand OpenStatisticsPageCommand => _openStatisticsPageCommand;  

        public ICommand OpenGiftCertificatesPageCommand => _openGiftCertificatesPageCommand; 
    }
}
