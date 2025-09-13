using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class UserRoleTableAccess : IUserRoleTableAccess 
    {
        private DbContextOptions<ContextDataBase> _option;
        public UserRoleTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
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
            using (ContextDataBase context = new ContextDataBase(_option))
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
