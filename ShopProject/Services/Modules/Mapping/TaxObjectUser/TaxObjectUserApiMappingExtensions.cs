using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObjectUser;
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.Services.Modules.Mapping.UserRole;
using System.Collections;
using System.Collections.Generic;

namespace ShopProject.Services.Modules.Mapping.TaxObjectUser
{
    internal static class TaxObjectUserApiMappingExtensions
    {
        public static ShopProject.Model.Domain.TaxObjectUser.TaxObjectUser ToTaxObjectUser(this TaxObjectUserDto item,IEnumerable<ShopProject.Model.Domain.UserRole.UserRole> userRole)
        {
            var result = new ShopProject.Model.Domain.TaxObjectUser.TaxObjectUser()
            {
                ID = item.ID,
            };
            if (item.TaxObject != null)
            {
                result.TaxObject = item.TaxObject.ToTaxObject();
                if (item.OperationRecorder != null)
                {
                    result.OperationRecorders = item.OperationRecorder.ToOperationRecorder();
                }
            }
            if (item.User != null)
            {
                result.User = item.User.ToUser(userRole.ToUserRoleDto());
            }
            return result;
        }

        public static IEnumerable<ShopProject.Model.Domain.TaxObjectUser.TaxObjectUser> ToTaxObjectUser(this IEnumerable<TaxObjectUserDto> items, IEnumerable<ShopProject.Model.Domain.UserRole.UserRole> userRole)
        {
            var result = new List<ShopProject.Model.Domain.TaxObjectUser.TaxObjectUser>();
            foreach (var item in items)
            {
                result.Add(ToTaxObjectUser(item, userRole));
            }
            return result;
        }
    }
}
