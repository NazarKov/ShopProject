using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDataBaseInitializer
    {
        public Task<bool> CreateDataBase(ISqlOperationServise dataBaseOperation, ISqlSecurityService dataBaseSecurityService,
            string login, string password, string nameDataBase, ConnectionString connectionString); 
        public Task<bool> IsCreate();  
        public void Clear();

        public Task<bool> Connection();
        public void Delete();
    }
}
