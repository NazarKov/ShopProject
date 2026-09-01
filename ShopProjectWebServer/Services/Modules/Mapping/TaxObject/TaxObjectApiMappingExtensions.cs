using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.TaxObject;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Models.Domain.Paginator;

namespace ShopProjectWebServer.Services.Modules.Mapping.TaxObject
{
    public static class TaxObjectApiMappingExtensions
    {
        public static TaxObjectDto ToTaxObjectDto(this ShopProjectWebServer.Models.Domain.TaxObject.TaxObject item)
        {
            return new TaxObjectDto()
            {
                ID = item.ID.ToString(),
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
                TypeStatus = (int)item.TypeStatus,
                LoadTaxServer = item.LoadTaxServer,
            };
        }
        public static IEnumerable<TaxObjectDto> ToTaxObjectDto(this IEnumerable<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject> items)
        {
            var result = new List<TaxObjectDto>();
            foreach (var item in items)
            {
                result.Add(ToTaxObjectDto(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.TaxObject.TaxObject ToTaxObject(this TaxObjectDto item)
        {
            return new ShopProjectWebServer.Models.Domain.TaxObject.TaxObject()
            {
                ID = new Guid(item.ID),
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
                TypeStatus = (TypeStatusTaxObject)item.TypeStatus,
                LoadTaxServer = item.LoadTaxServer,
            };
        }
        public static IEnumerable<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject> ToTaxObject(this IEnumerable<TaxObjectDto> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject>();
            foreach (var item in items)
            {
                result.Add(ToTaxObject(item));
            }
            return result;
        }

        public static Paginator<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject, int> ToPaginator(this PaginatorDto<TaxObjectDto, int> item)
        {
            return new Paginator<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject, int>()
            {
                CountItemPage = item.CountItemPage, 
                DataType = item.DataType,
                Page = item.Page,
                Pages = item.Pages,
            };
        }
        public static PaginatorDto<TaxObjectDto, int> ToPaginatorDto(this Paginator<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject, int> item)
        {
            var result = new PaginatorDto<TaxObjectDto, int>()
            {
                CountItemPage = item.CountItemPage, 
                DataType = item.DataType,
                Page = item.Page,
                Pages = item.Pages,
            };
            if (item.Data != null)
            {
                result.Data = item.Data.ToTaxObjectDto();
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.TaxObject.TaxObject ToTaxObject(this CreateTaxObjectDto item)
        {
            return new ShopProjectWebServer.Models.Domain.TaxObject.TaxObject()
            { 
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
                TypeStatus = (TypeStatusTaxObject)item.TypeStatus,
                LoadTaxServer = item.LoadTaxServer,
            };
        }

        public static IEnumerable<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject> ToTaxObject(this IEnumerable<CreateTaxObjectDto> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.TaxObject.TaxObject>();
            foreach (var item in items)
            {
                result.Add(ToTaxObject(item));
            }
            return result;
        }

        public static ShopProjectWebServer.Models.Domain.TaxObject.TaxObject ToTaxObject(this UpdateTaxObjectDto item)
        {
            return new ShopProjectWebServer.Models.Domain.TaxObject.TaxObject()
            {
                ID =Guid.Parse(item.ID),
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
                TypeStatus = (TypeStatusTaxObject)item.TypeStatus,
                LoadTaxServer = item.LoadTaxServer,
            };
        }
    }
}
