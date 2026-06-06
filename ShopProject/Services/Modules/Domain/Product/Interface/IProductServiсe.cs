using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Enum; 
using ShopProject.Services.Modules.Common; 
using System.Collections;
using System.Collections.Generic; 
using System.Threading.Tasks;
using ProductModel = ShopProject.Model.Domain.Product.Product;

namespace ShopProject.Services.Modules.Domain.Product.Interface
{
    internal interface IProductServiсe
    {
        public Task<OperationResult<ProductModel>> Add(ProductModel product);
        public Task<OperationResult<ProductModel>> Update(ProductModel product);
        public Task<OperationResult<bool>> UpdateRange(List<ProductModel> items);
        public Task<OperationResult<bool>> UpdateParameter(string parameter, object value, ProductModel item);
        public OperationResult<IEnumerable<ProductModel>> ChangeParameterList(string parameter, object value, IEnumerable<ProductModel> items);
        public Task<OperationResult<Paginator<ProductModel,TypeStatusProduct>>> GetPageColumn(int page, int countColumn, TypeStatusProduct status);
        public Task<OperationResult<Paginator<ProductModel, TypeStatusProduct>>> SearchByName(string item, int page, int countColumn, TypeStatusProduct status);
        public Task<OperationResult<Paginator<ProductModel, TypeStatusProduct>>> SearchByBarCode(string item, int page, int countColumn, TypeStatusProduct status); 
      
        
        public Task<ProductsInfo> GetProductStatistics(); 
 
        public List<ProductModel> ContertIListToList(IList list);

        public void SetProductOnSession(ShopProject.Model.Domain.Product.Product item);
        public ShopProject.Model.Domain.Product.Product GetProductOnSession();
        public void SetProductsOnSession(List<ProductModel> items);
        public IEnumerable<ProductModel> GetProductsOnSession();
        public Task<ProductModel> SearchByBarCode(string item, TypeStatusProduct statusProduct);
        public string RemoveSeparatorBarCode(string item);
    }
}
