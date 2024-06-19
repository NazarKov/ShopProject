using ShopProject.DataBase.Context;
using ShopProject.DataBase.Entities;
using ShopProject.DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class OperationsRecorderTableAccess : IEntityAccess<OperationsRecorderEntiti> ,IEntityUpdate<OperationsRecorderEntiti>
    {
        public void Add(OperationsRecorderEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    if (item != null)
                    {
                        context.OperationsRecorders.Add(item);
                    }
                }
                context.SaveChanges();
            }
        }

        public void Delete(OperationsRecorderEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    if (context.OperationsRecorders != null)
                    {
                        if (item != null)
                        {
                            context.OperationsRecorders.Remove(context.OperationsRecorders.Find(item.ID));
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

        public IEnumerable<OperationsRecorderEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.OperationsRecorders.Load();
                    if (context.OperationsRecorders.Count() != 0)
                    {
                        return context.OperationsRecorders.ToList();
                    }
                    else
                    {
                        throw new Exception("Немає доступних РРО.\nДобавте або налаштуйте нове РРО");
                    }
                }
                return null;
            }
        }

        public void Update(OperationsRecorderEntiti item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context != null)
                {
                    context.OperationsRecorders.Load();
                    context.ObjectOwners.Load();
                    var item = context.OperationsRecorders.Find(id);
                    if (item != null)
                    {
                        switch(nameParameter)
                        {
                            case nameof(item.ObjectOwner):
                                {
                                    var obj = context.ObjectOwners.Find(valueParameter);
                                    if (obj != null)
                                    {
                                        item.ObjectOwner = obj;
                                    }
                                    break;
                                }
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        public void UpdateRange(List<OperationsRecorderEntiti> items)
        {
            throw new NotImplementedException();
        }
    }
}
