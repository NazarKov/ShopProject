using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Models.Domain.Enum;

namespace ShopProjectWebServer.Services.Modules.Mapping.TaxObject
{
    public static class TaxObjectEntityMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.TaxObject.TaxObject ToTaxObject(this TaxObjectEntity item)
        {
            return new Models.Domain.TaxObject.TaxObject()
            {
                ID = item.ID,
                D_ACC_END = item.D_ACC_END,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                C_DISTR = item.C_DISTR,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_TERRIT = item.C_TERRIT,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                NameOwner = item.NameOwner,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                Status = item.Status,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
                TypeStatus = (Models.Domain.Enum.TypeStatusTaxObject)item.TypeStatus,
            };
        }

        public static TaxObjectEntity ToTaxObjectEntity(this ShopProjectWebServer.Models.Domain.TaxObject.TaxObject item)
        {
            return new TaxObjectEntity()
            {
                ID = item.ID,
                D_ACC_END = item.D_ACC_END,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                C_DISTR = item.C_DISTR,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_TERRIT = item.C_TERRIT,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                NameOwner = item.NameOwner,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                Status = item.Status,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
                TypeStatus = (ShopProjectDataBase.Helper.TypeStatusTaxObject)item.TypeStatus,
            };
        }

        public static IEnumerable<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject> ToTaxObject(this IEnumerable<TaxObjectEntity> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject>();
            foreach (var item in items)
            {
                result.Add(ToTaxObject(item));
            }
            return result;
        }
        public static IEnumerable<TaxObjectEntity> ToTaxObjectEntity(this IEnumerable<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject> items)
        {
            var result = new List<TaxObjectEntity>();
            foreach (var item in items)
            {
                result.Add(ToTaxObjectEntity(item));
            }
            return result;
        }
    }
}
