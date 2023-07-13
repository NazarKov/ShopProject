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
    public enum TypeParameterSetTableOrderXML
    {

    }
    internal class OrderXMLTableRepositories    : ITableRepository<OrderXML,TypeParameterSetTableOrderXML>
    {
        public void Add(OrderXML orderXML)
        {

            using(ShopContext context = new ShopContext())
            {
                context.orderXML.Load();
                context.orderXML.Add(orderXML);
                context.SaveChanges();
            }
        }

        public OrderXML LastXML()
        {
            using( ShopContext context = new ShopContext())
            {
                context.orderXML.Load();
                if (context.orderXML.Count() != 0)
                {
                    return context.orderXML.OrderByDescending(item => item.ID).First();
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
