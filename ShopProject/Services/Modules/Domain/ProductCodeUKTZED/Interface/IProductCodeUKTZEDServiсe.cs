using ShopProject.Model.Domain.Paginator; 
using ShopProject.Model.Enum; 
using ShopProject.Services.Modules.Common; 
using System.Collections.Generic; 
using System.Threading.Tasks; 
using ProductCodeUKTZEDModel = ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED;

namespace ShopProject.Services.Modules.Domain.ProductCodeUKTZED.Interface
{
    internal interface IProductCodeUKTZEDServiсe
    {
        public Task<OperationResult<ProductCodeUKTZEDModel>> Add(ProductCodeUKTZEDModel item);
        public Task<OperationResult<ProductCodeUKTZEDModel>> Update(ProductCodeUKTZEDModel item);
        public Task<OperationResult<bool>> Delete(ProductCodeUKTZEDModel item); 
        public Task<OperationResult<bool>> UpdateParameter(string parameter,object value, ProductCodeUKTZEDModel item);
        public Task<OperationResult<Paginator<ProductCodeUKTZEDModel,TypeStatusCodeUKTZED>>> GetPageColumn(int page, int countColumn, TypeStatusCodeUKTZED status);
        public Task<OperationResult<Paginator<ProductCodeUKTZEDModel,TypeStatusCodeUKTZED>>> SearchByBarCode(string item, int page, int countColumn, TypeStatusCodeUKTZED status);
        public Task<OperationResult<Paginator<ProductCodeUKTZEDModel,TypeStatusCodeUKTZED>>> SearchByName(string item, int page, int countColumn, TypeStatusCodeUKTZED status);



        public Task<IEnumerable<ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED>> GetFromSession();
        public void SetOnSession(ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED item);
        public ShopProject.Model.Domain.ProductCodeUKTZED.ProductCodeUKTZED GetProductCodeUKTZEDFromSession();
    }
}
