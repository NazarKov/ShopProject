using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.ModelRepository
{
    public enum TypeParameterSetTableOrder
    {

    }
    internal class OrderTableRepositories : ITableRepository<Operation, TypeParameterSetTableOrder>
    {
        public void Add(Operation order)
        {
            using(ShopContext context = new ShopContext())
            {
                context.orders.Load();
                if(context.orders!=null)
                    context.orders.Add(order);
                context.SaveChanges();
            }
        }
        public void Update(Operation order)
        {
            using (ShopContext context = new ShopContext())
            {
                context.orders.Load();
                if(context.orders!=null)
                {
                    if (order != null)
                    {
                        UpdateFieldOrder(context.orders.Find(order.ID),order);
                    }
                    else
                    {
                        throw new Exception("Помилка добавлення заказ пустий");
                    }
                }
                context.SaveChanges();
            }
        }
        private void UpdateFieldOrder(Operation updateOrder,Operation dataOrder)
        {
            updateOrder.sale=dataOrder.sale;
            updateOrder.rest = dataOrder.rest;
            updateOrder.user=dataOrder.user;
            updateOrder.suma=dataOrder.suma;
            updateOrder.created_at=dataOrder.created_at;
        }
        public void Delete(Operation order)
        {
            using(ShopContext context = new ShopContext())
            {
                context.orders.Load();
                if(context.orders!= null)
                    context.orders.Remove(order);
                context.SaveChanges();
            }
        }
        public object GetId(int id) {
            
            using (ShopContext context = new ShopContext())
            {
                context.orders.Load();
                if( context.orders!= null)
                {
                    return context.orders.Find(id);
                }
                else
                {
                    throw new Exception("База даних пуста");
                }

            }
        }
        public IEnumerable<object> GetAll()
        {

            using (ShopContext context = new ShopContext())
            {
                
                if (context.orders.Count() != 0)
                {
                    context.orders.Load();
                    return context.orders.ToList();
                }
                else
                {
                    return null;
                }

            }
        }

    }
}
