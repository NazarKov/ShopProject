using ShopProject.DataBase.Context;
using ShopProject.Interfaces.InterfacesContextDatabase;
using ShopProject.Model.DataBase;
using ShopProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace ShopProject.Model.SettingPage
{
    internal class SettingDataBaseModel 
    {
      
        private IContextDataBase _contextDataBase;
        public SettingDataBaseModel()
        {
            _contextDataBase = new DataContext();
        }

        public bool CreateDataBase(string name)
        {
            try
            {
                AppSettingsManager.SetConnectionDataBase(name,TypeConnections.DEVELOPER);
                AppSettingsManager.SetParameterFile("NameDataBase", name);
                _contextDataBase.Create();
                return true;
                
            }
            catch (Exception ex)
            {
                HendlerShowExeption(ex); return false;
            }
        }

        public bool DeleteDataBase()
        {
            try
            {
                _contextDataBase.Delete();
                AppSettingsManager.SetParameterFile("NameDataBase", string.Empty);
                AppSettingsManager.SetParameterFile("ConnectionString", string.Empty);
                return true;
            }
            catch(Exception ex)
            {
                HendlerShowExeption(ex);
                return false;
            }
        }
        public bool ClearDataBase()
        {
            try
            {

                _contextDataBase.Clear();
                return true;
            }
            catch(Exception ex)
            {
                HendlerShowExeption(ex);
                return false;
            }
        }
      
        private void HendlerShowExeption(Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);

        }
    }
}
