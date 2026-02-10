namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDataBaseSecurityService
    {
        public Task<bool> CreateLogin(string login, string password, string nameDataBase);
    }
}
