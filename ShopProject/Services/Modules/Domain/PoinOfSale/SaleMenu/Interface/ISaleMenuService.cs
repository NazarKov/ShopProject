using ShopProject.Model.Domain.Operation; 
using ShopProject.Services.Modules.Common; 
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface
{
    internal interface ISaleMenuService
    {
        public Task<OperationResult<bool>> SendCheck(OperationSaleInfo operationSaleInfo); 
    }
}
