namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDataBaseOperation
    {
        public Task<bool> Сonnection(string connectionString);
    }
}
