using ShopProjectWebServer.Api.DtoModels.Discount;
using ShopProjectWebServer.Api.Mappings; 
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Services.Common;
using ShopProjectWebServer.Services.Modules.Domain.Discount.Interface;
using ShopProjectWebServer.Services.Modules.Mapping.Discount;
using DiscountModel = ShopProjectWebServer.Models.Domain.Discount.Discount;

namespace ShopProjectWebServer.Services.Modules.Domain.Discount
{
    internal class DiscountService : IDiscountService
    {
        private IDataBaseService _service; 
        public DiscountService(IDataBaseService service)
        {
            _service = service; 
        }
        public OperationResult<int> Add(DiscountModel discount)
        {  
            try
            {
                var id = _service.DataBaseAccess.DiscountTable.Add(discount.ToDiscountEntity());
                if (id != 0)
                {
                    return OperationResult<int>.Success(id);
                }
                else
                {
                    return OperationResult<int>.Fail("Error");
                }
            }
            catch(Exception ex)
            {
                return OperationResult<int>.Fail(ex.Message, Common.Enum.ErrorType.Server);
            }
            
        }

        public void Get(string token, DiscountDto discountDto)
        {
            throw new NotImplementedException();
        }
    }
}
