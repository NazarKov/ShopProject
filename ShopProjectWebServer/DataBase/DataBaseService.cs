using Microsoft.EntityFrameworkCore; 
using ShopProjectDataBase.Context;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Helpers.Enum;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface; 
using ShopProjectWebServer.Models.Domain.Setting; 
using ShopProjectWebServer.Service.Modules.Setting.Interface; 

namespace ShopProjectWebServer.DataBase
{
    public class DataBaseService : IDataBaseService
    {  
        private IDataAccess _dataBaseAccess;
        private IDataBaseInitializer _databaseInitializer;
        private IDataBaseSecurityService _securityService; 
        private IDataBaseOperationServise _dataBaseOperation; 

        public IDataAccess  DataBaseAccess
        {
            set { _dataBaseAccess = value; }
            get { return _dataBaseAccess; }
        }
        private ISettingService _settingService;
        private SettingDataBaseConnection _settingDataBaseConnection;
        
        public DataBaseService(ISettingService settingService)
        {
            _settingService = settingService;
            _settingDataBaseConnection = _settingService.GetSetting<SettingDataBaseConnection>(); 
            SetContext();
        } 

        private void SetContext()
        { 
            if(_settingDataBaseConnection != null && _settingDataBaseConnection.ConnectionString.DataBaseName != string.Empty && _settingDataBaseConnection.ConnectionString.DataBaseName != "")
            { 
                switch (_settingDataBaseConnection.TypeDataBase)
                {
                    case TypeDataBase.None:
                        {
                            break;
                        }
                    case TypeDataBase.SQL:
                        {
                            var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
                            optionsBuilder.UseSqlServer(_settingDataBaseConnection.ConnectionString.ToString(), 
                                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,                     
                                maxRetryDelay: TimeSpan.FromSeconds(10),
                                errorNumbersToAdd: null                
                                ));
                            ContextDataBase contextDataBase = new ContextDataBase(optionsBuilder.Options);
                            
                            _dataBaseAccess = new DataBaseSQLAccess(contextDataBase);
                            _databaseInitializer = new DatabaseSqlInitializer(contextDataBase); 
                            _dataBaseOperation = new SqlOperationServise();
                            break;
                        }
                    case TypeDataBase.File:
                        {
                            break;
                        } 
                }

            }

        }

        public async Task<bool> CreateConnectionSql(TypeDataBase typeDataBase, ConnectionString connectionString)
        {
            switch (typeDataBase) 
            {
                case TypeDataBase.None:
                    {
                        break;
                    }
                case TypeDataBase.SQL:
                    { 
                        _dataBaseOperation = new SqlOperationServise(); 
                        return await _dataBaseOperation.Сonnection(connectionString.ToString()); 
                    } 
                case TypeDataBase.File:
                    {
                        break;
                    }
            }

            return false;
        } 


        public async Task<bool> Create(string typeDataBase, string nameDataBase , string login, string password , ConnectionString connectionString)
        {  
            bool isCreate = false;

            switch (Enum.Parse<TypeDataBase>(typeDataBase))
            {
                case TypeDataBase.None:
                    {
                        break;
                    }
                case TypeDataBase.SQL:
                    { 
                        _databaseInitializer = new DatabaseSqlInitializer();
                        _dataBaseOperation = new SqlOperationServise();
                        _securityService = new SqlSecurityService(connectionString.ToString());
                        isCreate = await _databaseInitializer.CreateDataBase(_dataBaseOperation, _securityService, login, password, nameDataBase, connectionString);  
                        break;
                    }
                case TypeDataBase.File:
                    {
                        break;
                    }
            }

            if (isCreate)
            {
                SettingDataBaseConnection settingConnect = new SettingDataBaseConnection()
                {
                    ConnectionString = connectionString,
                    TypeDataBase = Enum.Parse<TypeDataBase>(typeDataBase)
                };

                _settingService.SetSetting<SettingDataBaseConnection>(settingConnect); 
            }

            return isCreate;
        } 
        public SettingDataBaseConnection GetSetting()
        {
            return _settingService.GetSetting<SettingDataBaseConnection>(); 
        }

        public async Task<bool> IsConnect()
        {
            return await _dataBaseOperation.Сonnection(_settingDataBaseConnection.ConnectionString.ToString());
        }
    }
}
