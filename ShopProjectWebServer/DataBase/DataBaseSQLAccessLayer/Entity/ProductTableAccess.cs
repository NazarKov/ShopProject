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
            _contextDataBase.SaveChanges();
        }

        public async Task AddRangeAsync(IEnumerable<ProductEntity> items)
        { 
            foreach (var item in items)
            {
                var entity = _contextDataBase.Products.FirstOrDefault(c => c.Code == item.Code);

                if (entity != null)
                {
                    entity.Count += item.Count;
                }
                else
                {
                    if (item.Unit != null)
                    {
                        item.Unit = _contextDataBase.ProductUnits.FirstOrDefault(u => u.ID ==   item.Unit.ID);
                    }
                    else
                    {
                        item.Unit = null;
                    }
                    if (item.CodeUKTZED != null)
                    {
                        item.CodeUKTZED = _contextDataBase.ProductCodeUKTZED.FirstOrDefault(c => c.ID == item.CodeUKTZED.ID);
                    }
                    else
                    {
                        item.CodeUKTZED = null;
                    }
                    if (item.Discount != null)
                    {
                        item.Discount = _contextDataBase.Discounts.FirstOrDefault(c => c.ID == item.Discount.ID);
                    }
                    else
                    {
                        item.Discount = null;
                    } 
                }
                await _contextDataBase.Products.AddRangeAsync(items);
                await _contextDataBase.SaveChangesAsync();
            }
        }


        public void Delete(ProductEntity item)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductEntity product)
        {
            UpdateFieldProducts(_contextDataBase.Products.Find(product.ID), product,
                            _contextDataBase.ProductUnits.Find(product.Unit.ID),
                            _contextDataBase.ProductCodeUKTZED.Find(product.CodeUKTZED.ID));
            _contextDataBase.SaveChanges();
        }
        public void UpdateRange(IEnumerable<ProductEntity> items)
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
            ProductEntity item;
            if (product.Code != null && product.Code != string.Empty)
            {
                item = _contextDataBase.Products.FirstOrDefault(i => i.Code == product.Code);
            }
            else
            {
                item = _contextDataBase.Products.FirstOrDefault(p=>p.ID == product.ID);
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
            _contextDataBase.SaveChanges();
        } 


        public IEnumerable<ProductEntity> GetAll()
        {
            return _contextDataBase.Products.Include(u => u.Unit).Include(c => c.CodeUKTZED).AsNoTracking();
        } 

        public ProductEntity GetByBarCode(string barCode, TypeStatusProduct statusProduct)
        { 
            if (statusProduct == TypeStatusProduct.Unknown)
            {
                return _contextDataBase.Products.FirstOrDefault(i => i.Code == barCode);
            }
            else
            {
                return _contextDataBase.Products.Where(t => t.Status == statusProduct).FirstOrDefault(i => i.Code == barCode);
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
