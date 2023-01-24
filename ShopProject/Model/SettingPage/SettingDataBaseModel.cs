using ShopProject.DataBase.Context;
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
        private XmlSerializer xmlSerializer;
        private ResourceModel setting;
        private ShopContext? db;
        private Configuration configuration;
        private ConnectionStringSettings? connectionString;

        private string? nameDataBase;

        public SettingDataBaseModel()
        {
            setting = new ResourceModel();
            db = new ShopContext();
            xmlSerializer = new XmlSerializer(typeof(ResourceModel));
            configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            FileOpenOptionsDataBase();
            this.nameDataBase = setting.NameDb;
        }

        public bool CreateDataBase()
        {
            try
            {
                if (db != null)
                {
                    db.Database.Create();
                    return true;
                }
            }
            catch (Exception ex)
            {
                HendlerShowExeption(ex); return false;
            }
            return false;
        }

        public bool DeleteDataBase()
        {
            try
            {
                if (db != null)
                {
                    return db.Database.Delete();
                }
            }
            catch(Exception ex)
            {
                HendlerShowExeption(ex);
                return false;
            }
            return false;
        }
      
        private void HendlerShowExeption(Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);

        }

        public void SetName(string name)
        {
            this.nameDataBase = name;
            setting.SetNameDb(name);
        }

        public string GetName()
        {
            if (nameDataBase != null)
                return nameDataBase;
            else
                return string.Empty;
        }
        
        public bool SetConnectionStringDataBase()
        {
            try
            {        
                RemoveConnectingString(configuration);
                
                configuration.ConnectionStrings.ConnectionStrings.Add(CreateConnectionString());
                configuration.Save(ConfigurationSaveMode.Modified, true);

                ConfigurationManager.RefreshSection("connectionStrings");

                return true;
            }
            catch(Exception ex)
            {
                HendlerShowExeption(ex);
                return false;
            }
        }

        private void RemoveConnectingString(Configuration configuration)
        {
            for (int i = 0, maxCount = configuration.ConnectionStrings.ConnectionStrings.Count; i < maxCount-1; i++)
            {
                configuration.ConnectionStrings.ConnectionStrings.RemoveAt(i);
            }
            if (configuration.ConnectionStrings.ConnectionStrings.Count != 0)
            {
                configuration.ConnectionStrings.ConnectionStrings.RemoveAt(0);
            }
        }
        
        private ConnectionStringSettings CreateConnectionString()
        {
            connectionString = new ConnectionStringSettings();
            connectionString.ConnectionString = "Data Source=" + Environment.MachineName + ";Initial Catalog=" + this.nameDataBase + ";Integrated Security=True";
            connectionString.Name = "DBConnectionMyString";
            connectionString.ProviderName = "System.Data.SqlClient";

            return connectionString;
        }

        public void FileSaveOptionsDataBase()
        {
            using (FileStream fs = new FileStream(@"..\..\..\Resource\appSetting.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, setting);
            }
        }
       
        private void FileOpenOptionsDataBase()
        {
            using (FileStream fs = new FileStream(@"..\..\..\Resource\appSetting.xml", FileMode.OpenOrCreate))
            {
                setting = (ResourceModel)xmlSerializer.Deserialize(fs);
            }
        }

        public void RemoveSetting()
        {
            try
            {
                setting = new ResourceModel();
                FileSaveOptionsDataBase();
                RemoveConnectingString(configuration);

            }
            catch (Exception ex)
            {
                HendlerShowExeption(ex);
            }
        }

    }
}
