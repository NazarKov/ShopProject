﻿using ShopProjectSQLDataBase.Context;
using ShopProjectSQLDataBase.Entities;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.Api.Helpers.ProductContoller;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;
using System.Linq;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductTableAccess : IProductTableAccess 
    {
        private string _connectionString;

        public ProductTableAccess(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        public void Add(ProductEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.ProductCodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        UpdateFieldProducts(context.Products.Find(product.ID), product,
                                   context.ProductUnits.Where(unit => unit.ShortNameUnit == product.Unit.ShortNameUnit).FirstOrDefault(),
                                   context.ProductCodeUKTZED.Where(code => code.NameCode == product.CodeUKTZED.NameCode).FirstOrDefault());
                    }
                    context.SaveChanges();
                }
            }
        }
        public void UpdateRange(IEnumerable<ProductEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
                                    context.ProductUnits.Where(unit => unit.ShortNameUnit == item.Unit.ShortNameUnit).FirstOrDefault(),
                                    context.ProductCodeUKTZED.Where(code => code.NameCode == item.CodeUKTZED.NameCode).FirstOrDefault());
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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

        public PaginatorData<ProductEntity> GetAllPageColumn(double page , double countColumn,TypeStatusProduct productstatus)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    context.ProductUnits.Load();
                    context.Products.Load();

                    if (context.Products.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<ProductEntity> result = new PaginatorData<ProductEntity>();

                        if (productstatus == TypeStatusProduct.Unknown)
                        {
                            result.Page = (int)page; 
                            result.Data = context.Products.OrderBy(i => i.ID)
                                                          .Skip(countStart)
                                                          .Take((int)countColumn).ToList();
                            
                            pages = context.Products.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var products = context.Products.Where(item => item.Status == productstatus).ToList();
                            result.Data = products.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = products.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;
                    }
                    else
                    {
                        return new PaginatorData<ProductEntity>();
                    }
                }
                return new PaginatorData<ProductEntity>();
            }
        }

        public ProductEntity GetByBarCode(string barCode, TypeStatusProduct statusProduct)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
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

        public PaginatorData<ProductEntity> GetProductByNamePageColumn(string name , double page, double countColumn, TypeStatusProduct statusProduct)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    context.ProductUnits.Load();
                    context.Products.Load();

                    if (context.Products != null && context.Products.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<ProductEntity> result = new PaginatorData<ProductEntity>(); 

                        if (statusProduct == TypeStatusProduct.Unknown)
                        {
                            result.Page = (int)page;
                            var products = context.Products.Where(i => i.NameProduct.Contains(name)).ToList();
                            result.Data = products.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = products.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var products = context.Products.Where(t => t.Status == statusProduct)
                                                           .Where(i => i.NameProduct.Contains(name)).ToList();
                            result.Data = products.OrderBy(i => i.ID)
                                                  .Skip(countStart)
                                                  .Take((int)countColumn).ToList();

                            pages = products.Count() / countColumn;
                        }

                        int pagesCount = 0;

                        if (!(pages % 2 == 0))
                        {
                            pagesCount = (int)pages;
                            pagesCount++;
                        }
                        result.Pages = pagesCount;

                        return result;
 
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new PaginatorData<ProductEntity>();
            }
        }

        public ProductInfo GetProductInfo()
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                { 
                    context.Products.Load();

                    if (context.Products != null && context.Products.Count() != 0)
                    {
                        return new ProductInfo()
                        {
                            CountProductAllStatus = context.Products.Count(),
                            CountProductInStockStatus = context.Products.Where(i => i.Status == TypeStatusProduct.InStock).Count(),
                            CountProductOutStockStatus = context.Products.Where(i => i.Status == TypeStatusProduct.OutStock).Count(),
                            CountProductArchivedStauts = context.Products.Where(i => i.Status == TypeStatusProduct.Archived).Count(),
                        };
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new ProductInfo();
            }
        }
    }
}
