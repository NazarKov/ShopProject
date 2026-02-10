using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class UserRoleTableAccess : IUserRoleTableAccess 
    {

        private readonly ContextDataBase _contextDataBase;

        public UserRoleTableAccess(ContextDataBase  contextDataBase)
        {
            _contextDataBase = contextDataBase;
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
            _contextDataBase.UserRoles.Load();
            if (_contextDataBase.Users.Count() != 0)
            {
                return _contextDataBase.UserRoles.ToList();
            }
            else
            {
                return null;
            }
        }

        public void Update(UserRoleEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
