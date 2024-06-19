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
using ShopProject.Views.StoragePage;
using ShopProject.Model.Command;
using ShopProject.Views.SalePage;
using System.Windows.Forms;
using ShopProject.Views.AdminPage;
using ShopProject.Views.UserPage;
using ShopProject.Helpers;
using System.Threading;
using LocateWindow;
using ShopProject.View.StoragePage;
using ShopProject.View.ToolsPage;
using ShopProject.View.UserPage;

namespace ShopProject.ViewModel.HomePage
{
    internal class HomeViewModel: ViewModel<HomeViewModel> 
    {
        private ICommand exitApp;
        private ICommand openSetting;
        private ICommand openStorage;
        private ICommand openArchive;
        private ICommand openExportProduct;
        private ICommand opemImportProduct;
        private ICommand openCreateStiker;
        private ICommand openOutOfStock;
        private ICommand openSaleMenuCommand;
        private ICommand openDeliveryOfGoods;
        private ICommand openUserPageCommand;
        private ICommand openObjectOwnerPageCommand;
        private ICommand openSoftwareDeviceSettlementOperationsPageCommand;
        private ICommand exitUser;

        public HomeViewModel()
        {
            _widht = 0;
            _height = 0;

            _userName = string.Empty;
            _page = new Page();

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            exitApp = new DelegateCommand(() => {/* Application.Current.MainWindow.Close();*/});
            openSetting = new DelegateCommand(() => { new Setting().ShowDialog(); });
            openArchive = new DelegateCommand(() => { Page = new Archive(); });
            openStorage = new DelegateCommand(() => { Page = new StorageView(); });
            openExportProduct = new DelegateCommand(() => { new ExportProductExelView().Show(); });
            opemImportProduct = new DelegateCommand(() => { new ImportProductExelView().Show(); });
            openCreateStiker = new DelegateCommand(() => { new CreateStickerView().Show(); });
            openOutOfStock = new DelegateCommand(() => { Page = new OutOfStock(); });
            openSaleMenuCommand = new DelegateCommand(OpenSaleMenu);
            openDeliveryOfGoods = new DelegateCommand(() => { new DeliveryProductView().Show(); });
            openUserPageCommand = new DelegateCommand(() => { Page = new Users(); });
            openObjectOwnerPageCommand = new DelegateCommand(() => { Page = new ObjectOwnerShip(); });
            openSoftwareDeviceSettlementOperationsPageCommand = new DelegateCommand(() => { Page = new ShopProject.Views.AdminPage.OperationsRecorder(); });
            exitUser = new DelegateCommand(() => { Session.Remove(); Page = new Authorization(); VisibilityMenu = Visibility.Hidden; });

            SetFieldWindow(null);

            Mediator.Subscribe("VisibleMenu", SetFieldWindow);
            Mediator.Subscribe("OpenWorkShiftMenu", OpenWorkShiftMenu);
            Mediator.Subscribe("OpenChangePassword", (object obg) => { Page = new ChangePassword(); });
            Mediator.Subscribe("OpenAuthorization", (object obg) => { Page = new Authorization(); });
            Mediator.Subscribe("OpenOperationRerocderMenu",(object obg) => { Page = new OperationsRecorderView(); });
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
            set 
            {
                _page = value;
                OnPropertyChanged("Page");
            }
        }
        private void SetFieldWindow(object obj)
        {
            if (Session.CheckSession())
            {
                _visibilitiMenu = Visibility.Visible;
                if (Session.FocusDevices != null)
                {
                    Page = new WorkShiftMenu();
                }
                else
                {
                    Page = new OperationsRecorderView();
                }
                SetName();
            }
            else
            {
                Page = new Authorization();
                _visibilitiMenu = Visibility.Collapsed;
            }
            OnPropertyChanged(nameof(VisibilityMenu));
        }
        private void SetName()
        {
            var item = Session.User;
            if (item.FullName != string.Empty)
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
        public ICommand OpenArchive => openArchive;
        public ICommand OpenExportProduct => openExportProduct;
        public ICommand OpenImportProduct => opemImportProduct;
        public ICommand OpenCreateStiker => openCreateStiker;
        public ICommand OpenOutOfStock => openOutOfStock;
        public ICommand OpenSaleMenuCommand => openSaleMenuCommand;
        public ICommand OpenDeliveryOfGoods => openDeliveryOfGoods;
        public ICommand OpenUserCommand => openUserPageCommand;
        public ICommand OpenObjectOwnerPageCommand => openObjectOwnerPageCommand;
        public ICommand OpenSoftwareDeviceSettlementOperationsPageCommand => openSoftwareDeviceSettlementOperationsPageCommand;
        public ICommand ExitUserCommand => exitUser;
    }
}
