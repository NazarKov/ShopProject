using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IProductCodeUKTZEDServise
    {
        public bool Add(string token, CreateProductUKTZEDDto codeUKTZED);

        public bool Update(string token, UpdateProductCodeUKTZEDDto codeUKTZED);
        public bool UpdateParameter(string token, string parameter, string value, UpdateProductCodeUKTZEDDto codeUKTZEDE);

        public bool DeleteCodeUKTZEDE(string token, int id);

        public ProductCodeUKTZEDDto GetCodeUKTZEDEByCode(string token, string code, TypeStatusCodeUKTZED status);
        public PaginatorDto<ProductCodeUKTZEDDto> GetCodeUKTZEDByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusCodeUKTZED status);
        public PaginatorDto<ProductCodeUKTZEDDto> GetCodeUKTZEDPageColumn(string token, int page, int countColumn, TypeStatusCodeUKTZED status);

        public IEnumerable<ProductCodeUKTZEDDto> GetAll(string token);
    }
}
