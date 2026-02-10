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

        private readonly ContextDataBase _contextDataBase;

        public ProductTableAccess(ContextDataBase  contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public void Add(ProductEntity item)
        {
            _contextDataBase.Products.Load();
            _contextDataBase.ProductUnits.Load();
            _contextDataBase.ProductCodeUKTZED.Load();
            _contextDataBase.Discounts.Load();
            if (_contextDataBase.Products != null)
            {
                if (item.Unit != null)
                {
                    item.Unit = _contextDataBase.ProductUnits.Find(item.Unit.ID);
                }
                if (item.CodeUKTZED != null)
                {
                    item.CodeUKTZED = _contextDataBase.ProductCodeUKTZED.Find(item.CodeUKTZED.ID);
                }
                if (item.Discount != null)
                {
                    item.Discount = _contextDataBase.Discounts.Find(item.Discount.ID);
                }

                _contextDataBase.Products.Add(item);
            }
            _contextDataBase.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<ProductEntity> items)
        {
            _contextDataBase.Products.Load();
            _contextDataBase.ProductUnits.Load();
            _contextDataBase.ProductCodeUKTZED.Load();
            _contextDataBase.Discounts.Load();
            if (_contextDataBase.Products != null)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    var item = _contextDataBase.Products.FirstOrDefault(c => c.Code == items.ElementAt(i).Code);

                    if (item != null)
                    {
                        item.Count += items.ElementAt(i).Count;
                    }
                    else
                    {
                        if (items.ElementAt(i).Unit != null)
                        {
                            items.ElementAt(i).Unit = _contextDataBase.ProductUnits.FirstOrDefault(u => u.ID == items.ElementAt(i).Unit.ID);
                        }
                        else
                        {
                            items.ElementAt(i).Unit = null;
                        }
                        if (items.ElementAt(i).CodeUKTZED != null)
                        {
                            items.ElementAt(i).CodeUKTZED = _contextDataBase.ProductCodeUKTZED.FirstOrDefault(c => c.ID == items.ElementAt(i).CodeUKTZED.ID);
                        }
                        else
                        {
                            items.ElementAt(i).CodeUKTZED = null;
                        }
                        if (items.ElementAt(i).Discount != null)
                        {
                            items.ElementAt(i).Discount = _contextDataBase.Discounts.FirstOrDefault(c => c.ID == items.ElementAt(i).Discount.ID);
                        }
                        else
                        {
                            items.ElementAt(i).Discount = null;
                        }

                        await _contextDataBase.Products.AddAsync(items.ElementAt(i));
                    }
                }
            }
            await _contextDataBase.SaveChangesAsync();
        }


        public void Delete(ProductEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductEntity product)
        {
            _contextDataBase.Products.Load();
            _contextDataBase.ProductUnits.Load();
            _contextDataBase.ProductCodeUKTZED.Load();
            if (_contextDataBase.Products != null)
            {
                UpdateFieldProducts(_contextDataBase.Products.Find(product.ID), product,
                            _contextDataBase.ProductUnits.Find(product.Unit.ID),
                            _contextDataBase.ProductCodeUKTZED.Find(product.CodeUKTZED.ID));
            }
            _contextDataBase.SaveChanges();
        }
        public void UpdateRange(IEnumerable<ProductEntity> items)
        {
            _contextDataBase.Products.Load();
            _contextDataBase.ProductUnits.Load();
            _contextDataBase.ProductCodeUKTZED.Load();
            if (_contextDataBase.Products != null)
            {
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        UpdateFieldProducts(_contextDataBase.Products.Find(item.ID), item,
                            _contextDataBase.ProductUnits.Find(item.Unit.ID),
                            _contextDataBase.ProductCodeUKTZED.Find(item.CodeUKTZED.ID));
                    }
                }
                else
                {
                    throw new Exception("Товар не заповнено");
                }
            }
            _contextDataBase.SaveChanges();
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
            _contextDataBase.Products.Load();

            if (_contextDataBase.Products.Count() != 0)
            {
                ProductEntity item;
                if (product.Code != null && product.Code != string.Empty)
                {
                    item = _contextDataBase.Products.Where(i => i.Code == product.Code).First();
                }
                else
                {
                    item = _contextDataBase.Products.Find(product.ID);
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
                                    }
                                    ;

                            }
                            break;
                        }
                }
            }

            _contextDataBase.SaveChanges();
        } 


        public IEnumerable<ProductEntity> GetAll()
        {
            _contextDataBase.ProductCodeUKTZED.Load();
            _contextDataBase.ProductUnits.Load();
            _contextDataBase.Products.Load();

            if (_contextDataBase.Products.Count() != 0)
            {
                return _contextDataBase.Products.ToList();
            }
            else
            {
                return new List<ProductEntity>();
            }
        } 

        public ProductEntity GetByBarCode(string barCode, TypeStatusProduct statusProduct)
        {
            _contextDataBase.ProductCodeUKTZED.Load();
            _contextDataBase.ProductUnits.Load();
            _contextDataBase.Products.Load();

            if (_contextDataBase.Products != null && _contextDataBase.Products.Count() != 0)
            {
                ProductEntity result = new ProductEntity();
                if (statusProduct == TypeStatusProduct.Unknown)
                {
                    result = _contextDataBase.Products.First(i => i.Code == barCode);
                }
                else
                {
                    result = _contextDataBase.Products.Where(t => t.Status == statusProduct).First(i => i.Code == barCode);
                }
                return result;
            }
            else
            {
                throw new Exception("Неможливий пошук оскільки немає товарів");
            }
        } 

        public IEnumerable<ProductEntity> GetByNameAndStatus(string name, TypeStatusProduct status)
        {
            IQueryable<ProductEntity> query = _contextDataBase.Products.Include(c => c.CodeUKTZED)
                    .Include(u => u.Unit)
                    .Include(d => d.Discount)
                    .AsNoTracking();

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
