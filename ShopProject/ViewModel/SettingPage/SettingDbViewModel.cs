using ShopProject.DataBase.Context;
using ShopProject.Model;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingDbViewModel : ViewModel<SettingDbViewModel>
    {
        private ICommand createDataBase;
        private ICommand deleteDataBase;
        SettingDbModel settingDb;
        ShopContext context;

        public SettingDbViewModel()
        {
            settingDb = new SettingDbModel();

            createDataBase = new DelegateCommand(CreateDb);
            deleteDataBase = new DelegateCommand(DeleteDb);

            settingDb.DerializableResurses();
            if (settingDb.setting.NameDb != "")
            {
                IsCreateButton = false;
                IsCreateLableName = "База створена";
                DBName=settingDb.setting.NameDb;
            }
            else
            {
                IsCreateButton = true;
            }

        }

        private string _dbname;
        public string DBName
        {
            set { _dbname = value; }
            get { return _dbname; }
        }

        private string _isCreateLableName;
        public string IsCreateLableName
        {
            get { return _isCreateLableName; }
            set {
                _isCreateLableName = value;
                OnPropertyChanged("IsCreateLableName");
            }
        }

        private bool _isCreateButton;
        public bool IsCreateButton
        {
            get { return _isCreateButton; }
            set
            {
                _isCreateButton = value;
                OnPropertyChanged("IsCreateButton");
            }
        }

        public ICommand CreateDataBase => createDataBase;

        private void CreateDb()
        {  
            if (settingDb.ConnectionDB(_dbname))
            {
                new Thread(new ThreadStart(createDb)).Start();

                IsCreateLableName = "База створена";
                IsCreateButton = false;

                settingDb.setting.NameDb = _dbname;
                settingDb.SerializableResurses();

            }
            else
            {
                MessageBox.Show("База даних не створена","Eror",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        
        void createDb()
        {
            try
            {
                context = new ShopContext();
                context.Database.Create();
                MessageBox.Show("База даних створена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand DeleteDataBase => deleteDataBase;

        private void DeleteDb()
        {
            
            new Thread(new ThreadStart(deleteDb)).Start();
            settingDb.RemoveSetting();
        }
        void deleteDb()
        {
            context = new ShopContext();
            context.Database.Delete();
            IsCreateButton = true;
            IsCreateLableName = "";
            DBName = null;
            MessageBox.Show("База даних видалена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
