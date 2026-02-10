using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{ 
    public interface ICreateDataBaseServise
    {
        public Task<bool> CreateDataBase(IDatabaseInitializer databaseInitializer, IDataBaseOperation dataBaseOperation, IDataBaseSecurityService dataBaseSecurityService,
            string login, string password, string nameDataBase, ConnectionString connectionString); 

        public Task<bool> CanConnect(IDataBaseOperation dataBaseSqlOperation);

        public string GetConnectionString();

    }
}
