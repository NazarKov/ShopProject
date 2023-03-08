using Microsoft.EntityFrameworkCore.Infrastructure;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesContextDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.DataBase
{
    internal class DataContext : IContextDataBase
    {
        public DataContext() { }

        public void Create()
        {
            using (ShopContext context = new ShopContext())
            {
                context.Database.CreateIfNotExists();
                context.SaveChanges();
            }
        }
        public void Delete()
        {
            using(ShopContext context = new ShopContext())
            {
                context.Database.Delete();
            }
        }
        public void Clear()
        {
            using(ShopContext context = new ShopContext())
            {
                context.productOrders.RemoveRange(context.productOrders);
                context.productArchives.RemoveRange(context.productArchives);
                context.productsOutOfs.RemoveRange(context.productsOutOfs);
                context.orders.RemoveRange(context.orders);
                context.products.RemoveRange(context.products);

                context.user.RemoveRange(context.user);
                context.cashRegisters.RemoveRange(context.cashRegisters);
                context.GiftCertificates.RemoveRange(context.GiftCertificates);
                
                context.SaveChanges();
            }
        }
        
    }
}
