using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductCodeUKTZEDTableAccess : IProductCodeUKTZEDTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public ProductCodeUKTZEDTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public void Add(ProductCodeUKTZEDEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null && context.ProductCodeUKTZED != null)
                {
                    context.ProductCodeUKTZED.Load();

                    var unit = context.ProductCodeUKTZED.FirstOrDefault(i => i.NameCode == item.NameCode);

                    if (unit == null)
                    {
                        unit = context.ProductCodeUKTZED.FirstOrDefault(i => i.Code == item.Code);
                    } 
                    if (unit != null)
                    {
                        throw new Exception("Одиниця виміру існує");
                    }
                    context.ProductCodeUKTZED.Add(item);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<ProductCodeUKTZEDEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();

                    if (context.ProductCodeUKTZED !=null && context.ProductCodeUKTZED.Count() != 0)
                    {
                        return context.ProductCodeUKTZED.ToList();
                    } 
                }
                return new List<ProductCodeUKTZEDEntity>();
            }
        } 
        public ProductCodeUKTZEDEntity GetCodeUKTZEDByCode(int number, TypeStatusCodeUKTZED statusCodeUKTZED)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();

                    if (context.ProductCodeUKTZED != null && context.ProductCodeUKTZED.Count() != 0)
                    {
                        ProductCodeUKTZEDEntity result = new ProductCodeUKTZEDEntity();
                        if (statusCodeUKTZED == TypeStatusCodeUKTZED.Unknown)
                        {
                            result = context.ProductCodeUKTZED.First(i => i.Code == number.ToString());
                        }
                        else
                        {
                            result = context.ProductCodeUKTZED.Where(t => t.Status == statusCodeUKTZED).First(i => i.Code == number.ToString());
                        }
                        return result;
                    }
                    else
                    {
                        throw new Exception("Неможливий пошук оскільки немає товарів");
                    }

                }
                return new ProductCodeUKTZEDEntity();
            }
        } 
        public void Update(ProductCodeUKTZEDEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    if (context.ProductCodeUKTZED != null)
                    {
                        UpdateFieldCodeUKTZED(context.ProductCodeUKTZED.Find(item.ID), item);
                    }
                    context.SaveChanges();
                }
            }
        }

        private void UpdateFieldCodeUKTZED(ProductCodeUKTZEDEntity codeUKTZEDUpdate, ProductCodeUKTZEDEntity codeUKTZED)
        {
            codeUKTZEDUpdate.NameCode = codeUKTZED.NameCode;
            codeUKTZEDUpdate.Code = codeUKTZED.Code;
            codeUKTZEDUpdate.Status = codeUKTZED.Status; 
        }



        public void Delete(ProductCodeUKTZEDEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();
                    if (context.ProductCodeUKTZED != null)
                    {
                        var codeUKTZED = context.ProductCodeUKTZED.Find(item.ID);
                        context.ProductCodeUKTZED.Remove(codeUKTZED);
                    }
                    context.SaveChanges();
                }
            }
        }

        public void UpdateParameter(ProductCodeUKTZEDEntity item, string parameter, object value)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.ProductCodeUKTZED.Load();

                    if (context.ProductCodeUKTZED != null && context.ProductCodeUKTZED.Count() != 0)
                    {
                        var unit = context.ProductCodeUKTZED.FirstOrDefault(i => i.ID == item.ID);
                        if (unit != null)
                        {

                            switch (parameter)
                            {
                                case nameof(item.NameCode):
                                    {
                                        unit.NameCode = item.NameCode;
                                        break;
                                    }
                                case nameof(item.Code):
                                    {
                                        unit.Code = item.Code;
                                        break;
                                    } 
                                case nameof(item.Status):
                                    {
                                        var status = Enum.Parse<TypeStatusCodeUKTZED>(value.ToString());
                                        item.Status = status;
                                        switch (status)
                                        {
                                            case TypeStatusCodeUKTZED.Favorite:
                                                {
                                                    unit.Status = TypeStatusCodeUKTZED.Favorite;
                                                    break;
                                                }
                                            case TypeStatusCodeUKTZED.UnFavorite:
                                                {
                                                    unit.Status = TypeStatusCodeUKTZED.UnFavorite;
                                                    break;
                                                }
                                            default:
                                                {
                                                    unit.Status = TypeStatusCodeUKTZED.UnFavorite;
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

        public IEnumerable<ProductCodeUKTZEDEntity> GetByNameAndStatus(string name, TypeStatusCodeUKTZED status)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                IQueryable<ProductCodeUKTZEDEntity> query = context.ProductCodeUKTZED.AsNoTracking();

                if (status != TypeStatusCodeUKTZED.Unknown)
                {
                    query = query.Where(o => o.Status == status);
                }

                if (!(name == string.Empty))
                {
                    query = query.Where(o => o.NameCode.Contains(name));
                }

                var result = query.ToList();
                return result;
            }
        }
    }
}
