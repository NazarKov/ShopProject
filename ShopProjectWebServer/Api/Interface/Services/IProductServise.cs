using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IProductServise
    {
        public ProductInfoDto  GetInfoProducts(string token);

        public ProductDto GetProductsByBarCode(string token, string barCode , TypeStatusProduct status);

        public PaginatorDto<ProductDto> GetProductByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusProduct status);
        public PaginatorDto<ProductDto> GetProductsPageColumn(string token, int page, int countColumn, TypeStatusProduct status);

        public IEnumerable<ProductDto> GetProducts(string token);

        public bool Add(string token, CreateProductDto product);

        public Task<bool> AddProductRangeAsync(string token, IEnumerable<CreateProductDto> product);

        public bool UpdateProduct(string token, UpdateProductDto product);

        public bool UpdateProductRange(string token, IEnumerable<UpdateProductDto> product);

        public bool UpdateParameterProduct(string token,string parameter ,string value, UpdateProductDto product);
    }
}
