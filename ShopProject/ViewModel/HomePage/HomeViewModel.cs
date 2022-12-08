using ShopProject.Model.HomePage;
using ShopProject.Model;
using ShopProject.Views.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using ShopProject.Views.HomePage;

namespace ShopProject.ViewModel.HomePage
{
    internal class HomeViewModel: ViewModel<HomeViewModel>
    {
        private ICommand authorizationUser;
        private ICommand exitApp;
        private ICommand openSetting;
        private ICommand openStorage;
        private ICommand openArchive;

        HomeModel homeModel;

        public HomeViewModel()
        {

            authorizationUser = new DelegateCommand(AuthorizationUser);
            exitApp = new DelegateCommand(() => { Application.Current.MainWindow.Close();});
            openSetting = new DelegateCommand(() => { new Setting().ShowDialog(); });
            openArchive = new DelegateCommand(() => { Page = new Archive(); });
            openStorage = new DelegateCommand(() => { Page = new Storage(); });
            
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

        public ICommand AuthorizationUserCommand => authorizationUser;

        private void AuthorizationUser()
        {
            string temp;
            homeModel.ConnectionDB(_dbConnectSelectedItem);
            temp = homeModel.Authorization(_name, _password);
            if (temp == "0")
            {
                MessageBox.Show("Акаунт не знайдено", "Eror", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                VisibleCanvasAuthorization = "Hidden";
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

    }
}
