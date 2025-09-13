using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductUnitTableAccess : IProductUnitTableAccess 
    {
        private DbContextOptions<ContextDataBase> _option;
        public ProductUnitTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public void Add(ProductUnitEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null && context.ProductUnits != null)
                {
                    context.ProductUnits.Load();

                    var unit = context.ProductUnits.FirstOrDefault(i => i.NameUnit == item.NameUnit);

                    if (unit == null)
                    {
                        unit = context.ProductUnits.FirstOrDefault(i => i.ShortNameUnit == item.ShortNameUnit);
                    }

                    if (unit == null)
                    {
                        unit = context.ProductUnits.FirstOrDefault(i => i.NameUnit == item.NameUnit);
                    }

                    if (unit != null)
                    {
                        throw new Exception("Одиниця виміру існує");
                    }
                    context.ProductUnits.Add(item);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<ProductUnitEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();

                    if (context.ProductUnits.Count() != 0)
                    {
                        return context.ProductUnits.ToList();
                    }
                    else
                    {
                        return new List<ProductUnitEntity>();
                    }
                }
                return null;
            }
        }

        public PaginatorData<ProductUnitEntity> GetAllPageColumn(double page, double countColumn, TypeStatusUnit statusUnit)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();

                    if (context.ProductUnits != null && context.ProductUnits.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<ProductUnitEntity> result = new PaginatorData<ProductUnitEntity>();

                        if (statusUnit == TypeStatusUnit.Unknown)
                        {
                            result.Page = (int)page;
                            result.Data = context.ProductUnits.OrderBy(i => i.ID)
                                                              .Skip(countStart)
                                                              .Take((int)countColumn).ToList();
                            pages = context.ProductUnits.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var units = context.ProductUnits.Where(item => item.Status == statusUnit).ToList();
                            result.Data = units.OrderBy(i => i.ID)
                                               .Skip(countStart)
                                               .Take((int)countColumn).ToList();

                            pages = units.Count() / countColumn;
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
                }
            }
            return new PaginatorData<ProductUnitEntity>();
        }

        public ProductUnitEntity GetUnitByCode(int number, TypeStatusUnit statusUnit)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();

                    if (context.ProductUnits != null && context.ProductUnits.Count() != 0)
                    {
                        ProductUnitEntity result = new ProductUnitEntity();
                        if (statusUnit == TypeStatusUnit.Unknown)
                        {
                            result = context.ProductUnits.First(i => i.Number == number);
                        }
                        else
                        {
                            result = context.ProductUnits.Where(t => t.Status == statusUnit).First(i => i.Number == number);
                        }
                        return result;
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new ProductUnitEntity();
            }
        }

        public PaginatorData<ProductUnitEntity> GetUnitByNamePageColumn(string name, double page, double countColumn, TypeStatusUnit statusUnit)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();

                    if (context.ProductUnits != null && context.ProductUnits.Count() != 0)
                    {
                        double pages;

                        int countEnd = (int)(page * countColumn);
                        int countStart = (int)(countEnd - countColumn);

                        PaginatorData<ProductUnitEntity> result = new PaginatorData<ProductUnitEntity>();

                        if (statusUnit == TypeStatusUnit.Unknown)
                        {
                            result.Page = (int)page;

                            var units = context.ProductUnits.Where(i => i.NameUnit.Contains(name)).ToList();

                            result.Data = units.OrderBy(i => i.ID)
                                               .Skip(countStart)
                                               .Take((int)countColumn).ToList();
                            pages = units.Count() / countColumn;
                        }
                        else
                        {
                            result.Page = (int)page;
                            var units = context.ProductUnits.Where(t => t.Status == statusUnit)
                                                            .Where(i => i.NameUnit.Contains(name)).ToList();

                            result.Data = units.OrderBy(i => i.ID)
                                               .Skip(countStart)
                                               .Take((int)countColumn).ToList();

                            pages = units.Count() / countColumn;
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
                return new PaginatorData<ProductUnitEntity>();
            }
        }

        public void Update(ProductUnitEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();
                    if (context.ProductUnits != null)
                    {
                        UpdateFieldUnit(context.ProductUnits.Find(item.ID), item);
                    }
                    context.SaveChanges();
                }
            }
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
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();
                    if (context.ProductUnits != null)
                    {
                        var unit = context.ProductUnits.Find(item.ID);
                        context.ProductUnits.Remove(unit);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void UpdateParameter(ProductUnitEntity item, string parameter, object value)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductUnits.Load();

                    if (context.ProductUnits != null && context.ProductUnits.Count() != 0)
                    {
                        var unit = context.ProductUnits.FirstOrDefault(i => i.ID == item.ID);
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
                        }
                    } 
                    context.SaveChanges();
                } 
            }
        }
    }
}
