using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Options;
using ShopProjectDataBase.Context;
using ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.Helpers.Settings;
using System.Threading.Tasks;

namespace ShopProjectWebServer.DataBase
{
    public class DataBaseMainController 
    {
        private Settings _settings;
        private FileSettingManager _settingManager; 

        private IDataAccess? _dataBaseAccess;
        private IDatabaseInitializer? _databaseInitializer;
        private IDataBaseSecurityService? _securityService;
        private ICreateDataBaseServise? _createDataBaseServise;
        private IDataBaseOperation? _dataBaseOperation; 

        public IDataAccess?  DataBaseAccess
        {
            set { _dataBaseAccess = value; }
            get { return _dataBaseAccess; }
        }
        
        public DataBaseMainController()
        {
            _settings = new Settings();
            _settingManager = new FileSettingManager(); 
            SetSettings(); 
            SetContext();
        }
        private void SetSettings()
        {
            _settings = _settingManager.ReadJson();
        }

        private void SetContext()
        { 
            if(_settings.SettingConnect!=null && _settings.SettingConnect.ConnectionString != string.Empty && _settings.SettingConnect.ConnectionString != "")
            { 
                switch (_settings.SettingConnect.TypeDataBase)
                {
                    case TypeDataBase.None:
                        {
                            break;
                        }
                    case TypeDataBase.SQL:
                        {
                            var optionsBuilder = new DbContextOptionsBuilder<ContextDataBase>();
                            optionsBuilder.UseSqlServer(_settings.SettingConnect.ConnectionString.ToString());
                            ContextDataBase contextDataBase = new ContextDataBase(optionsBuilder.Options);
                            
                            _dataBaseAccess = new DataBaseSQLAccess(contextDataBase);
                            _databaseInitializer = new DatabaseSqlInitializer(contextDataBase);
                            _createDataBaseServise = new DataBaseCreateSqlServise();
                            _dataBaseOperation = new DataBaseSqlOperation();
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
                        _dataBaseOperation = new DataBaseSqlOperation(); 
                        return await _dataBaseOperation.Сonnection(connectionString.ToString()); 
                    } 
                case TypeDataBase.File:
                    {
                        break;
                    }
            }

            return false;
        } 


        public async Task Create(string typeDataBase, string nameDataBase , string login, string password , ConnectionString connectionString)
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
                        _createDataBaseServise = new DataBaseCreateSqlServise();
                        _databaseInitializer = new DatabaseSqlInitializer();
                        _dataBaseOperation = new DataBaseSqlOperation();
                        _securityService = new DatabaseSQLSecurityService(connectionString.ToString());
                        isCreate = await _createDataBaseServise.CreateDataBase(_databaseInitializer, _dataBaseOperation, _securityService, login, password, nameDataBase, connectionString);  
                        break;
                    }
                case TypeDataBase.File:
                    {
                        break;
                    }
            }

            if (isCreate)
            {
                SettingDataBase settingConnect = new SettingDataBase()
                {
                    ConnectionString = connectionString.ToString(),
                    TypeDataBase = Enum.Parse<TypeDataBase>(typeDataBase)
                };
                
                var setting = _settingManager.ReadJson();
                if (setting != null)
                {
                    setting.SettingConnect = settingConnect;
                    _settingManager.SaveJson<Settings>(setting);
                }
            } 
        }

        public  SettingDataBase GetInfo()
        {
            return _settings.SettingConnect;
        }
    }
}
