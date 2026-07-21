using ShopProjectWebServer.Api.DtoModels.TaxObjectUser;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObject;
using ShopProjectWebServer.Services.Modules.Mapping.User;

namespace ShopProjectWebServer.Services.Modules.Mapping.TaxObjectUser
{
    public static class TaxObjectUserApiMappingExtensions
    {
        public static TaxObjectUserDto ToTaxObjectUserDto(this ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser item)
        {
            var result = new TaxObjectUserDto()
            {
                ID = item.ID,
            };
            if (item.TaxObject != null)
            {
                result.TaxObject = item.TaxObject.ToTaxObjectDto();
                if(item.OperationRecorder!= null)
                {
                    result.OperationRecorder = item.OperationRecorder.ToOperationRecorderDto();
                }
            }
            if (item.User != null)
            {
                result.User = item.User.ToUserDto();
            }
            return result;
        }
        public static IEnumerable<TaxObjectUserDto> ToTaxObjectUserDto(this IEnumerable<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser> items)
        {
            var result = new List<TaxObjectUserDto>();
            foreach(var item in items) 
            {
                result.Add(ToTaxObjectUserDto(item));
            }
            return result;
        }
    }
}
