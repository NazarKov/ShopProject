using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.ModelRepository
{
    internal class ArhiveTableRepositories : ITableRepository<ProductArchive>
    {
        public ArhiveTableRepositories(){}

        public void Add(ProductArchive productArhive)
        {
            using (ShopContext context = new ShopContext())
            {
                context.productArchives.Load();
                context.products.Load();

                if (context.productArchives != null && productArhive.ID != 0)
                {
                    if (productArhive != null)
                    {
                        context.productArchives.Add(productArhive); ;
                    }
                }
                context.SaveChanges();
            }
        }
     
        //public void Update(object item)//не реалізований в програмі
        //{
        //    ProductArchive product = (ProductArchive)item;
        //    using (ShopContext context = new ShopContext())
        //    {
        //        context.productArchives.Load();
        //        if (context.productArchives != null)
        //        {
        //            if (product != null)
        //            {
        //                UpdateFieldProduct(context.productArchives.Find(product.ID), product);
        //            }
        //            else
        //            {
        //                throw new Exception("Товар не знайдено");
        //            }
        //        }
        //        context.SaveChanges();
        //    }
        //}
        //private void UpdateFieldProduct(ProductArchive productUpdate, ProductArchive product)
        //{
        //    productUpdate.code = product.code;
        //    productUpdate.price = product.price;
        //    productUpdate.articule = product.articule;
        //    productUpdate.units = product.units;
        //    productUpdate.count = product.count;
        //    productUpdate.sales = product.sales;
        //    productUpdate.name = product.name;
        //}

        public void Delete(ProductArchive item)
        {
            using (ShopContext context = new ShopContext())
            {
                context.products.Load();
                context.productArchives.Load();
                if (context.productArchives != null)
                {
                    var productArchive = context.productArchives.Find(item.ID);

                    if (productArchive != null)
                    {
                        context.productArchives.Remove(productArchive);
                        //if (productArchive.ID != null)
                            //if(context.products!=null)
                           // context.products.Remove(productArchive);
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
                context.productArchives.Load();
                if (context.productArchives != null)
                {
                    return context.productArchives.Find(i);
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
                context.productArchives.Load();
                if (context.productArchives != null)
                {
                    return context.productArchives.ToList();
                }
                else
                {
                    throw new Exception("База даних пуста");
                }
            }
        }
    }
}
