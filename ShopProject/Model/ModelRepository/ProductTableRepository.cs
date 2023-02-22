using NPOI.SS.Formula.Functions;
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
    class ProductTableRepository : ITableRepository<Product>
    {
        public ProductTableRepository(){}
       
        public void Add(Product product)
        {
            using (ShopContext context = new ShopContext())
            {
                context.products.Load();
                if(context.products!=null)
                    context.products.Add(product);
                context.SaveChanges();
            }
        }
        public void Update(Product product)
        {
            using(ShopContext context = new ShopContext())
            {
                context.products.Load();
                if (context.products != null)
                {
                    if (product != null)
                    {
                        UpdateFieldProduct(context.products.Find(product.ID), product);
                    }
                    else
                    {
                        throw new Exception("Товар не знайдено");
                    }
                }
                context.SaveChanges();
            }
        }
        private void UpdateFieldProduct(Product productUpdate,Product product)
        {
            productUpdate.code = product.code;
            productUpdate.price = product.price;
            productUpdate.articule = product.articule;
            productUpdate.units = product.units;
            productUpdate.count = product.count;
            productUpdate.sales = product.sales;
            productUpdate.name = product.name;
        }
        
        public void Delete(Product item)
        {
            using (ShopContext context = new ShopContext())
            {
                context.products.Load();
                if (context.products != null)
                {
                    var product = context.products.Find(item.ID);

                    if (product != null)
                    {
                        context.products.Remove(product);
                    }
                    else
                    {
                        throw new Exception("Товар не знайдено");
                    }
                }
                context.SaveChanges();
            }
        }

        public object GetId(int i)
        {
            using (ShopContext context = new ShopContext())
            {
                context.products.Load();
                if (context.products != null)
                {
                    return context.products.Find(i);
                }
                else
                {
                    throw new Exception("Товар не знайдено");
                }
            }
        }
        public IEnumerable<object> GetAll()
        {
            using (ShopContext context = new ShopContext())
            {
                context.products.Load();
                if(context.products!=null)
                {
                    return context.products.Where( p=> p.status == "in_stock").ToList();//in_stock товар готовий до продажі
                }
                else
                {
                    throw new Exception("База даних пуста");
                }
            }
        }
    
    }
}
