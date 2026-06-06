using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using System.Threading.Tasks;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class ProductCodeUKTZEDTableAccess : IProductCodeUKTZEDTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public ProductCodeUKTZEDTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public async Task<ProductCodeUKTZEDEntity> AddAsync(ProductCodeUKTZEDEntity item)
        { 
            var unit = _contextDataBase.ProductCodeUKTZED.FirstOrDefault(i => i.NameCode == item.NameCode); 
            if (unit == null)
            {
                unit = _contextDataBase.ProductCodeUKTZED.FirstOrDefault(i => i.Code == item.Code);
            }
            if (unit != null)
            {
                throw new Exception("Одиниця виміру існує");
            }
            await _contextDataBase.ProductCodeUKTZED.AddAsync(item);
            await _contextDataBase.SaveChangesAsync(); 
            return item;
        }

        public async Task UpdateAsync(ProductCodeUKTZEDEntity item)
        {
            UpdateFieldCodeUKTZED(_contextDataBase.ProductCodeUKTZED.Find(item.ID), item);
            await _contextDataBase.SaveChangesAsync();
        }

        private void UpdateFieldCodeUKTZED(ProductCodeUKTZEDEntity codeUKTZEDUpdate, ProductCodeUKTZEDEntity codeUKTZED)
        {
            codeUKTZEDUpdate.NameCode = codeUKTZED.NameCode;
            codeUKTZEDUpdate.Code = codeUKTZED.Code;
            codeUKTZEDUpdate.Status = codeUKTZED.Status;
        }

        public async Task UpdateParameterAsync(ProductCodeUKTZEDEntity item, string parameter, object value)
        {
            var unit = _contextDataBase.ProductCodeUKTZED.FirstOrDefault(i => i.ID == item.ID);
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
            await _contextDataBase.SaveChangesAsync();
        }
        public void Delete(ProductCodeUKTZEDEntity item)
        {
            var codeUKTZED = _contextDataBase.ProductCodeUKTZED.Find(item.ID);
            if (codeUKTZED == null) return;

            _contextDataBase.ProductCodeUKTZED.Remove(codeUKTZED);
            _contextDataBase.SaveChanges();
        }



        public IEnumerable<ProductCodeUKTZEDEntity> GetAll()
        {
            return _contextDataBase.ProductCodeUKTZED.AsNoTracking().ToList();
        } 
        public IEnumerable<ProductCodeUKTZEDEntity> GetByCode(int number, TypeStatusCodeUKTZED statusCodeUKTZED)
        { 
            if (statusCodeUKTZED == TypeStatusCodeUKTZED.Unknown)
            {
                return _contextDataBase.ProductCodeUKTZED.AsNoTracking().Where(i => i.Code.Contains(number.ToString()));
            }
            else
            {
                return _contextDataBase.ProductCodeUKTZED.AsNoTracking().Where(t => t.Status == statusCodeUKTZED).Where(i => i.Code.Contains(number.ToString()));
            } 
        }  
        public IEnumerable<ProductCodeUKTZEDEntity> GetByNameAndStatus(string name, TypeStatusCodeUKTZED status)
        {
            IQueryable<ProductCodeUKTZEDEntity> query = _contextDataBase.ProductCodeUKTZED.AsNoTracking();

            if (status != TypeStatusCodeUKTZED.Unknown)
            {
                query = query.Where(o => o.Status == status);
            }

            if (!(name == string.Empty))
            {
                query = query.Where(o => o.NameCode.Contains(name));
            } 
            return query.ToList();
        }

        public async Task<bool> ExistsByCodeAsync(string code)
        {
            return await _contextDataBase.ProductCodeUKTZED.AnyAsync(p => p.Code == code);
        }
    }
}
