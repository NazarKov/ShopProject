 
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;

namespace ShopProjectWebServer.Api.Interface.Services
{
    public interface IProductUnitServise
    {
        public bool AddUnit(string token, CreateProductUnitDto unit);

        public bool UpdateUnit(string token, UpdateProductUnitDto unit);
        public bool UpdateParameterUnit(string token,  string parameter, string value, UpdateProductUnitDto unit);

        public bool DeleteUnit(string token, int id);

        public ProductUnitDto GetUnitByCode(string token, string code, TypeStatusUnit status);

        public PaginatorDto<ProductUnitDto> GetUnitsByNamePageColumn(string token, string name, int page, int countColumn, TypeStatusUnit status);

        public PaginatorDto<ProductUnitDto> GetUnitsPageColumn(string token, int page, int countColumn, TypeStatusUnit status);

        public IEnumerable<ProductUnitDto> GetUnits(string token);
    }
}
