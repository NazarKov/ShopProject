using ShopProject.Model.Domain.ObjectOwner;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ObjectOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ObjectOwner
{
    public static class ApiObjectOwnerMappingExtensions
    {
        public static CreateObjectOwnerDto ToObjectOwner(this ShopProject.Model.Domain.ObjectOwner.ObjectOwner item)
        {
            return new CreateObjectOwnerDto()
            {
                Status = item.Status,
                C_DISTR = item.C_DISTR,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                TypeStatus = (int)item.TypeStatus,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_TERRIT = item.C_TERRIT,
                D_ACC_END = item.D_ACC_END,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
                NameOwner = item.NameOwner,
            };
        }
        public static ShopProject.Model.Domain.ObjectOwner.ObjectOwner ToObjectOwner(this ObjectOwnerDto item)
        {
            return new ShopProject.Model.Domain.ObjectOwner.ObjectOwner()
            {
                ID= Guid.Parse(item.ID),
                Status = item.Status,
                C_DISTR = item.C_DISTR,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                TypeStatus = (TypeStatusObjectOwner)item.TypeStatus,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_TERRIT = item.C_TERRIT,
                D_ACC_END = item.D_ACC_END,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
                NameOwner = item.NameOwner
            };
        }
        public static IEnumerable<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> ToObjectOwner(this IEnumerable<ObjectOwnerDto> items)
        {
            var result = new List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
            foreach (var item in items)
            {
                result.Add(ToObjectOwner(item));
            }
            return result;
        }
        public static List<CreateObjectOwnerDto> ToObjectOwner(this IEnumerable<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> items)
        {
            var result = new List<CreateObjectOwnerDto>();
            foreach (var item in items)
            {
                result.Add(ToObjectOwner(item));
            }
            return result;
        }
    }
}
