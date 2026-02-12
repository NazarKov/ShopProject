namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface ISqlSecurityService
    {
        public Task<bool> CreateLogin(string login, string password, string nameDataBase);
    }
}
