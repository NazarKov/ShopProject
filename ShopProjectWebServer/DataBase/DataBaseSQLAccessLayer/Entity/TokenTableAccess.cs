using ShopProjectSQLDataBase.Context;
using ShopProjectSQLDataBase.Entities; 
using ShopProjectWebServer.DataBase.Helpers;        
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class TokenTableAccess : ITokenTableAccess 
    {
        private string _connectionString;
        public TokenTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(TokenEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.UserRoles.Load();
                    context.UserTokens.Load();

                    if (item != null)
                    {
                        item.User = context.Users.Find(item.User.ID);
                        context.UserTokens.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(TokenEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TokenEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                { 
                    context.Users.Load();
                    context.UserTokens.Load();

                    if (context.Users.Count() != 0)
                    { 
                        return context.UserTokens.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        public void Update(TokenEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
