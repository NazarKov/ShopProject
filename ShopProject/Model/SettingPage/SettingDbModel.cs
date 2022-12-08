using ShopProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopProject.Model.SettingPage
{
    internal class SettingDbModel 
    {
        XmlSerializer xmlSerializer;
        public ResourceModel setting;

        public SettingDbModel()
        {
            setting = new ResourceModel();
            xmlSerializer = new XmlSerializer(typeof(ResourceModel));
        }

        public bool ConnectionDB(string dbname)
        {
            try
            {
                Configuration configuration = null;
                ConnectionStringSettings connectionString = null;

                configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                for (int i = 0; i < configuration.ConnectionStrings.ConnectionStrings.Count; i++)
                {
                    configuration.ConnectionStrings.ConnectionStrings.RemoveAt(i);
                }
                connectionString = new ConnectionStringSettings();

                //    connectionString.ConnectionString = "Data Source=" + Environment.MachineName + "\\SQLExpress;Initial Catalog=Shop;Integrated Security=True";

                connectionString.ConnectionString = "Data Source=" + Environment.MachineName + ";Initial Catalog=" + dbname + ";Integrated Security=True";

                connectionString.Name = "DBConnectionMyString";
                connectionString.ProviderName = "System.Data.SqlClient";
                configuration.ConnectionStrings.ConnectionStrings.Add(connectionString);
             
                configuration.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings");

                return true;
            }
            catch
            {
                return false;
            }
            return false;
        }


        public void SerializableResurses()
        {
            xmlSerializer = new XmlSerializer(typeof(ResourceModel));
            using (FileStream fs = new FileStream(@"..\..\..\Resource\appSetting.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, setting);
            }
        }
        public void DerializableResurses()
        {
            using (FileStream fs = new FileStream(@"..\..\..\Resource\appSetting.xml", FileMode.OpenOrCreate))
            {
                setting = (ResourceModel)xmlSerializer.Deserialize(fs);
            }
        }

        public void RemoveSetting()
        {
            setting = new ResourceModel();
            SerializableResurses();
        }

    }
}
