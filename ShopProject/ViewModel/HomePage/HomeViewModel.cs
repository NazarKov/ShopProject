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

        HomeModel homeModel;

        public HomeViewModel()
        {

            exitApp = new DelegateCommand(() => { Application.Current.MainWindow.Close();});
            openSetting = new DelegateCommand(() => { new Setting().ShowDialog(); });
            openArchive = new DelegateCommand(() => { Page = new Archive(); });
            openStorage = new DelegateCommand(() => { Page = new Storage(); });
            openExportProduct = new DelegateCommand(() => { new ExportProductExel().ShowDialog(); });
            opemImportProduct = new DelegateCommand(() => { new ImportProductExel().ShowDialog(); });
            
            newModels();

        }

        void newModels()
        {
            homeModel = new HomeModel();
            _dbConnect = new List<string>();
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

    }
}
