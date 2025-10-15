using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;
using System.Reflection.Metadata;

namespace ShopProjectWebServer.Api.Services
{
    public class ProductUnitServise : IProductUnitServise
    {
        public bool AddUnit(string token, CreateProductUnitDto unit)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductUnitTable.Add(unit.ToProductUnitEntity());
            return true;
        }

        public bool DeleteUnit(string token, int id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductUnitTable.Delete(new ShopProjectDataBase.Entities.ProductUnitEntity() { ID = id});
            return true;
        }

        public ProductUnitDto GetUnitByCode(string token, string code, TypeStatusUnit status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.ProductUnitTable.GetUnitByCode(int.Parse(code),status).ToProductUnitDto(); 
        }

        public IEnumerable<ProductUnitDto> GetUnits(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.ProductUnitTable.GetAll().ToProductUnitDto();
        }

        public PaginatorDto<ProductUnitDto> GetUnitsByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUnit status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var units = DataBaseMainController.DataBaseAccess.ProductUnitTable.GetByNameAndStatus(name, status);
            var paginator = PaginatorDto<ProductUnitEntity>.CreationPaginator(units, page, countColumn);

            return new PaginatorDto<ProductUnitDto>(paginator.Page,paginator.Pages,paginator.Data.ToProductUnitDto());
        }

        public PaginatorDto<ProductUnitDto> GetUnitsPageColumn(string token, int page, int countColumn, TypeStatusUnit status)
            =>GetUnitsByNamePageColumn(token,string.Empty,page,countColumn,status);

        public bool UpdateParameterUnit(string token, string parameter, string value, UpdateProductUnitDto unit)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductUnitTable.UpdateParameter(unit.ToProductUnitEntity(),parameter,value);
            return true;
        }

        public bool UpdateUnit(string token, UpdateProductUnitDto unit)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductUnitTable.Update(unit.ToProductUnitEntity());
            return true;
        }
    }
}
