using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class TokenTableAccess : ITokenTableAccess 
    {

        private readonly ContextDataBase _contextDataBase;
        public TokenTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public void Add(TokenEntity item)
        { 
            if (_contextDataBase != null)
            { 
                if (item != null)
                {
                    item.User = _contextDataBase.Users.Find(item.User.ID);
                    _contextDataBase.UserTokens.Add(item);
                }
                _contextDataBase.SaveChanges(); 
            }
        }

        public void Delete(TokenEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TokenEntity> GetAll()
        {
            _contextDataBase.Users.Load();
            _contextDataBase.UserTokens.Load();

            if (_contextDataBase.Users.Count() != 0)
            {
                return _contextDataBase.UserTokens.ToList();
            }
            else
            {
                return null;
            }
        }

        public void Update(TokenEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
