using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Linq;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductTableAccess : IProductTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public ProductTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public void Add(ProductEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.ProductCodeUKTZED.Load();
                    if (context.Products != null)
                    {

                        item.Unit = context.ProductUnits.Find(item.Unit.ID);
                        item.CodeUKTZED = context.ProductCodeUKTZED.Find(item.CodeUKTZED.ID);

                        context.Products.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void AddRange(IEnumerable<ProductEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.ProductCodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        for (int i = 0; i < items.Count(); i++)
                        {
                            items.ElementAt(i).Unit = context.ProductUnits.Find(items.ElementAt(i).Unit.ID);
                            items.ElementAt(i).CodeUKTZED = context.ProductCodeUKTZED.Find(items.ElementAt(i).CodeUKTZED.ID);
                        }
                        context.Products.AddRange(items);
                    }
                    context.SaveChanges();
                }
            }
        }


        public void Delete(ProductEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductEntity product)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.ProductCodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        UpdateFieldProducts(context.Products.Find(product.ID), product,
                                    context.ProductUnits.Find(product.Unit.ID),
                                    context.ProductCodeUKTZED.Find(product.CodeUKTZED.ID));
                    }
                    context.SaveChanges();
                }
            }
        }
        public void UpdateRange(IEnumerable<ProductEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.ProductCodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                UpdateFieldProducts(context.Products.Find(item.ID), item,
                                    context.ProductUnits.Find(item.Unit.ID),
                                    context.ProductCodeUKTZED.Find(item.CodeUKTZED.ID));
                            }
                        }
                        else
                        {
                            throw new Exception("Товар не заповнено");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        private void UpdateFieldProducts(ProductEntity ProductsUpdate, ProductEntity Products, ProductUnitEntity unit, ProductCodeUKTZEDEntity codeUKTZED)
        {
            ProductsUpdate.Code = Products.Code;
            ProductsUpdate.NameProduct = Products.NameProduct;
            ProductsUpdate.Price = Products.Price;
            ProductsUpdate.Discount = Products.Discount;
            ProductsUpdate.Status = Products.Status;
            ProductsUpdate.Articule = Products.Articule;
            ProductsUpdate.Count = Products.Count;
            ProductsUpdate.CodeUKTZED = codeUKTZED;
            ProductsUpdate.Unit = unit;
        }

        public void UpdateParameter(ProductEntity product, string nameParameter, object valueParameter)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Products.Load();

                    if (context.Products.Count() != 0)
                    {
                        ProductEntity item;
                        if (product.Code != null && product.Code != string.Empty)
                        {
                            item = context.Products.Where(i => i.Code == product.Code).First();
                        }
                        else
                        {
                            item = context.Products.Find(product.ID);
                        }

                        switch (nameParameter)
                        {
                            case nameof(item.Count):
                                {
                                    item.Count += decimal.Parse(valueParameter.ToString());
                                    break;
                                }
                            case nameof(item.Status):
                                {
                                    var status = Enum.Parse<TypeStatusProduct>(valueParameter.ToString());
                                    item.Status = status;
                                    switch (status)
                                    {
                                        case TypeStatusProduct.OutStock:
                                            {
                                                item.OutStockAt = new DateTimeOffset();
                                                break;
                                            }
                                        case TypeStatusProduct.Archived:
                                            {
                                                item.ArhivedAt = new DateTimeOffset();
                                                break;
                                            }
                                        default:
                                            {
                                                item.OutStockAt = null;
                                                item.ArhivedAt = null;
                                                break;
                                            };

                                    }
                                    break;
                                }
                        }
                    }
                }
                context.SaveChanges();
            }
        }


        public IEnumerable<ProductEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    context.ProductUnits.Load();
                    context.Products.Load();

                    if (context.Products.Count() != 0)
                    {
                        return context.Products.ToList();
                    }
                    else
                    {
                        return new List<ProductEntity>();
                    }
                }
                return null;
            }
        } 

        public ProductEntity GetByBarCode(string barCode, TypeStatusProduct statusProduct)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    context.ProductUnits.Load();
                    context.Products.Load();

                    if (context.Products != null && context.Products.Count() != 0)
                    {
                        ProductEntity result  = new ProductEntity();
                        if (statusProduct == TypeStatusProduct.Unknown)
                        {
                            result = context.Products.First(i => i.Code == barCode);
                        }
                        else
                        {
                            result = context.Products.Where(t => t.Status == statusProduct).First(i => i.Code == barCode);
                        }
                        return result;
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new ProductEntity();
            }
        }

    

        public ProductEntity GetByBarCode(string barCode)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    context.ProductUnits.Load();
                    context.Products.Load();

                    if (context.Products != null && context.Products.Count() != 0)
                    { 
                        return context.Products.First(i => i.Code == barCode);
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new ProductEntity();
            }
        }

        public IEnumerable<ProductEntity> GetByNameAndStatu(string name, TypeStatusProduct status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
<<<<<<< HEAD
                IQueryable<ProductEntity> query = context.Products.Include(c=>c.CodeUKTZED)
                    .Include(u=>u.Unit)
                    .Include(d=>d.Discount)
                    .AsNoTracking();
=======
                IQueryable<ProductEntity> query = context.Products.AsNoTracking();
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

                if (status != TypeStatusProduct.Unknown)
                {
                    query = query.Where(o => o.Status == status);
                }

                if (!(name == string.Empty))
                {
                    query = query.Where(o => o.NameProduct.Contains(name));
                }

                var result = query.ToList();
                return result;
            }
        }
    }
}
