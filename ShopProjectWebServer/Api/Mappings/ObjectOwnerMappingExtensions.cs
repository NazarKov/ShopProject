﻿using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.ObjectOwner;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class ObjectOwnerMappingExtensions
    {
        public static ObjectOwnerEntity ToObjectOwnerEntity(this DeleteObjectOwnerDto item)
        {
            return new ObjectOwnerEntity() { ID = item.ID };
        }

        public static ObjectOwnerEntity ToObjectOwnerEntity(this CreateObjectOwnerDto item)
        {
            var result = new ObjectOwnerEntity()
            {
                ID = item.ID,
                C_TERRIT = item.C_TERRIT,
                D_ACC_END = item.D_ACC_END,
                REG_NUM_OBJ = item.REG_NUM_OBJ,
                Address = item.Address,
                CodeObject = item.CodeObject,
                C_DISTR = item.C_DISTR,
                D_ACC_START = item.D_ACC_START,
                D_LAST_CH = item.D_LAST_CH,
                KATOTTG = item.KATOTTG,
                NameObject = item.NameObject,
                Status = item.Status,
                TypeObjectName = item.TypeObjectName,
                TypeOfRights = item.TypeOfRights,
            };

            Enum.TryParse(item.TypeStatus.ToString(), out TypeStatusObjectOwner type);
            result.TypeStatus = type; 
            return result;
        }

        public static IEnumerable<ObjectOwnerEntity> ToObjectOwnerEntity(this IEnumerable<CreateObjectOwnerDto> items)
        {
            var result = new List<ObjectOwnerEntity>();

            foreach (var item in items) 
            {
                result.Add(ToObjectOwnerEntity(item));
            }  
            return result;
        }
    }
}
