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
    public enum TypeParameterSetTableOutOfStock
    {
        create_at = 0,
        product = 1,
    }
    internal class OutOfStockTableRepositories : ITableRepository<ProductsOutOfStock,TypeParameterSetTableOutOfStock>
    {
        public void Add(ProductsOutOfStock product)
        {
            using (ShopContext context = new ShopContext())
            {
                context.products.Load();
                context.productsOutOfs.Load();

                if (context.productsOutOfs != null && product.ID != 0)
                {
                    if (product != null)
                    {
                        context.productsOutOfs.Add(product);
                    }
                }
                context.SaveChanges();
            }
        }
        public void Delete(ProductsOutOfStock product)
        {
            using(ShopContext context = new ShopContext())
            {
                context.productsOutOfs.Load();
                context.products.Load();
                if (context.productsOutOfs != null)
                {   
                    var item = context.productsOutOfs.Find(product.ID);
                    if(item != null)
                    {
                        context.productsOutOfs.Remove(item);
                    }
                    else
                    {
                        throw new Exception("товар не знайдено");
                    }
                }
                context.SaveChanges();
            }
        }
        public object GetBId(int id)
        {
            using(ShopContext context = new ShopContext())
            {
                context.productsOutOfs.Load();
                if (context.productsOutOfs != null)
                    return context.productsOutOfs.Find(id);
                else
                    throw new Exception("товар не знайдено");
            }
        }
        public IEnumerable<object> GetAll()
        {
            using(ShopContext context = new ShopContext())
            {
                context.products.Load();
                context.productsOutOfs.Load();
                if (context.productsOutOfs != null)
                    return context.productsOutOfs.ToList();
                else
                    throw new Exception("База даних пуста");
            }
        }

    }
}
