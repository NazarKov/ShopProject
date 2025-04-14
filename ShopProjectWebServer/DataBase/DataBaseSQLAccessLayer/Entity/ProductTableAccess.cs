using ShopProjectDataBase.DataBase.Context;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.DataBase.HelperModel;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Data.Entity;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductTableAccess : IProductTableAccess<ProductEntity>
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

        public void AddRange(IEnumerable<ProductEntity> items)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.Products.Load();
                    context.ProductUnits.Load();
                    context.CodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        for (int i = 0; i < items.Count(); i++)
                        {
                            items.ElementAt(i).Unit = context.ProductUnits.Find(items.ElementAt(i).Unit.ID);
                            items.ElementAt(i).CodeUKTZED = context.CodeUKTZED.Find(items.ElementAt(i).CodeUKTZED.ID);
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
                    context.CodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        UpdateFieldProducts(context.Products.Find(product.ID), product,
                                   context.ProductUnits.Where(unit => unit.ShortNameUnit == product.Unit.ShortNameUnit).FirstOrDefault(),
                                   context.CodeUKTZED.Where(code => code.NameCode == product.CodeUKTZED.NameCode).FirstOrDefault());
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
                    context.CodeUKTZED.Load();
                    if (context.Products != null)
                    {
                        if (items != null)
                        {
                            foreach (var item in items)
                            {
                                UpdateFieldProducts(context.Products.Find(item.ID), item,
                                    context.ProductUnits.Where(unit => unit.ShortNameUnit == item.Unit.ShortNameUnit).FirstOrDefault(),
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

        private void UpdateFieldProducts(ProductEntity ProductsUpdate, ProductEntity Products, ProductUnitEntity unit, CodeUKTZEDEntity codeUKTZED)
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
                    context.CodeUKTZED.Load();
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

        public ProductEntity GetByBarCode(string barCode)
        {
            using (ContextDataBase context = new ContextDataBase(_connectionString))
            {
                if (context != null)
                {
                    context.CodeUKTZED.Load();
                    context.ProductUnits.Load();
                    context.Products.Load();

                    if (context.Products.Count() != 0)
                    {
                        var item = context.Products.Where(i => i.Code == barCode).FirstOrDefault();
                        if (item != null)
                        {
                            return item;
                        }
                        else
                        {
                            throw new Exception("Товар не знайдено");
                        }
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немє товарів");
                    }

                }
                return null;
            }
        }
    }
}
