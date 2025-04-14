using ShopProject.Helpers;
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
        //private SettingsDataBaseApiContoller _contoller;

        public SettingDataBaseModel()
        {
            //_contoller = HttpDataBaseAPIController.SettingContoller;
        }
        //public async Task<SettingsDataBase> GetSetting(string token)
        //{
        //    try
        //    {
        //        if (_contoller == null)
        //        {
        //            throw new Exception("Перевірте підключення до серверу");
        //        }
        //        return await _contoller.GetSetting(token);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}
        //public async Task ConnectDataBase(string nameDataBase, string passwordDataBase, string password, ConnectionType  typeConnect, TypeDataBase typeDataBase)
        //{
        //    try
        //    {
        //        if (_contoller == null)
        //        {
        //            throw new Exception("Перевірте підключення до серверу");
        //        }
        //        SettingsDataBase settings = new SettingsDataBase()
        //        {

        //            PasswordDataBase = passwordDataBase,
        //            ConnectionString = new ConnectionString()
        //            {
        //                ConnectionType = typeConnect,
        //                InitialCatalog = nameDataBase,
        //                IntegratedSecurity = true,
        //            },
        //            TypeDataBase = typeDataBase
        //        };
        //        await _contoller.ConnectDataBase(settings, password);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public async Task DisconnectDataBase(string token)
        //{
        //    try
        //    {
        //        await _contoller.DisconnectDataBase(token);
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}
