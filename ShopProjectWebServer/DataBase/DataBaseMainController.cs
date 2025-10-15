 using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.Helpers.Settings;

namespace ShopProjectWebServer.DataBase
{
    public static class DataBaseMainController // прибрати static винести налашування 
    {
        private static Settings _settings;
        private static FileSettingManager _settingManager;

        private static IDataAccess _dataBaseAccess;

        public static IDataAccess  DataBaseAccess
        {
            set { _dataBaseAccess = value; }
            get { return _dataBaseAccess; }
        }
        public static void Init()
        {
            _settingManager = new FileSettingManager();

            _settings = _settingManager.ReadJson();

            if (_settings.SettingConnect != null)
            {
                _dataBaseAccess = new DataBaseSQLAccess(_settings.SettingConnect.ConnectionString.ToString());
                
            }
        }

        public static void Create(string nameDataBase, string password, string typeDataBase, string typeConnect)
        {
            SettingDataBase settingConnect = new SettingDataBase();
            ConnectionString connectionString = new ConnectionString();

            switch (Enum.Parse(typeof(TypeDataBase), typeDataBase, true))
            {
                case TypeDataBase.None:
                    {
                        settingConnect.TypeDataBase = TypeDataBase.None;
                        break;
                    }
                case TypeDataBase.SQL:
                    {
                        settingConnect.TypeDataBase = TypeDataBase.SQL;
                        connectionString = new ConnectionString()
                        {
                            Server = Environment.MachineName,
                            TrustServerCertificate = true,
                            DataBaseName = nameDataBase,
                            UserName = "ShopAdmin",
                            Password = password
                        };
                        switch ((Enum.Parse(typeof(TypeConnectDataBase), typeConnect, true)))
                        {
                            case TypeConnectDataBase.None:
                                {
                                    connectionString.TypeDataBase = TypeConnectDataBase.None;
                                    break;
                                }
                            case TypeConnectDataBase.DEVELEPER:
                                {
                                    connectionString.TypeDataBase = TypeConnectDataBase.DEVELEPER;
                                    break;
                                }
                            case TypeConnectDataBase.SQLEXPRESS:
                                {
                                    connectionString.TypeDataBase = TypeConnectDataBase.SQLEXPRESS;
                                    break;
                                }
                        }
                        settingConnect.ConnectionString = connectionString.ToString();
                        break;
                    }
                case TypeDataBase.File:
                    {
                        break;
                    }
            }

            var setting = _settingManager.ReadJson();
            if (setting != null)
            {
                setting.SettingConnect = settingConnect;
                _settingManager.SaveJson<Settings>(setting);
            }

            //_dataBaseAccess = new DataBaseSQLAccess(settingConnect.ConnectionString.ToString());
        }

        public static SettingDataBase GetInfo()
        {
            return _settings.SettingConnect;
        }
    }
}
