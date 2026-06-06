using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Services.Common;

namespace ShopProjectWebServer.Services.Modules.Domain.Product.Interface
{
    public interface IProductService
    {
        public Task<OperationResult<Models.Domain.Product.Product>> AddAsync(Models.Domain.Product.Product product);
        public Task<OperationResult<bool>> AddRangeAsync(IEnumerable<Models.Domain.Product.Product> product);
        public Task<OperationResult<bool>> UpdateAsync(Models.Domain.Product.Product product);
        public Task<OperationResult<bool>> UpdateParameterAsync(string parameter, string value, Models.Domain.Product.Product product);
        public Task<OperationResult<bool>> UpdateRangeAsync(IEnumerable<Models.Domain.Product.Product> product);
        public OperationResult<Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int>> GetPageColumn(Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int> paginator);
        public OperationResult<Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int>> GetByNamePageColumn(string name, Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int> paginator);
        public OperationResult<Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int>> GetByBarCode(string barCode, Models.Domain.Paginator.Paginator<Models.Domain.Product.Product, int> paginator);
        public OperationResult<Models.Domain.Product.Product> GetByBarCode(string barCode, int status = 0);
        public OperationResult<Models.Domain.Product.ProductsInfo> GetInfoProducts();
    }
}
