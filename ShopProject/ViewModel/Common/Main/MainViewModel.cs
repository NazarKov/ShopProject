using ShopProject.Infrastructure.Mvvm;
using ShopProject.Infrastructure.Mvvm.Interface;
using ShopProject.Model.Navigation;
using ShopProject.Services.Infrastructure.ControlWindow.Interface;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Main.Interface;
using ShopProject.Services.Modules.Session.Interface;
using ShopProject.View.AdminPage.Dashboard;
using ShopProject.View.AdminPage.PointOfSale;
using ShopProject.View.AdminPage.Storage;
using ShopProject.View.AdminPage.Storage.Tools;
using ShopProject.View.AdminPage.UserManagement;
using ShopProject.View.Common.Authorization;
using ShopProject.View.Common.Main.MainComponent.DeviceStatus;
using ShopProject.View.Common.Main.MainComponent.LostConnection;
using ShopProject.View.Common.Setting;
using ShopProject.View.Common.Start;
using ShopProject.View.GiftCertificatesPage;
using ShopProject.View.HomePage.HomePageComponent;
using ShopProject.View.Integration.Excel.Export;
using ShopProject.View.Integration.Excel.Import;
using ShopProject.View.Integration.Printing;
using ShopProject.View.Integration.Windows.Service;
using ShopProject.View.TemplatePage;
using ShopProject.View.UserPage.PointOfSale;
using ShopProject.View.UserPage.PointOfSale.SaleMenu;
using ShopProject.ViewModel.AdminPage.Dashboard;
using ShopProject.ViewModel.AdminPage.PointOfSale;
using ShopProject.ViewModel.AdminPage.Storage;
using ShopProject.ViewModel.AdminPage.UserManagement;
using ShopProject.ViewModel.Common.Authorization;
using ShopProject.ViewModel.Common.Main.MainComponent.DeviceStatus;
using ShopProject.ViewModel.Common.Main.MainComponent.LostConnection;
using ShopProject.ViewModel.Common.Setting;
using ShopProject.ViewModel.Common.Start;
using ShopProject.ViewModel.HomePage.HomePageComponent;
using ShopProject.ViewModel.Integration.Printing;
using ShopProject.ViewModel.Integration.Windows.Service;
using ShopProject.ViewModel.UserPage.PointOfSale;
using ShopProject.ViewModel.UserPage.PointOfSale.SaleMenu;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.Common.Main
{
    internal class MainViewModel : ViewModel<MainViewModel> , IViewModelLoadResourse
    {
        private readonly ICommand _exitAppCommand;
        private readonly ICommand _openSettingCommand;
        private readonly ICommand _openStorageCommand;
        private readonly ICommand _openExportProductCommand;
        private readonly ICommand _openImportProductCommand;
        private readonly ICommand _openCreateStikerCommand; 
        private readonly ICommand _openAssignedPointOfSaleCommand;
        private readonly ICommand _openDeliveryOfGoodsCommand;
        private readonly ICommand _openUsersPageCommand;
        private readonly ICommand _openPoinOfSalePageCommand; 
       // private ICommand _openStatisticsPageCommand;  
        private readonly ICommand _openGiftCertificatesPageCommand;
        private readonly ICommand _openNotificationPanelCommand; 

        private readonly ICommand _exitUserCommand;

        private readonly IMainAppServise _mainAppService;
        private readonly ISessionService _sessionService;
        private readonly IMessageBoxControlService _messageBoxControlService;
        private readonly IControlWindowService _controlWindowService;

        public MainViewModel(IMainAppServise mainAppService, ISessionService sessionService, IMessageBoxControlService messageBoxControlService,IControlWindowService controlWindowService)
        {
            _sessionService = sessionService;
            _mainAppService = mainAppService;
            _messageBoxControlService = messageBoxControlService;
            _controlWindowService = controlWindowService;
            _userName = string.Empty;
            _page = new Page();
            _statusMenu = new System.Windows.Controls.UserControl();
            _lostConnectionPage = new Page();
            _notification = new UserControl();

            _openSettingCommand = CreateCommand(() => { Page = App.Container.GetViewWithViewModel<SettingView,SettingViewModel>(); });
            _openStorageCommand = CreateCommand(() => { Page = App.Container.GetViewWithViewModel<StorageView, StorageViewModel>(); });
            _openCreateStikerCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<StickerPrintView, StickerPrintViewModel>().Show(); });
            _openUsersPageCommand = CreateCommand(() => { Page = App.Container.GetViewWithViewModel<UserManagementView, UserManagementViewModel>(); });
            _openPoinOfSalePageCommand = CreateCommand(() => { Page = App.Container.GetViewWithViewModel<PointOfSaleView,PointOfSaleViewModel>(); }); 
            _exitAppCommand = CreateCommand(() => { ExitApp(); });



            _openExportProductCommand = CreateCommand(() => { new ExportExcelProductView().Show(); });
            _openImportProductCommand = CreateCommand(() => { new ImportProductExcelView().Show(); });
            _openAssignedPointOfSaleCommand = CreateCommand(OpenAssignedPointOfSale);
            _openDeliveryOfGoodsCommand = CreateCommand(() => { new DeliveryProductView().Show(); }); 
            _exitUserCommand = CreateCommandAsync(RemoveSession); 
            _openGiftCertificatesPageCommand = CreateCommand(() => { Page = new GiftCertificatesView(); });
            _openNotificationPanelCommand = CreateCommandAsync(OpenNotificationPanel);

            _pageVisibiliti = Visibility.Visible;
            _visibilitiLostConnectionPage = Visibility.Collapsed;
            _visibilitiShadowLostConnectionPage = Visibility.Collapsed;
            _visibilitiShadowPage = Visibility.Collapsed;
            _isEnableMenuButton = false;
            _visibilitiMenu = Visibility.Collapsed;
            _visibilitiNotification = Visibility.Collapsed;
            _notificationValue = "0"; 

            StatusMenu = App.Container.GetViewWithViewModel<DeviceStatusView,DeviceStatusViewModel>();
            LostConnectionPage = App.Container.GetViewWithViewModel<ConnectionLostView,ConnectionLostViewModel>();
            Notification = App.Container.GetViewWithViewModel<NotificationView, NotificationViewModel>();
            Page = new LoadingView(); 
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

        private Visibility _pageVisibiliti;
        public Visibility PageVisibiliti
        {
            get { return _pageVisibiliti; }
            set { _pageVisibiliti = value;OnPropertyChanged(nameof(PageVisibiliti)); }
        }

        private Page _lostConnectionPage;
        public Page LostConnectionPage
        {
            get { return _lostConnectionPage; }
            set { _lostConnectionPage = value; OnPropertyChanged(nameof(LostConnectionPage));}
        }
        private Visibility _visibilitiLostConnectionPage;
        public Visibility VisibilitiLostConnectionPage
        {
            get { return _visibilitiLostConnectionPage; }
            set { _visibilitiLostConnectionPage = value; OnPropertyChanged(nameof(VisibilitiLostConnectionPage)); }
        }

        private Visibility _visibilitiShadowPage;
        public Visibility VisibilitiShadowPage
        {
            get { return _visibilitiShadowPage; }
            set { _visibilitiShadowPage = value; OnPropertyChanged(nameof(VisibilitiShadowPage)); }
        }
        private Visibility _visibilitiShadowLostConnectionPage;
        public Visibility VisibilitiShadowLostConnectionPage
        {
            get { return _visibilitiShadowLostConnectionPage; }
            set { _visibilitiShadowLostConnectionPage = value; OnPropertyChanged(nameof(VisibilitiShadowLostConnectionPage)); }
        }
        private bool _isEnableMenuButton;
        public bool IsEnableMenuButton
        {
            get { return _isEnableMenuButton; }
            set { _isEnableMenuButton = value; OnPropertyChanged(nameof(IsEnableMenuButton)); }
        }

        private UserControl _statusMenu;
        public  UserControl StatusMenu
        {
            get { return _statusMenu; }
            set { _statusMenu = value; OnPropertyChanged(nameof(StatusMenu)); }
        }

        private UserControl _notification;
        public  UserControl Notification
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
        public async Task LoadResourse()
        {
            await SafeExecuteAsync(InitResourse);
        }

        private async Task InitResourse()
        {

            InitStartViewButton(); 
            if (await _mainAppService.IsConnectServer())
            {
                await _mainAppService.LoadStartData();

                if (await _sessionService.CheckingSession())
                {
                    var result = await _mainAppService.LoadUserData(); 
                    if (result.IsSuccess)
                    {
                        if (result.Data != null)
                        {
                            if (result.Data.AutomaticLogin)
                            {
                                await _mainAppService.LoadSession();
                                await SetFieldWindow();
                                return;
                            }
                        }
                    } 
                }

                await MediatorService.ExecuteEventAsync("StopTimerCheckConnect");
                VisibilitiLostConnectionPage = Visibility.Collapsed;
                Page = App.Container.GetViewWithViewModel<AuthorizationView, AuthorizationViewModel>();
            }
            else
            {
                VisibilitiLostConnectionPage = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("StopTimerCheckConnect"); 
                Page = App.Container.GetViewWithViewModel<StartView, StartViewModel>();
            }
        }

        private void InitStartViewButton()
        {
            InitNavigationButton();
            MediatorService.AddEventAsync("VisibilitiNotification", async () => await ShowNotificationPanel());
            MediatorService.AddEventAsync<int>("AddNotificationCount", async count => await ShowNotificationCount(count)); 
            MediatorService.AddEventAsync("LostConnectSetVisible", async () => { VisibilitiShadowLostConnectionPage = Visibility.Visible; VisibilitiLostConnectionPage = Visibility.Visible; IsEnableMenuButton = false; });
            MediatorService.AddEventAsync("LostConnectSetHidden", async () => { VisibilitiShadowLostConnectionPage = Visibility.Collapsed; VisibilitiLostConnectionPage = Visibility.Collapsed; IsEnableMenuButton = true; });
            MediatorService.AddEventAsync("VisibilitiShadowSetVisible", async () => { VisibilitiShadowPage = Visibility.Visible; });
            MediatorService.AddEventAsync("VisibilitiShadowSetHidden", async () => { VisibilitiShadowPage = Visibility.Collapsed; });
            MediatorService.AddEvent("CloseSecondaryWindows", _controlWindowService.CloseSecondaryWindows);

            MediatorService.AddEventAsync("StartApp", async () =>
            {
                Page = new LoadingView();
                PageVisibiliti = Visibility.Visible;
                await _mainAppService.LoadStartData();
                await _mainAppService.LoadUserData();
                await _mainAppService.LoadSession();
                await SetFieldWindow();
            });

            MediatorService.AddEventAsync("SetPageAfterLoadingResourse", async () => { Page = App.Container.GetViewWithViewModel<DashBoardView, DashBoardViewModel>(); });
            MediatorService.AddEventAsync("SetHidenPage", async () => { PageVisibiliti = Visibility.Hidden; }); 
        }

        private void InitNavigationButton()
        { 
            MediatorService.AddNavigation(NavigationButton.RedirectToAssignedPointsOfSalePage, () => { Page = App.Container.GetViewWithViewModel<AssignedPointsOfSaleView, AssignedPointsOfSaleViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToWorkShiftMenuPage, () => { Page = App.Container.GetViewWithViewModel<WorkShiftMenuView, WorkShiftMenuViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToAuthorizationPage, () => { VisibilityMenu = Visibility.Collapsed; Page = App.Container.GetViewWithViewModel<AuthorizationView,AuthorizationViewModel>(); });
            MediatorService.AddEventAsync("ErrorUnAuthorization", 
                async () =>
                {
                    await RemoveSession();
                    _controlWindowService.ActivateMainWindow();
                    await _messageBoxControlService.Show("Сесія користувача завершенна авторизуйтеся повторно", "Information", ShopProject.Controls.MessegeBox.Enum.MessageBoxType.Information, "AuthorizationSnadow"); 
                });
            MediatorService.AddNavigation(NavigationButton.RedirectServerSelectionPage, () => { Page = App.Container.GetViewWithViewModel<ServerSelectionView, ServerSelectionViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectStartPage, () => { Page = App.Container.GetViewWithViewModel<StartView, StartViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.RedirectToRegisterWindwoServicePage, () => { Page = App.Container.GetViewWithViewModel<RegisterWindowsServiceView, RegisterWindowsServiceViewModel>(); });
            MediatorService.AddNavigation(NavigationButton.ExitApp, ExitApp);
        } 

        private async Task SetFieldWindow()
        {  
            SetName(); 
            VisibilityMenu = Visibility.Visible; 
            await MediatorService.ExecuteEventAsync("StartTimerCheckConnect");
        }  
        private async Task RemoveSession()
        { 
            VisibilityMenu = Visibility.Hidden; 
            IsEnableMenuButton = false;
            _sessionService.RemoveSession();
            Page = App.Container.GetViewWithViewModel<AuthorizationView, AuthorizationViewModel>();
            await MediatorService.ExecuteEventAsync("StopTimerCheckConnect");
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
        private void OpenAssignedPointOfSale()
        {
            if (_sessionService.CheckShiftSetting())
            {
                if (  _sessionService.CheckIsOpenShift())
                {
                    Page = App.Container.GetViewWithViewModel<WorkShiftMenuView,WorkShiftMenuViewModel>();
                }
                else
                {
                    Page = App.Container.GetViewWithViewModel<AssignedPointsOfSaleView, AssignedPointsOfSaleViewModel>();
                }
            }
            else
            { 
                Page = App.Container.GetViewWithViewModel<AssignedPointsOfSaleView, AssignedPointsOfSaleViewModel>();
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
        public ICommand OpenAssignedPointOfSaleCommand => _openAssignedPointOfSaleCommand;
        public ICommand OpenDeliveryOfGoodsCommnad => _openDeliveryOfGoodsCommand;
        public ICommand OpenUsersCommand => _openUsersPageCommand; 
        public ICommand OpenPoinOfSalePageCommand => _openPoinOfSalePageCommand; 
        public ICommand ExitUserCommand => _exitUserCommand;
        //public ICommand OpenStatisticsPageCommand => _openStatisticsPageCommand;  

        public ICommand OpenGiftCertificatesPageCommand => _openGiftCertificatesPageCommand; 
    }
}
