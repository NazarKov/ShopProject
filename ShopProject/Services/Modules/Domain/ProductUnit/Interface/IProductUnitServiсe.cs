using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Enum;
using ShopProject.Services.Modules.Common; 
using System.Collections.Generic; 
using System.Threading.Tasks;
using ProductUnitModel = ShopProject.Model.Domain.ProductUnit.ProductUnit;

namespace ShopProject.Services.Modules.Domain.ProductUnit.Interface
{
    internal interface IProductUnitServiсe
    {
        public Task<OperationResult<ProductUnitModel>> Add(ProductUnitModel item);
        public Task<OperationResult<ProductUnitModel>> Update(ProductUnitModel item);
        public Task<OperationResult<bool>> UpdateParameter(string parameter, object value, ProductUnitModel item);
        public Task<OperationResult<bool>> Delete(ProductUnitModel item);

        public Task<OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>> GetPageColumn(int page, int countColumn, TypeStatusUnit status);
        public Task<OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>> SearchByName(string item, int page, int countColumn, TypeStatusUnit status);

        public Task<OperationResult<Paginator<ProductUnitModel, TypeStatusUnit>>> SearchByBarCode(string item, int page, int countColumn, TypeStatusUnit status);

        public Task<IEnumerable<ShopProject.Model.Domain.ProductUnit.ProductUnit>> GetFromSession();
        public void SetUnitOnSession(ShopProject.Model.Domain.ProductUnit.ProductUnit item);
        public ShopProject.Model.Domain.ProductUnit.ProductUnit GetUnitFromSession();
    }
}
