using ShopProject.Model.Domain.TaxObject;
using ShopProject.Model.UI.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.TaxObject
{
    internal static class UiTaxObjectMappingExtensions
    {
        public static TaxObjectModel ToTaxObjectModel(this ShopProject.Model.Domain.TaxObject.TaxObject item)
        {
            return new TaxObjectModel()
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
                LoadTaxServer = item.LoadTaxServer,
            };
        }



        public static ShopProject.Model.Domain.TaxObject.TaxObject ToTaxObject(this TaxObjectModel item)
        {
            return new ShopProject.Model.Domain.TaxObject.TaxObject()
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
                LoadTaxServer = item.LoadTaxServer,
            };
        }

        public static IEnumerable<ShopProject.Model.Domain.TaxObject.TaxObject> ToTaxObjectModel(this IEnumerable<TaxObjectModel> items)
        {
            var result = new List<ShopProject.Model.Domain.TaxObject.TaxObject>();
            foreach (var item in items)
            {
                result.Add(item.ToTaxObject());
            }
            return result;
        }

        public static IEnumerable<TaxObjectModel> ToTaxObjectModel(this IEnumerable<ShopProject.Model.Domain.TaxObject.TaxObject> items)
        {
            var result = new List<TaxObjectModel>();
            foreach (var item in items)
            {
                result.Add(item.ToTaxObjectModel());
            }
            return result;
        }
        public static IEnumerable<TaxObjectSelectItemModel> ToTaxObjectSelctedItemModel(this IEnumerable<ShopProject.Model.Domain.TaxObject.TaxObject> items)
        {
            var result = new List<TaxObjectSelectItemModel>();
            foreach (var item in items)
            {
                result.Add(new TaxObjectSelectItemModel() { TaxObject = item.ToTaxObjectModel(), IsActive = false });
            }
            return result;
        }
        public static IEnumerable<ShopProject.Model.Domain.TaxObject.TaxObject> ToTaxObjectModel(this IEnumerable<TaxObjectSelectItemModel> items)
        {
            var result = new List<ShopProject.Model.Domain.TaxObject.TaxObject>();
            foreach (var item in items)
            {
                result.Add(item.TaxObject.ToTaxObject());
            }
            return result;
        }

    }
}
