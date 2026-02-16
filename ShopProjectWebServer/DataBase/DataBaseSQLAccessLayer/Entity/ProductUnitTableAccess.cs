using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper; 
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductUnitTableAccess : IProductUnitTableAccess 
    {

        private readonly ContextDataBase _contextDataBase;
        public ProductUnitTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public void Add(ProductUnitEntity item)
        { 
            var unit = _contextDataBase.ProductUnits.FirstOrDefault(i => i.NameUnit == item.NameUnit);

            if (unit == null)
            {
                unit = _contextDataBase.ProductUnits.FirstOrDefault(i => i.ShortNameUnit == item.ShortNameUnit);
            }

            if (unit == null)
            {
                unit = _contextDataBase.ProductUnits.FirstOrDefault(i => i.NameUnit == item.NameUnit);
            }

            if (unit != null)
            {
                throw new Exception("Одиниця виміру існує");
            }
            _contextDataBase.ProductUnits.Add(item);
            _contextDataBase.SaveChanges();
        }

        public IEnumerable<ProductUnitEntity> GetAll()
        {
            return _contextDataBase.ProductUnits.ToList();
        } 

        public ProductUnitEntity GetUnitByCode(int number, TypeStatusUnit statusUnit)
        { 
            if (statusUnit == TypeStatusUnit.Unknown)
            {
                return _contextDataBase.ProductUnits.FirstOrDefault(i => i.Number == number);
            }
            else
            {
                return _contextDataBase.ProductUnits.Where(t => t.Status == statusUnit).FirstOrDefault(i => i.Number == number);
            } 
        }  
        public void Update(ProductUnitEntity item)
        {
            UpdateFieldUnit(_contextDataBase.ProductUnits.Find(item.ID), item);
            _contextDataBase.SaveChanges();
        }

        private void UpdateFieldUnit(ProductUnitEntity UnitUpdate, ProductUnitEntity unit)
        {
            UnitUpdate.NameUnit = unit.NameUnit;
            UnitUpdate.ShortNameUnit = unit.ShortNameUnit;
            UnitUpdate.Number = unit.Number;
            UnitUpdate.Status = unit.Status;
        } 
        public void Delete(ProductUnitEntity item)
        {
            var unit = _contextDataBase.ProductUnits.Find(item.ID);

            if (unit == null) return; 
            _contextDataBase.ProductUnits.Remove(unit);
            _contextDataBase.SaveChanges();
        }

        public void UpdateParameter(ProductUnitEntity item, string parameter, object value)
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
                _contextDataBase.SaveChanges();
            } 
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
    }
}
