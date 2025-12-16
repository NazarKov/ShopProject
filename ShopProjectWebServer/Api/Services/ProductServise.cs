using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.Interface.Services;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.DataBase; 

namespace ShopProjectWebServer.Api.Services
{
    public class ProductServise : IProductServise
    {
        public bool Add(string token, CreateProductDto product)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductTable.Add(product.ToProductEntity());
            return true;
        }

        public async Task<bool> AddProductRangeAsync(string token, IEnumerable<CreateProductDto> product)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            await DataBaseMainController.DataBaseAccess.ProductTable.AddRangeAsync(product.ToProductEnity());
            return true;
        }

        public ProductInfoDto GetInfoProducts(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return ProductInfoDto.Create(DataBaseMainController.DataBaseAccess.ProductTable.GetAll());
        }

        public PaginatorDto<ProductDto> GetProductByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusProduct status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            } 
            var products = DataBaseMainController.DataBaseAccess.ProductTable.GetByNameAndStatus(name, status);

            var paginator = PaginatorDto<ProductEntity>.CreationPaginator(products.Reverse(), page, countColumn);
            return new PaginatorDto<ProductDto>(paginator.Page,paginator.Pages,paginator.Data.ToProductDto());
        }

        public IEnumerable<ProductDto> GetProducts(string token)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.ProductTable.GetAll().ToProductDto();
        }

        public ProductDto GetProductsByBarCode(string token, string barCode , TypeStatusProduct status)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            return DataBaseMainController.DataBaseAccess.ProductTable.GetByBarCode(barCode , status).ToProductDto();
        }

        public PaginatorDto<ProductDto> GetProductsPageColumn(string token, int page, int countColumn, TypeStatusProduct status)
            => GetProductByNamePageColumn(token , string.Empty , page , countColumn ,status);

        public bool UpdateParameterProduct(string token, string parameter, string value, UpdateProductDto product)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductTable.UpdateParameter(product.ToProductEntity(),parameter,value);
            return true;
        }

        public bool UpdateProduct(string token, UpdateProductDto product)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductTable.Update(product.ToProductEntity());
            return true;
        }

        public bool UpdateProductRange(string token, IEnumerable<UpdateProductDto> product)
        {
            if (!AuthorizationApi.LoginToken(token))
            {
                throw new Exception("Невірний токен авторизації");
            }
            DataBaseMainController.DataBaseAccess.ProductTable.UpdateRange(product.ToProductEntity());
            return true;
        }
    }
}
