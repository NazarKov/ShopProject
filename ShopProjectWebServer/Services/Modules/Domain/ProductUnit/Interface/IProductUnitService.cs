
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductUnit;
using ShopProjectWebServer.Services.Common;

namespace ShopProjectWebServer.Services.Modules.Domain.ProductUnit.Interface
{
    public interface IProductUnitService
    {
        public Task<OperationResult<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit>> Add(ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit unit);

        public Task<OperationResult<bool>> Update(ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit unit);
        public Task<OperationResult<bool>> UpdateParameter(string parameter, string value, ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit unit);

        public Task<OperationResult<bool>> Delete(int id);

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int>> GetByCodePageColumn(int code, 
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int> paginator);

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int>> GetByNamePageColumn(string name, 
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int> paginator);

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int>> GetPageColumn(
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit, int> paginator);

        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.ProductUnit.ProductUnit>> GetAll();
    }
}
