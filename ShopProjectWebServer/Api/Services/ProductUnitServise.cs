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
        private DataBaseMainController _controller;
        private AuthorizationServise _authorizationServise;

        public ProductUnitServise(DataBaseMainController controller)
        {
            _controller = controller;
            _authorizationServise = new AuthorizationServise(controller);
        }
        public bool AddUnit(string token, CreateProductUnitDto unit)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.ProductUnitTable.Add(unit.ToProductUnitEntity());
            return true;
        }

        public bool DeleteUnit(string token, int id)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.ProductUnitTable.Delete(new ShopProjectDataBase.Entities.ProductUnitEntity() { ID = id});
            return true;
        }

        public ProductUnitDto GetUnitByCode(string token, string code, TypeStatusUnit status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.ProductUnitTable.GetUnitByCode(int.Parse(code),status).ToProductUnitDto(); 
        }

        public IEnumerable<ProductUnitDto> GetUnits(string token)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return _controller.DataBaseAccess.ProductUnitTable.GetAll().ToProductUnitDto();
        }

        public PaginatorDto<ProductUnitDto> GetUnitsByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUnit status)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var units = _controller.DataBaseAccess.ProductUnitTable.GetByNameAndStatus(name, status);
            var paginator = PaginatorDto<ProductUnitEntity>.CreationPaginator(units, page, countColumn);

            return new PaginatorDto<ProductUnitDto>(paginator.Page,paginator.Pages,paginator.Data.ToProductUnitDto());
        }

        public PaginatorDto<ProductUnitDto> GetUnitsPageColumn(string token, int page, int countColumn, TypeStatusUnit status)
            =>GetUnitsByNamePageColumn(token,string.Empty,page,countColumn,status);

        public bool UpdateParameterUnit(string token, string parameter, string value, UpdateProductUnitDto unit)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.ProductUnitTable.UpdateParameter(unit.ToProductUnitEntity(),parameter,value);
            return true;
        }

        public bool UpdateUnit(string token, UpdateProductUnitDto unit)
        {
            if (!_authorizationServise.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            _controller.DataBaseAccess.ProductUnitTable.Update(unit.ToProductUnitEntity());
            return true;
        }
    }
}
