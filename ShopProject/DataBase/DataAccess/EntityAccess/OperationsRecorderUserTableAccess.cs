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
    internal class OperationsRecorderUserTableAccess : IEntityAccess<OperationsRecorderUserEntiti>
    {
        public void Add(OperationsRecorderUserEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.Users.Load();
                    context.OperationsRecorderUsers.Load();
                    if (item != null)
                    {
                        item.Users = context.Users.Find(item.Users.ID);
                        item.OpertionsRecorders = context.OperationsRecorders.Find(item.OpertionsRecorders.ID);

                        context.OperationsRecorderUsers.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(OperationsRecorderUserEntiti item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationsRecorderUserEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    context.Users.Load();
                    context.OperationsRecorderUsers.Load();
                    context.ObjectOwners.Load();
                    if (context.OperationsRecorders.Count() != 0)
                    {
                        return context.OperationsRecorderUsers.ToList();
                    }
                    else
                    {
                        throw new Exception("Немає доступних РРО.\nДобавте або налаштуйте нове РРО");
                    }
                }
                return null;
            }
        }

        public void Update(OperationsRecorderUserEntiti item)
        {
            throw new NotImplementedException();
        }
    }
}
