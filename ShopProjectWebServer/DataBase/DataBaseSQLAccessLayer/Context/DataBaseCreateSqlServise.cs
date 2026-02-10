using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Context
{
    public class DataBaseCreateSqlServise : ICreateDataBaseServise
    {
        private ConnectionString _connectionString;

        public DataBaseCreateSqlServise()
        {
            _connectionString = new ConnectionString();
        }

        public async Task<bool> CreateDataBase(IDatabaseInitializer databaseInitializer , IDataBaseOperation dataBaseOperation , IDataBaseSecurityService dataBaseSecurityService ,
            string login,string password,string nameDataBase , ConnectionString connectionString)
        {
            connectionString.DataBaseName = nameDataBase;
            if(await databaseInitializer.Create(connectionString.ToString()))
            {
                if (await dataBaseSecurityService.CreateLogin(login, password, nameDataBase)) 
                {
                    connectionString.UserName = login;
                    connectionString.Password = password;
                    return await databaseInitializer.Connection();
                }
            } 
            return false; 
        } 

        public async Task<bool> CanConnect(IDataBaseOperation dataBaseSqlOperation)
        {
            if (dataBaseSqlOperation != null)
            {
                return await dataBaseSqlOperation.Сonnection(_connectionString.ToString());
            }
            else
            {
                return false;
            }
        }

        public string GetConnectionString()
        {
            return _connectionString.ToString();
        }
    }
}
