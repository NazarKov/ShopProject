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
        private ICommand _createDataBase;
        private ICommand _deleteDataBase;
        private ICommand _clearDataBase;

        private SettingDataBaseModel _model;
     
        public SettingDataBaseViewModel()
        {
            _model = new SettingDataBaseModel();
            _createDataBase = new DelegateCommand(()=> new Thread(new ThreadStart(CreateDataBaseInUpdateField)).Start());
            _deleteDataBase = new DelegateCommand(()=> new Thread(new ThreadStart(DeleteDataBaseAndDeleteSettingDataBase)).Start());
            _clearDataBase = new DelegateCommand(()=> new Thread(new ThreadStart(ClearDataBaseAll)).Start());
           
            _isCreateButton = true;
            _dbname = string.Empty;
            _isCreateLableName = string.Empty;
            _typeConnect = new List<string>();
            

            SetFieldComboBox();
            if (AppSettingsManager.GetParameterFiles("ConnectionString") != string.Empty)
            {
                SetFieldText(true);         
            }
            else
            {
                SetFieldText(false);
            }
        }
        private void SetFieldComboBox()
        {
            TypeConnect = new List<string>();
            TypeConnect.Add("EXPRESS");
            TypeConnect.Add("DEVELOPER");

            if (AppSettingsManager.GetParameterFiles("TypeConnect")=="EXPRESS")
            {
                SelectItemConnect = 0;
            }
            else if(AppSettingsManager.GetParameterFiles("TypeConnect") == "DEVELOPER")
            {
                SelectItemConnect = 1;
            }
            else
            {
                SelectItemConnect = 2;
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
        private int _selectItemConnect;
        public int SelectItemConnect
        {
            get { return _selectItemConnect; }
            set { _selectItemConnect = value; OnPropertyChanged("SelectItemConnect"); }
        }

        private List<string> _typeConnect;
        public List<string> TypeConnect
        {
            get { return _typeConnect; }
            set { _typeConnect = value; OnPropertyChanged("TypeConnect"); }
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

        public ICommand CreateDataBase => _createDataBase;

        private void CreateDataBaseInUpdateField()
        {
     
            if (_model.CreateDataBase(DBName,SelectItemConnect))
            {
                SetFieldText(true);
                MessageBox.Show("База даних створена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("База даних не створена","Eror",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        public ICommand DeleteDataBase => _deleteDataBase;
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

        public ICommand ClearDataBase => _clearDataBase;
        private void ClearDataBaseAll()
        {
            if(_model.ClearDataBase())
            {
                MessageBox.Show("База даних ощищена", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("База даних не очищена", "Eror", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      


    }
}
