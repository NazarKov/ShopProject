using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopProject.Model.ModelRepository
{
    public enum TypeParameterSetTableProductArhive
    {
        create_at = 0,
        product = 1,
    }
    internal class ArhiveTableRepositories : ITableRepository<ProductArchive, TypeParameterSetTableProductArhive>
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
                context.products.Load();
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
