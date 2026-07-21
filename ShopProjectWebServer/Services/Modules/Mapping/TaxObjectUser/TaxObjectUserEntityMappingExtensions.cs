using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObject;
using ShopProjectWebServer.Services.Modules.Mapping.User;

namespace ShopProjectWebServer.Services.Modules.Mapping.TaxObjectUser
{
    public static class TaxObjectUserEntityMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser ToTaxObjectUser(this TaxObjectUserEnitity item)
        {
            var result = new Models.Domain.TaxObjectUser.TaxObjectUser()
            {
                ID = item.ID,  
            };
            if(item.TaxObject != null)
            {
                result.TaxObject = item.TaxObject.ToTaxObject();
                if (item.TaxObject.OperationsRecorder != null)
                {
                    result.OperationRecorder = item.TaxObject.OperationsRecorder.ToOperationRecorder();
                }
            }
            if (item.User != null) 
            {
                result.User = item.User.ToUser();
            }
            return result;
        }
        public static IEnumerable<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser> ToTaxObjectUser(this IEnumerable<TaxObjectUserEnitity> items) 
        { 
            var result = new List<ShopProjectWebServer.Models.Domain.TaxObjectUser.TaxObjectUser>();
            foreach (var item in items) 
            {
                result.Add(ToTaxObjectUser(item));
            }
            return result;
        }
    }
}
