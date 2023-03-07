using ShopProject.DataBase.Context;
using ShopProject.Model;
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

        private SettingDataBaseModel _model;
     
        public SettingDataBaseViewModel()
        {
            _model = new SettingDataBaseModel();
            createDataBase = new DelegateCommand(()=>new Thread(new ThreadStart(CreateDataBaseInUpdateField)).Start());
            deleteDataBase = new DelegateCommand(()=> new Thread(new ThreadStart(DeleteDataBaseAndDeleteSettingDataBase)).Start());
           
            _isCreateButton = true;
            _dbname = string.Empty;
            _isCreateLableName = string.Empty;
           
            if (AppSettingsManager.GetParameterFiles("ConnectionString") != string.Empty)
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
                DBName = AppSettingsManager.GetParameterFiles("NameDataBase");
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
            set { _dbname = value; OnPropertyChanged("DBName"); }
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
     
            if (_model.CreateDataBase(DBName))
            {
                SetFieldText(true);
                MessageBox.Show("База даних створена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("База даних не створена","Eror",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        public ICommand DeleteDataBase => deleteDataBase;
        private void DeleteDataBaseAndDeleteSettingDataBase()
        {
            if (_model.DeleteDataBase())
            {
                SetFieldText(false);
                MessageBox.Show("База даних видалена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("База даних не видалена","Eror",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

      


    }
}
