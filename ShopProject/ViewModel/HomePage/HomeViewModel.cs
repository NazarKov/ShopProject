using ShopProject.Model.HomePage;
using ShopProject.Views.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using ShopProject.Model.Command;
using ShopProject.Views.SalePage;
using System.Windows.Forms;
using ShopProject.Views.AdminPage;
using ShopProject.Views.UserPage;
using ShopProject.Helpers;
using System.Threading; 
using ShopProject.View.StoragePage;
using ShopProject.View.ToolsPage;
using ShopProject.View.UserPage;
using ShopProject.View.StatisticsPage;
using MessageBox = System.Windows.MessageBox;
using ShopProject.ViewModel.AdminPage.WebServer;
using ShopProject.View.AdminPage.WebServer;
using ShopProject.View.HomePage;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;

namespace ShopProject.ViewModel.HomePage
{
    internal class HomeViewModel: ViewModel<HomeViewModel> 
    {
        private ICommand exitApp;
        private ICommand openSetting;
        private ICommand openStorage;
        private ICommand openExportProduct;
        private ICommand opemImportProduct;
        private ICommand openCreateStiker;
        private ICommand openSaleMenuCommand;
        private ICommand openDeliveryOfGoods;
        private ICommand openUserPageCommand;
        private ICommand openObjectOwnerPageCommand;
        private ICommand openSoftwareDeviceSettlementOperationsPageCommand;
        private ICommand openStatisticsPage;
        private ICommand _openWebServerPageCommand;

        private ICommand _exitUserCommand;
        
        
        private HomeModel _model;

        public HomeViewModel()
        {

            _model = new HomeModel();
            _widht = 0;
            _height = 0;

            _userName = string.Empty;
            _page = new Page();

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            exitApp = new DelegateCommand(() => {/* Application.Current.MainWindow.Close();*/});
            openSetting = new DelegateCommand(() => { new Setting().ShowDialog(); }); 
            openStorage = new DelegateCommand(() => { Page = new StorageView(); });
            openExportProduct = new DelegateCommand(() => { new ExportProductExelView().Show(); });
            opemImportProduct = new DelegateCommand(() => { new ImportProductExelView().Show(); });
            openCreateStiker = new DelegateCommand(() => { new CreateStickerView().Show(); });
            openSaleMenuCommand = new DelegateCommand(OpenSaleMenu);
            openDeliveryOfGoods = new DelegateCommand(() => { new DeliveryProductView().Show(); });
            openUserPageCommand = new DelegateCommand(() => { Page = new Users(); });
            openObjectOwnerPageCommand = new DelegateCommand(() => { Page = new ObjectOwnerShip(); });
            openSoftwareDeviceSettlementOperationsPageCommand = new DelegateCommand(() => { Page = new ShopProject.Views.AdminPage.OperationsRecorder(); });
            openStatisticsPage = new DelegateCommand(() => { Page = new StatisticsView(); });
            _openWebServerPageCommand = new DelegateCommand(()=> { Page = new SettingWebServerView(); });

            _exitUserCommand = new DelegateCommand(() => { Session.RemoveSession(); Page = new AuthorizationView(); VisibilityMenu = Visibility.Hidden; });

            //Page = new StartView();
            if (AppSettingsManager.GetParameterFiles("URL").ToString() == string.Empty)
            {
                Page = new StartView();
            }
            else
            {
                _model.Init();
                SetFieldWindow(null);
            }




            Mediator.Subscribe("RedirectToAuthorizationView", (object obg) => { Page = new AuthorizationView(); });
            Mediator.Subscribe("RedirectToOperationsRecorderView", SetFieldWindow); 
            Mediator.Subscribe("RedirectToWorkShiftMenu", OpenWorkShiftMenu);
            Mediator.Subscribe("OpenWorkShiftMenu", OpenWorkShiftMenu);
            Mediator.Subscribe("OpenChangePassword", (object obg) => { Page = new ChangePassword(); });
            Mediator.Subscribe("OpenAuthorization", (object obg) => { Page = new AuthorizationView(); });
            Mediator.Subscribe("OpenOperationRerocderMenu", (object obg) => { Page = new OperationsRecorderView(); });

        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged("UserName"); }

        }
        private Visibility _visibilitiMenu;
        public Visibility VisibilityMenu
        {
            get { return _visibilitiMenu; }
            set { _visibilitiMenu = value; OnPropertyChanged("VisibilityMenu"); }

        }
        private int _widht;
        public int Width
        {
            get { return _widht; } 
            set{ _widht = value;OnPropertyChanged("Width"); }
        }
        private int _height;
        public int Height
        {
            get { return _height; } 
            set { _height = value; OnPropertyChanged("Height"); }
        }

        private Page _page;
        public Page Page
        {
            get { return _page; }
            set {_page = value; OnPropertyChanged(nameof(Page));}
        }
        private void SetFieldWindow(object obj)
        {
            if (Session.CheckSession())
            {
                _visibilitiMenu = Visibility.Visible;
                if (Session.FocusDevices != null)
                {
                    Page = new StorageView();
                }
                else
                {
                    Page = new OperationsRecorderView();
                }
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
            if (item.FullName != null && item.FullName != string.Empty)
            {
                UserName = item.FullName;
            }
            else
            {
                UserName = item.Login;
            }
        }
        private void OpenWorkShiftMenu(object obj)
        {
            Page = new WorkShiftMenu();
        }
        private void OpenSaleMenu()
        {
            if (Session.FocusDevices != null)
            {
                Page = new WorkShiftMenu();
            }
            else
            {
                Page = new OperationsRecorderView();
            }
        }


        public ICommand OpenSetting => openSetting;
        public ICommand ExitApp => exitApp;
        public ICommand OpenStorage => openStorage;
        public ICommand OpenExportProduct => openExportProduct;
        public ICommand OpenImportProduct => opemImportProduct;
        public ICommand OpenCreateStiker => openCreateStiker;
        public ICommand OpenSaleMenuCommand => openSaleMenuCommand;
        public ICommand OpenDeliveryOfGoods => openDeliveryOfGoods;
        public ICommand OpenUserCommand => openUserPageCommand;
        public ICommand OpenObjectOwnerPageCommand => openObjectOwnerPageCommand;
        public ICommand OpenSoftwareDeviceSettlementOperationsPageCommand => openSoftwareDeviceSettlementOperationsPageCommand;
        public ICommand ExitUserCommand => _exitUserCommand;
        public ICommand OpenStatisticsPage => openStatisticsPage;
        public ICommand OpenWebServerPageCommand => _openWebServerPageCommand;
    }
}
