using ShopProjectDataBase.Entities; 
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class OperationRecorderAndUserMappingExtensions
    {
        public static OperationsRecorderEntity ToOperationRecorderEntity(this BindingUserToOperationRecorderDto item)
        {
            return new OperationsRecorderEntity() { ID = item.ID };
        }

        public static IEnumerable<OperationsRecorderEntity> ToOperationRecordersEntity(this IEnumerable<BindingUserToOperationRecorderDto> items)
        {
            var result = new List<OperationsRecorderEntity>();

            foreach (var item in items) {
            
                result.Add(ToOperationRecorderEntity(item));
            }
            return result;
        }
        public static OperationRecorderUserDto ToOperationRecorderEntity(this OperationsRecorderUserEntity item)
        {
            return new OperationRecorderUserDto() { ID = item.ID , UserID = item.Users.ID };
        }

        public static IEnumerable<OperationRecorderUserDto> ToOperationRecordersDto(this IEnumerable<OperationsRecorderUserEntity> items) 
        {
            var result = new List<OperationRecorderUserDto>();

            foreach (var item in items)
            { 
                result.Add(ToOperationRecorderEntity(item));
            }
            return result;
        }
    }
}
