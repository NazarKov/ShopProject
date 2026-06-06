using ShopProject.Model.Domain.ObjectOwner;
using ShopProject.Model.UI.ObjectOwner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.ObjectOwner
{
    internal static class UiObjectOwnerMappingExtensions
    {
        public static ObjectOwnerModel ToObjectOwnerModel(this ShopProject.Model.Domain.ObjectOwner.ObjectOwner item)
        {
            return new ObjectOwnerModel()
            {
                ID = item.ID,
                Status = item.Status,
                C_DISTR = item.C_DISTR,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                TypeStatus = item.TypeStatus,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_TERRIT = item.C_TERRIT,
                D_ACC_END = item.D_ACC_END,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                NameOwner = item.NameOwner,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
            };
        }



        public static ShopProject.Model.Domain.ObjectOwner.ObjectOwner ToObjectOwner(this ObjectOwnerModel item)
        {
            return new ShopProject.Model.Domain.ObjectOwner.ObjectOwner()
            {
                ID = item.ID,
                Status = item.Status,
                C_DISTR = item.C_DISTR,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                TypeStatus = item.TypeStatus,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_TERRIT = item.C_TERRIT,
                D_ACC_END = item.D_ACC_END,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                NameOwner = item.NameOwner,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
            };
        }

        public static IEnumerable<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> ToObjectOwner(this IEnumerable<ObjectOwnerModel> items)
        {
            var result = new List<ShopProject.Model.Domain.ObjectOwner.ObjectOwner>();
            foreach (var item in items)
            {
                result.Add(item.ToObjectOwner());
            }
            return result;
        }

        public static IEnumerable<ObjectOwnerModel> ToObjectOwnerModel(this IEnumerable<ShopProject.Model.Domain.ObjectOwner.ObjectOwner> items)
        {
            var result = new List<ObjectOwnerModel>();
            foreach (var item in items)
            {
                result.Add(item.ToObjectOwnerModel());
            }
            return result;
        }

    }
}
