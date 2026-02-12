namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface ISqlOperationServise
    {
        public Task<bool> Сonnection(string connectionString);
    }
}
