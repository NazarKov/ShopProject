using ShopProject.DataBase.Context;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    class UserRoleTableAccess : IEntityAccess<UserRoleEntiti>
    {
        public void Add(UserRoleEntiti item)
        {
            throw new NotImplementedException();
        }
        public void Delete(UserRoleEntiti item)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserRoleEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
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
        public void Update(UserRoleEntiti item)
        {
            throw new NotImplementedException();
        }
    }
}
