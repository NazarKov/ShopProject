using ShopProject.DataBase.Context;
using ShopProject.DataBase.Entities;
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
    internal class UserTableAccess : IEntityAccess<UserEntiti>, IEntityAdd<UserEntiti>, IEntityGet<UserEntiti> , IEntityUpdate<UserEntiti>
    {
        public void Add(UserEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.UserRoles.Load();
                    if (item != null)
                    {
                        item.UserRole = context.UserRoles.Find(item.UserRole.ID);
                        context.Users.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void AddRange(List<UserEntiti> items)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Users.Load();
                    if (context.Users != null)
                    {
                        foreach (UserEntiti item in items)
                        {
                            context.Users.Add(item);
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Delete(UserEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Users.Load();
                    if (context.Users != null)
                    {
                        if (item != null)
                        {
                            context.Users.Remove(context.Users.Find(item.ID));
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

        public IEnumerable<UserEntiti> GetAll(string statusGoods)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.UserRoles.Load();
                    context.ObjectOwners.Load();
                    context.OperationsRecorders.Load();
                    context.Users.Load();
                    if (context.Users.Count() != 0)
                    {
                        var itms = context.OperationsRecorders.ToList();
                        var item = context.Users.ToList();
                        return context.Users.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
        }

        public UserEntiti GetByBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public UserEntiti GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserEntiti GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public UserEntiti GetItemId(Guid id)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Users.Load();
                    if (context.Users != null)
                    {
                        if (context.Users.Count() == 0)
                        {
                            throw new Exception("База даних не містить користувачів");
                        }
                        return context.Users.Find(id);
                    }
                    else
                    {
                        throw new Exception("Товар не знайдено");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        public void Update(UserEntiti item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Users.Load();
                    context.OperationsRecorders.Load();
                    var user = context.Users.Find(id);
                    switch (nameParameter)
                    {
                        //case nameof(user.Password):
                        //    {
                        //        user.Password = valueParameter.ToString();
                        //        break;
                        //    }
                        //case nameof(user.operationsRecorders):
                        //    {
                        //        if(user.operationsRecorders == null)
                        //        {
                        //            user.operationsRecorders = new List<OperationsRecorderEntiti>();
                        //        }
                        //        user.operationsRecorders.Add(context.OperationsRecorders.Find(((OperationsRecorderEntiti)valueParameter).ID));
                        //        break;
                        //    }
                    }

                }
                context.SaveChanges();
            }
        }

        public void UpdateRange(List<UserEntiti> items)
        {
            throw new NotImplementedException();
        }
    }
}
