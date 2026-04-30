namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDataBaseOperationServise
    {
        public Task<bool> Сonnection(string connectionString);
    }
}
