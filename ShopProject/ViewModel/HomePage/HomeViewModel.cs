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
            openCreateStiker = new DelegateCommand(() => { new CreateStiker().Show(); });
            openOutOfStock = new DelegateCommand(() => { Page = new OutOfStock(); });
            openSaleMenu = new DelegateCommand(() => { Page = new SaleMenu(); });

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


        #region
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Number1");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Number2");
            }
        }
        private List<string> _dbConnect;
        public List<string> DbConnect
        {
            get { return _dbConnect; }
            set { _dbConnect = value; }
        }

        private string _dbConnectSelectedItem;
        public string DbConnectSelectedItem
        {
            get { return _dbConnectSelectedItem; }
            set { _dbConnectSelectedItem = value; }
        }

        private string _visibleCanvasAuthorization;
        public string VisibleCanvasAuthorization
        {
            get { return _visibleCanvasAuthorization; }
            set
            {
                _visibleCanvasAuthorization = value;
                OnPropertyChanged("VisibleCanvasAuthorization");
            }
        }



        #endregion

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
    }
}
