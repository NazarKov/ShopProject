using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class OperationTableAccess : IEntityAccess<OperationEntiti>
    {
        public void Add(OperationEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.Operations.Load();
                    context.Users.Load();
                    if (item != null)
                    {
                        item.User = context.Users.Find(item.User.ID);
                        context.Operations.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }
        public void Delete(OperationEntiti item)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<OperationEntiti> GetAll()
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.Products.Load();
                    context.Operations.Load();
                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.ToList();
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                return null;
            }
        }
        public void Update(OperationEntiti item)
        {
            throw new NotImplementedException();
        }
    }
}
