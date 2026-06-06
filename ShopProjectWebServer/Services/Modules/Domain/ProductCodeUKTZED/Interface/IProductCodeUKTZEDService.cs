using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.ProductCodeUKTZED;
using ShopProjectWebServer.Services.Common;

namespace ShopProjectWebServer.Services.Modules.Domain.ProductCodeUKTZED.Interface
{
    public interface IProductCodeUKTZEDService
    {
        public Task<OperationResult<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED>> AddAsync(ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED codeUKTZED);

        public Task<OperationResult<bool>> UpdateAsync(ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED codeUKTZED);
        public Task<OperationResult<bool>> UpdateParameterAsync(string parameter, string value, ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED codeUKTZEDE);

        public OperationResult<bool> Delete(int id);

        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int>> GetByCode(string code,
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int> paginator);
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int>> GetByNamePageColumn(string name,
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int> paginator);
        public OperationResult<ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int>> GetPageColumn(
            ShopProjectWebServer.Models.Domain.Paginator.Paginator<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED, int> paginator);

        public OperationResult<IEnumerable<ShopProjectWebServer.Models.Domain.ProductCodeUKTZED.ProductCodeUKTZED>> GetAll();
    }
}
