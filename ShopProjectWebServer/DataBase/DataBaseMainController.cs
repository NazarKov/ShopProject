using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Entities;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.Helpers.Settings;

namespace ShopProjectWebServer.DataBase
{
    public static class DataBaseMainController
    {
        private static Settings _settings;
        private static FileSettingManager _settingManager;

        private static IDataAccess<CodeUKTZEDEntity, DiscountEntity, GiftCertificatesEntity, ObjectOwnerEntity, OperationsRecorderEntity, OperationsRecorderUserEntity, OperationEntity,
        OrderEntity, ProductEntity, ProductUnitEntity, UserRoleEntity, UserEntity,TokenEntity> _dataBaseAccess;

        public static IDataAccess<CodeUKTZEDEntity, DiscountEntity, GiftCertificatesEntity, ObjectOwnerEntity, OperationsRecorderEntity, OperationsRecorderUserEntity, OperationEntity,
        OrderEntity, ProductEntity, ProductUnitEntity, UserRoleEntity, UserEntity,TokenEntity> DataBaseAccess
        {
            set { _dataBaseAccess = value; }
            get { return _dataBaseAccess; }
        }
        public static void init()
        {
            _settingManager = new FileSettingManager();

            _settings = _settingManager.ReadJson();

            if (_settings.SettingConnect != null)
            {
                _dataBaseAccess = new DataBaseSQLAccess(_settings.SettingConnect.ConnectionString.ToString());
                //pattern singelton
            }
        }

        public static void Create(string nameDataBase, string password, string typeDataBase, string typeConnect)
        {
            SettingDataBase settingConnect = new SettingDataBase();
            settingConnect.Name = nameDataBase;
            settingConnect.PasswordDataBase = password;


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
                        settingConnect.ConnectionString = new ConnectionString()
                        {
                            DataSource = Environment.MachineName,
                            IntegratedSecurity = true,
                            InitialCatalog = nameDataBase,
                        };
                        switch ((Enum.Parse(typeof(TypeConnectDataBase), typeConnect, true)))
                        {
                            case TypeConnectDataBase.None:
                                {
                                    settingConnect.ConnectionString.TypeDataBase = TypeConnectDataBase.None;
                                    break;
                                }
                            case TypeConnectDataBase.DEVELEPER:
                                {
                                    settingConnect.ConnectionString.TypeDataBase = TypeConnectDataBase.DEVELEPER;
                                    break;
                                }
                            case TypeConnectDataBase.SQLEXPRESS:
                                {
                                    settingConnect.ConnectionString.TypeDataBase = TypeConnectDataBase.SQLEXPRESS;
                                    break;
                                }
                        }


                        _dataBaseAccess = new DataBaseSQLAccess(settingConnect.ConnectionString.ToString());
                        _dataBaseAccess.Create(settingConnect.ConnectionString.ToString());
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

        }

        public static SettingDataBase GetInfo()
        {
            return _settings.SettingConnect;
        }
    }
}
