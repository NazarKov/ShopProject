using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.ObjectOwner;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class ObjectOwnerMappingExtensions
    {
        public static CreateObjectOwnerDto ToObjectOwner(this ObjectOwner item)
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
        public static ObjectOwner ToObjectOwner(this ObjectOwnerDto item)
        {
            return new ObjectOwner()
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
        public static IEnumerable<ObjectOwner> ToObjectOwner(this IEnumerable<ObjectOwnerDto> items)
        {
            var result = new List<ObjectOwner>();
            foreach (var item in items)
            {
                result.Add(ToObjectOwner(item));
            }
            return result;
        }
        public static List<CreateObjectOwnerDto> ToObjectOwner(this IEnumerable<ObjectOwner> items)
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
