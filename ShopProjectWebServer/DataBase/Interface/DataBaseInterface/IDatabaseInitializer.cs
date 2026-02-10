namespace ShopProjectWebServer.DataBase.Interface.DataBaseInterface
{
    public interface IDatabaseInitializer
    {
        public Task<bool> Create(string connectionString);
        public Task<bool> IsCreate();  
        public void Clear();

        public Task<bool> Connection();
        public void Delete();
    }
}
