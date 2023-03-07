using ShopProject.DataBase.Context;
using ShopProject.Interfaces.InterfacesContextDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
