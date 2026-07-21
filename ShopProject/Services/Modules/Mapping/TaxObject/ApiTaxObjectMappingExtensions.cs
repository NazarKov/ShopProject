using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.ObjectOwner;
using ShopProject.Services.Integration.Network.WebServerApi.DtoModels.TaxObject;
using System;
using System.Collections.Generic; 
using TaxObjectModel = ShopProject.Model.Domain.TaxObject.TaxObject;

namespace ShopProject.Services.Modules.Mapping.TaxObject
{
    public static class ApiTaxObjectMappingExtensions
    {
        public static CreateTaxObjectDto ToCreateTaxObject(this TaxObjectModel item)
        {
            return new CreateTaxObjectDto()
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
        public static IEnumerable<CreateTaxObjectDto> ToCreateTaxObject(this IEnumerable<TaxObjectModel> items)
        {
            var result = new List<CreateTaxObjectDto>();
            foreach (var item in items)
            {
                result.Add(ToCreateTaxObject(item));
            }
            return result;
        }
        public static TaxObjectModel ToTaxObject(this TaxObjectDto item)
        {
            return new TaxObjectModel()
            {
                ID= Guid.Parse(item.ID),
                Status = item.Status,
                C_DISTR = item.C_DISTR,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                TypeStatus = (TypeStatusTaxObject)item.TypeStatus,
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
        public static IEnumerable<TaxObjectModel> ToTaxObject(this IEnumerable<TaxObjectDto> items)
        {
            var result = new List<TaxObjectModel>();
            foreach (var item in items)
            {
                result.Add(ToTaxObject(item));
            }
            return result;
        }
        public static List<CreateTaxObjectDto> ToObjectOwner(this IEnumerable<TaxObjectModel> items)
        {
            var result = new List<CreateTaxObjectDto>();
            foreach (var item in items)
            {
                result.Add(ToCreateTaxObject(item));
            }
            return result;
        }
    }
}
