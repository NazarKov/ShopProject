using Microsoft.EntityFrameworkCore;
using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class ProductTableAccess : IEntityAccess<ProductEntiti> , IEntityAdd<ProductEntiti> ,IEntityDelete<ProductEntiti>
        , IEntityGet<ProductEntiti> , IEntityUpdate<ProductEntiti>
    {
        public void Add(ProductEntiti item)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if (context.Products != null)
                    {

                        item.Unit = context.ProductUnits.Find(item.Unit.ID);
                        item.CodeUKTZED = context.CodeUKTZED.Find(item.CodeUKTZED.ID);

                        context.Products.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void AddRange(List<ProductEntiti> items)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        foreach (ProductEntiti item in items)
                        {
                            item.Unit = context.ProductUnits.FirstOrDefault(unit => unit.Number == item.Unit.Number);
                            item.CodeUKTZED = context.CodeUKTZED.FirstOrDefault(unit => unit.Code == item.CodeUKTZED.Code);

                            context.Products.Add(item);
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public void Delete(ProductEntiti item)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Products.Load();
                    if (context.Products != null)
                    {
                        if (item != null)
                        {
                            context.Products.Remove(context.Products.Find(item.ID));
                        }
                        else
                        {
                            throw new Exception("Товар не знайдено");
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public void DeleteRange(List<ProductEntiti> items)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context != null)
                {
                    context.Products.Load();
                    if(context.Products != null)
                    {
                        if(items!= null)
                        {
                            context.Products.RemoveRange(items);
                        }
                        else
                        {
                            throw new Exception("Товарів не знайдено");
                        }
                    }
                    context.SaveChangesAsync();
                }
            }
        }

        public IEnumerable<ProductEntiti> GetAll(string status)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if(context.Products != null)
                    {
                        if(context.Products.Count()!=0)
                        {
                            return context.Products.Where(item => item.Status == status).ToList();
                        }
                        else
                        {
                            return new List<ProductEntiti>();
                            throw new Exception("База даних не містить товарів.\nСтворіть товар");
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    
                }
                else { throw new Exception(); }
            }
        }

        public IEnumerable<ProductEntiti> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProductEntiti? GetByBarCode(string barCode)
        {
            using(ContextDataBase context =new ContextDataBase())
            {
                if(context!=null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if(context.Products!=null)
                    {
                        if (context.Products.Count() == 0)
                        {
                            return null;
                        }
                        else
                        {
                            var item = context.Products.Where(item => item.Code == barCode).FirstOrDefault();
                            return item;
                        }
                    }
                    else
                    {
                        throw new Exception("Товарів не знайдено");
                    }
                }
                else
                { throw new Exception(); }
            }
        }

        public ProductEntiti GetById(Guid id)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context!=null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if(context.Products != null)
                    {
                        if(context.Products.Count()==0)
                        {
                            throw new Exception("База даних не містить товари");
                        }
                        return context.Products.Find(id);
                    }
                    else
                    {
                        throw new Exception("Товар не знайдено");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public void Update(ProductEntiti item)
        {
            using(ContextDataBase context = new ContextDataBase())
            {
                if(context !=null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if(context.Products != null)
                    {
                        if(item!=null)
                        {
                            UpdateFieldProducts(context.Products.Find(item.ID), item,context.ProductUnits.Find(item.Unit.ID),context.CodeUKTZED.Find(item.CodeUKTZED.ID));
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
     
        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Products.Load();
                    if (context.Products.Count() != 0)
                    {
                        var item = context.Products.Find(id);

                        switch (nameParameter)
                        {
                            case nameof(item.Count):
                            {
                                item.Count += (decimal)valueParameter;
                                break;
                            }
                            case nameof(item.Status):
                            {
                                item.Status = valueParameter.ToString();
                                break;
                            }
                            case nameof(item.ArhivedAt):
                            {
                                if (valueParameter == null)
                                {
                                    item.ArhivedAt = new DateTimeOffset();
                                }
                                else
                                {

                                    item.ArhivedAt = (DateTime)valueParameter;
                                }
                                break;
                            }
                            case nameof(item.OutStockAt):
                            {
                                if (valueParameter == null)
                                {
                                    item.OutStockAt = new DateTimeOffset();
                                }
                                else
                                {
                                    item.OutStockAt = (DateTime)valueParameter;
                                }
                                break;
                            }
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public void UpdateRange(List<ProductEntiti> items)
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                UpdateFieldProducts(context.Products.Find(item.ID), item, 
                                    context.ProductUnits.Where(unit=>unit.ShortNameUnit==item.Unit.ShortNameUnit).FirstOrDefault(),
                                    context.CodeUKTZED.Where(code => code.NameCode == item.CodeUKTZED.NameCode).FirstOrDefault());
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

        private void UpdateFieldProducts(ProductEntiti ProductsUpdate, ProductEntiti Products,ProductUnitEntiti unit,CodeUKTZEDEntiti codeUKTZED)
        {
            ProductsUpdate.Code = Products.Code;
            ProductsUpdate.NameProduct = Products.NameProduct;
            ProductsUpdate.Price = Products.Price;
            ProductsUpdate.Sales = Products.Sales;
            ProductsUpdate.Status = Products.Status;
            ProductsUpdate.Articule = Products.Articule;
            ProductsUpdate.Count = Products.Count;
            ProductsUpdate.CodeUKTZED = codeUKTZED;
            ProductsUpdate.Unit = unit;
        }
    }
}
