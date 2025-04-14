using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Entities;
using ShopProjectWebServer.DataBase.HelperModel;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class UserRoleTableAccess : IUserRoleTableAccess<UserRoleEntity>
    {
        private string _connectionString;
        public UserRoleTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(UserRoleEntity item)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserRoleEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRoleEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.UserRoles.Load();
                    if (context.Users.Count() != 0)
                    {
                        return context.UserRoles.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        public void Update(UserRoleEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
