using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Threading.Tasks;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductUnitTableAccess : IProductUnitTableAccess 
    {

        private readonly ContextDataBase _contextDataBase;
        public ProductUnitTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public async Task<ProductUnitEntity> AddAsync(ProductUnitEntity item)
        {  
            await _contextDataBase.ProductUnits.AddAsync(item);
            await _contextDataBase.SaveChangesAsync();
            return item;
        }
        public async Task UpdateAsync(ProductUnitEntity item)
        {
            UpdateFieldUnit(_contextDataBase.ProductUnits.Find(item.ID), item);
            await _contextDataBase.SaveChangesAsync();
        }

        private void UpdateFieldUnit(ProductUnitEntity UnitUpdate, ProductUnitEntity unit)
        {
            UnitUpdate.NameUnit = unit.NameUnit;
            UnitUpdate.ShortNameUnit = unit.ShortNameUnit;
            UnitUpdate.Number = unit.Number;
            UnitUpdate.Status = unit.Status;
        }

        public async Task UpdateParameterAsync(ProductUnitEntity item, string parameter, object value)
        {
            var unit = _contextDataBase.ProductUnits.FirstOrDefault(i => i.ID == item.ID);
            if (unit != null)
            {

                switch (parameter)
                {
                    case nameof(item.NameUnit):
                        {
                            unit.NameUnit = item.NameUnit;
                            break;
                        }
                    case nameof(item.ShortNameUnit):
                        {
                            unit.ShortNameUnit = item.ShortNameUnit;
                            break;
                        }
                    case nameof(item.Number):
                        {
                            unit.Number = item.Number;
                            break;
                        }
                    case nameof(item.Status):
                        {
                            var status = Enum.Parse<TypeStatusUnit>(value.ToString());
                            item.Status = status;
                            switch (status)
                            {
                                case TypeStatusUnit.Favorite:
                                    {
                                        unit.Status = TypeStatusUnit.Favorite;
                                        break;
                                    }
                                case TypeStatusUnit.UnFavorite:
                                    {
                                        unit.Status = TypeStatusUnit.UnFavorite;
                                        break;
                                    }
                                default:
                                    {
                                        unit.Status = TypeStatusUnit.UnFavorite;
                                        break;
                                    }
                            }
                            break;
                        }
                }
                await _contextDataBase.SaveChangesAsync();
            }
        }

        public IEnumerable<ProductUnitEntity> GetAll()
        {
            return _contextDataBase.ProductUnits.ToList();
        } 

        public IEnumerable<ProductUnitEntity> GetByCode(int number, TypeStatusUnit status)
        { 
            if (status == TypeStatusUnit.Unknown)
            {
                return _contextDataBase.ProductUnits.Where(i => i.Number.ToString().Contains(number.ToString()));
            }
            else
            {
                return _contextDataBase.ProductUnits.Where(t => t.Status == status).Where(i => i.Number.ToString().Contains(number.ToString()));
            } 
        }  
       
        public async Task DeleteAsync(int id)
        {
            var unit = _contextDataBase.ProductUnits.Find(id);

            if (unit == null) return; 
            _contextDataBase.ProductUnits.Remove(unit);
            await _contextDataBase.SaveChangesAsync();
        }

      

        public IEnumerable<ProductUnitEntity> GetByNameAndStatus(string name, TypeStatusUnit status)
        {
            IQueryable<ProductUnitEntity> query = _contextDataBase.ProductUnits.AsNoTracking();

            if (status != TypeStatusUnit.Unknown)
            {
                query = query.Where(o => o.Status == status);
            }

            if (!(name == string.Empty))
            {
                query = query.Where(o => o.NameUnit.Contains(name));
            }

            var result = query.ToList();
            return result;
        }

        public async Task<bool> ExistsByBarCode(int code)
        {
            return await _contextDataBase.ProductUnits.AnyAsync(p => p.Number == code);  
        }
    }
}
