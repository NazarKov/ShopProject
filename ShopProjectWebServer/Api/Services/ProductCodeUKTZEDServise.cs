using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase;

namespace ShopProjectWebServer.Api.Services
{
    public class ProductCodeUKTZEDServise : IProductCodeUKTZEDServise
    {
        public bool Add(string token, CreateProductUKTZEDDto codeUKTZED)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.Add(codeUKTZED.ToProductCodeUKTZEDEntity());
            return true;
        }

        public bool DeleteCodeUKTZEDE(string token, int id)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.Delete(new ShopProjectDataBase.Entities.ProductCodeUKTZEDEntity() { ID = id});
            return true;
        }

        public IEnumerable<ProductCodeUKTZEDDto> GetAll(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            return DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetAll().ToProductCodeUKTZEDDto();
        }

        public PaginatorDto<ProductCodeUKTZEDDto> GetCodeUKTZEDByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusCodeUKTZED status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            var productCodeUKTZEDs = DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetByNameAndStatus(name, status);

            var paginator = PaginatorDto<ProductCodeUKTZEDEntity>.CreationPaginator(productCodeUKTZEDs, page, countColumn);
            return new PaginatorDto<ProductCodeUKTZEDDto>(paginator.Page, paginator.Pages, productCodeUKTZEDs.ToProductCodeUKTZEDDto());
        }

        public ProductCodeUKTZEDDto GetCodeUKTZEDEByCode(string token, string code, TypeStatusCodeUKTZED status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.GetCodeUKTZEDByCode(int.Parse(code),status).ToProductCodeUKTZEDDto();
        }

        public PaginatorDto<ProductCodeUKTZEDDto> GetCodeUKTZEDPageColumn(string token, int page, int countColumn, TypeStatusCodeUKTZED status)
            => GetCodeUKTZEDByNamePageColumn(token, string.Empty, page, countColumn, status);

        public bool Update(string token, UpdateProductCodeUKTZEDDto codeUKTZED)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.Update(codeUKTZED.ToProductCodeUKTZEDEntity());
            return true;
        }

        public bool UpdateParameter(string token, string parameter, string value, UpdateProductCodeUKTZEDDto codeUKTZEDE)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductCodeUKTZEDTable.UpdateParameter(codeUKTZEDE.ToProductCodeUKTZEDEntity(), parameter , value);
            return true;
        }
    }
}
