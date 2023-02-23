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
    internal class ArhiveTableRepositories : ITableRepository<ProductArchive>
    {
        private ITableRepository<Product> _productTableRepository;
        public ArhiveTableRepositories()
        {
            _productTableRepository = new ProductTableRepository();
        }

        public void Add(ProductArchive productArhive)
        {
            using (ShopContext context = new ShopContext())
            {
                context.productArchives.Load();
               
                ProductArchive archive = new ProductArchive();
                SetFiledAthive(archive, productArhive);

                if (context.productArchives != null)
                {
                    if (productArhive != null&&productArhive.product!=null)
                    {  
                        context.productArchives.Add(archive);
                    }
                }
                context.SaveChanges();
            }
        }
        private void SetFiledAthive(ProductArchive archive, ProductArchive productArchive)
        {
            archive.product = (Product)_productTableRepository.GetId(productArchive.product.ID);
            archive.created_at = DateTime.Now;
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
                        if (productArchive.product!=null)
                            if(context.products!=null)
                            context.products.Remove(productArchive.product);
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
