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
using ShopProject.Views.ToolsPage;
using ShopProject.Model.Command;
using ShopProject.Views.SalePage;
using System.Windows.Forms;
using ShopProject.Views.UserPage;

namespace ShopProject.ViewModel.HomePage
{
    internal class HomeViewModel: ViewModel<HomeViewModel>
    {
        private ICommand authorizationUser;
        private ICommand exitApp;
        private ICommand openSetting;
        private ICommand openStorage;
        private ICommand openArchive;
        private ICommand openExportProduct;
        private ICommand opemImportProduct;
        private ICommand openCreateStiker;
        private ICommand openOutOfStock;
        private ICommand openSaleMenu;
        private ICommand openDeliveryOfGoods;
        private ICommand openUserPageCommand;

        private HomeModel _homeModel;

        public HomeViewModel()
        {
            _widht = 0;
            _height = 0;

            _homeModel = new HomeModel();

            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;

            exitApp = new DelegateCommand(() => {/* Application.Current.MainWindow.Close();*/});
            openSetting = new DelegateCommand(() => { new Setting().ShowDialog(); });
            openArchive = new DelegateCommand(() => { Page = new Archive(); });
            openStorage = new DelegateCommand(() => { Page = new Storage(); });
            openExportProduct = new DelegateCommand(() => { new ExportGoodsExel().Show(); });
            opemImportProduct = new DelegateCommand(() => { new ImportGoodsExel().Show(); });
            openCreateStiker = new DelegateCommand(() => { new CreateSticker().Show(); });
            openOutOfStock = new DelegateCommand(() => { Page = new OutOfStock(); });
            openSaleMenu = new DelegateCommand(() => { Page = new WorkShiftMenu(); });
            openDeliveryOfGoods = new DelegateCommand(() => { new DeliveryOfGoods().Show(); });
            openUserPageCommand = new DelegateCommand(() => { Page = new User(); });
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

        public ICommand OpenSetting => openSetting;
        public ICommand ExitApp => exitApp;
        public ICommand OpenStorage => openStorage;
        public ICommand OpenArchive => openArchive;
        public ICommand OpenExportProduct => openExportProduct;
        public ICommand OpenImportProduct => opemImportProduct;
        public ICommand OpenCreateStiker => openCreateStiker;
        public ICommand OpenOutOfStock => openOutOfStock;
        public ICommand OpenSaleMenu => openSaleMenu;
        public ICommand OpenDeliveryOfGoods => openDeliveryOfGoods;
        public ICommand OpenUserCommand => openUserPageCommand;
    }
}
