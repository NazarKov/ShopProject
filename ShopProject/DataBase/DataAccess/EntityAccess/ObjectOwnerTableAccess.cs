using ShopProject.DataBase.Context;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class ObjectOwnerTableAccess : IEntityAccess<ObjectOwnerEntiti>
    {
        public void Add(ObjectOwnerEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                    if (item != null)
                    {
                        context.ObjectOwners.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }
        public void Delete(ObjectOwnerEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                    if (context.ObjectOwners != null)
                    {
                        if (item != null)
                        {
                            context.ObjectOwners.Remove(context.ObjectOwners.Find(item.ID));
                        }
                        else
                        {
                            throw new Exception("Товар не знайдено");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
        public IEnumerable<ObjectOwnerEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.ObjectOwners.Load();
                    if (context.ObjectOwners.Count() != 0)
                    {
                        return context.ObjectOwners.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }
        public void Update(ObjectOwnerEntiti item)
        {
            throw new NotImplementedException();
        }
    }
}
