using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class OrderTableAccess : IEntityAccess<OrderEntiti>
    {
        public void Add(OrderEntiti item)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Operations.Load();
                    context.Products.Load();
                    context.Orders.Load();
                    if (item != null)
                    {
                        context.Orders.Add(new OrderEntiti()
                        {
                            Count = item.Count,
                            Goods = context.Products.Find(item.Goods.ID),
                            Operation = context.Operations.Find(item.Operation.ID),
                        });
                    }
                context.SaveChanges();
                }
            }
        }
        public void Update(OrderEntiti item)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderEntiti item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderEntiti> GetAll()
        {
            using(ContextDataBase  context = new ContextDataBase())
            {
                if(context!= null)
                {
                    context.Products.Load();
                    context.Operations.Load();
                    context.Orders.Load();
                    if(context.Orders.Count()!=0)
                    {
                        return context.Orders.ToList();
                    }
                }
                return new List<OrderEntiti>();
            }
        }
    }
}
