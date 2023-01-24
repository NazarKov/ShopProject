using ShopProject.DataBase.Context;
using ShopProject.Model.Command;
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
    internal class SettingDataBaseViewModel : ViewModel<SettingDataBaseViewModel>
    {
        private ICommand createDataBase;
        private ICommand deleteDataBase;

        SettingDataBaseModel settingDb;
     
        public SettingDataBaseViewModel()
        {
            settingDb = new SettingDataBaseModel();
            createDataBase = new DelegateCommand(CreateDataBaseInUpdateField);
            deleteDataBase = new DelegateCommand(DeleteDataBaseAndDeleteSettingDataBase);
           
            _isCreateButton = true;
            _dbname = string.Empty;
            _isCreateLableName = string.Empty;
           
            if (settingDb.GetName() != string.Empty)
            {
                SetFieldText(true);         
            }
            else
            {
                SetFieldText(false);
            }
        }

        private void SetFieldText(bool createTrueAndFalse)
        {
            if (createTrueAndFalse)
            {
                IsCreateButton = false;
                IsCreateLableName = "База створена";
                DBName = settingDb.GetName();
            }
            else
            {
                IsCreateButton = true;
                IsCreateLableName =string.Empty;
                DBName = string.Empty;
            }
        }

        private string _dbname;
        public string DBName
        {
            get { return _dbname; }
            set { _dbname = value; }
        }

        private string _isCreateLableName;
        public string IsCreateLableName
        {
            get { return _isCreateLableName; }
            set { _isCreateLableName = value; OnPropertyChanged("IsCreateLableName");}
        }

        private bool _isCreateButton;
        public bool IsCreateButton
        {
            get { return _isCreateButton; }
            set { _isCreateButton = value; OnPropertyChanged("IsCreateButton"); }
        }

        public ICommand CreateDataBase => createDataBase;

        private void CreateDataBaseInUpdateField()
        {
            settingDb.SetName(_dbname);
            if (settingDb.SetConnectionStringDataBase())
            {
                new Thread(new ThreadStart(ThreadCreateDataBase)).Start();
                SaveFileSettingDataBase();
            }
            else
            {
                MessageBox.Show("База даних не створена","Eror",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        
        private void ThreadCreateDataBase()
        {
            if (settingDb.CreateDataBase())
            {
                UpdateField();
                MessageBox.Show("База даних створена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void UpdateField()
        {
            IsCreateLableName = "База створена";
            IsCreateButton = false;
        }
        private void SaveFileSettingDataBase()
        {
            if (_dbname != null)
            {
                settingDb.SetName(_dbname);
                settingDb.FileSaveOptionsDataBase();
            }
            else
            {
                GenerateException("Введіть назву бази даних");
            }
        }

        private Exception GenerateException(string nameExeption)
        {
            throw new Exception(nameExeption);
        }

        public ICommand DeleteDataBase => deleteDataBase;

        private void DeleteDataBaseAndDeleteSettingDataBase()
        {
            new Thread(new ThreadStart(DeleteDataBaseisFieldUpdate)).Start();
            settingDb.RemoveSetting();
        }
        private void DeleteDataBaseisFieldUpdate()
        {
            if (settingDb.DeleteDataBase())
            {
                SetFieldText(false);
                MessageBox.Show("База даних видалена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
