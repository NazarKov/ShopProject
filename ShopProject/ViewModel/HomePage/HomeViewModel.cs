using ShopProject.Helpers; 
using ShopProject.Helpers.Exceptions;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.HomePage;
using ShopProject.View.AdminPage.WebServer;
using ShopProject.View.HomePage;
using ShopProject.View.HomePage.HomePageComponent;
using ShopProject.View.StatisticsPage;  
using ShopProject.View.StoragePage;
using ShopProject.View.StoragePage.ExcelPage.ExportExcelPage;
using ShopProject.View.StoragePage.ExcelPage.ImportExcelPage;
using ShopProject.View.TemplatePage;
using ShopProject.View.ToolsPage;
using ShopProject.View.UserPage;
using ShopProject.Views.AdminPage;
using ShopProject.Views.SalePage;
using ShopProject.Views.SettingPage; 
using ShopProject.Views.UserPage;
using System;
using System.Threading.Tasks; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShopProject.ViewModel.HomePage
{
    internal class HomeViewModel: ViewModel<HomeViewModel> 
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
        private ICommand _openWebServerPageCommand;
        private ICommand _openUnitOfMeasurePageCommand;
        private ICommand _openProductCodeUKTZEDPageCommand;
        private ICommand _openUserPageCommand;

        private ICommand _exitUserCommand;
         
        private HomeModel _model;
        

        public HomeViewModel()
        {

            _model = new HomeModel();
            _widht = 0;
            _height = 0;

            _userName = string.Empty;
            _page = new Page();
            _statusMenu = new System.Windows.Controls.UserControl();
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            _exitAppCommand = new DelegateCommand(() => { ExitApp(null); });
            _openSettingCommand = new DelegateCommand(() => { new Setting().ShowDialog(); }); 
            _openStorageCommand = new DelegateCommand(() => { Page = new StorageView(); });
            _openExportProductCommand = new DelegateCommand(() => { new ExportExcelProductView().Show(); });
            _openImportProductCommand = new DelegateCommand(() => { new ImportProductExcelView().Show(); });
            _openCreateStikerCommand = new DelegateCommand(() => { new CreateStickerView().Show(); });
            _openSaleMenuCommand = new DelegateCommand(OpenSaleMenu);
            _openDeliveryOfGoodsCommand = new DelegateCommand(() => { new DeliveryProductView().Show(); });
            _openUsersPageCommand = new DelegateCommand(() => { Page = new UsersView(); });
            _openObjectOwnerPageCommand = new DelegateCommand(() => { Page = new ObjectOwnerShip(); });
            _openSoftwareDeviceSettlementOperationsPageCommand = new DelegateCommand(() => { Page = new ShopProject.Views.AdminPage.OperationsRecorder(); });
            _openStatisticsPageCommand = new DelegateCommand(() => { Page = new StatisticsView(); });
            _openWebServerPageCommand = new DelegateCommand(()=> { Page = new SettingWebServerView(); });
            _openUnitOfMeasurePageCommand = new DelegateCommand(() => { Page = new UnitsOfMeasureView(); });
            _exitUserCommand = new DelegateCommand(() => { Session.RemoveSession(); Page = new AuthorizationView(); VisibilityMenu = Visibility.Hidden; });
            _openProductCodeUKTZEDPageCommand = new DelegateCommand(() => { Page = new ProductCodeUKTZEDView(); });
            _openUserPageCommand = new DelegateCommand(() => { Page = new UserView(); });
            
            _visibilitiMenu = Visibility.Hidden;

            StatusMenu = new DeviceStatusView();
            Page = new LoadingView(); 
            InitResourse();
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
        private int _widht;
        public int Width
        {
            get { return _widht; } 
            set{ _widht = value;OnPropertyChanged(nameof(Width)); }
        }
        private int _height;
        public int Height
        {
            get { return _height; } 
            set { _height = value; OnPropertyChanged(nameof(Height)); }
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
        private void InitResourse()
        {
            try
            { 
                InitStartViewButton();
                if (AppSettingsManager.GetParameterFiles("URL").ToString() == string.Empty)
                {
                    Page = new StartView();
                }
                else
                {
                    _model.Init();
                    var result = false;

                    Task t = Task.Run(async () =>
                    {
                        result = await _model.IsConnectWebServer();
                    });
                    t.ContinueWith(t =>
                    {
                        if (result)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                SetFieldWindow();
                            }); 
                            MediatorService.AddEvent(NavigationButton.RedirectToWorkShiftMenu.ToString(), OpenWorkShiftMenu);
                            MediatorService.AddEvent(NavigationButton.RedirectToChangePassword.ToString(), (object obj) => { Page = new ChangePassword(); });
                            MediatorService.AddEvent(NavigationButton.RedirectToOperationsRecorderView.ToString(), (object obj) => { Page = new OperationsRecorderView(); }); 
                        }
                        else
                        {
                            Page = new StartView();
                        }
                    });

                }

            }
            catch (ExceptionURL)
            {
                Page = new StartView();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void InitStartViewButton()
        {
            MediatorService.AddEvent(NavigationButton.RedirectToAuthorizationView.ToString(), (object obg) => { Page = new AuthorizationView(); });
            MediatorService.AddEvent(NavigationButton.RedirectToTitleView.ToString(), (object obg) => { VisibilityMenu = Visibility.Visible; SetName(); Page = new TitleView();});
            MediatorService.AddEvent(NavigationButton.ExitApp.ToString(), ExitApp);
        }
        private void SetFieldWindow()
        {
            if (Session.CheckSession())
            {
                _visibilitiMenu = Visibility.Visible;
                Page = new TitleView();
                SetName();
            }
            else
            {
                Page = new AuthorizationView();
                _visibilitiMenu = Visibility.Collapsed;
            }
            OnPropertyChanged(nameof(VisibilityMenu));
        } 

        private void SetName()
        {
            var item = Session.User;
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
        private void OpenWorkShiftMenu(object obj)
        {
            Page = new WorkShiftMenu();
        }
        private void OpenSaleMenu()
        {
            if (Session.WorkingShiftStatus!=null &&  Session.WorkingShiftStatus.OperationRecorder != null)
            {
                Page = new WorkShiftMenu();
            }
            else
            {
                Page = new OperationsRecorderView();
            }
        } 
        private void ExitApp(object obj)
        {
            System.Windows.Application.Current.Shutdown();
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
        public ICommand OpenWebServerPageCommand => _openWebServerPageCommand;
        public ICommand OpenUnitOfMeasurePageCommand => _openUnitOfMeasurePageCommand;
        public ICommand OpenProductCodeUKTZEDPageCommand => _openProductCodeUKTZEDPageCommand;
    }
}
