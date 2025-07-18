using ShopProjectSQLDataBase.Entities;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface ITokenTableAccess 
    {
        void Add(TokenEntity item);
        void Update(TokenEntity item);
        void Delete(TokenEntity item);
        IEnumerable<TokenEntity> GetAll();
    }
}
