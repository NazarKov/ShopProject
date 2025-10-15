using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.OperationRecorderUser;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class OperationRecorderAndUserMappingExtensions
    {
        public static OperationsRecorderEntity ToOperationRecorderEntity(this BindingUserToOperationRecorderDto item)
        {
            return new OperationsRecorderEntity() { ID = Guid.Parse(item.ID) };
        }

        public static IEnumerable<OperationsRecorderEntity> ToOperationRecordersEntity(this IEnumerable<BindingUserToOperationRecorderDto> items)
        {
            var result = new List<OperationsRecorderEntity>();

            foreach (var item in items) {
            
                result.Add(ToOperationRecorderEntity(item));
            }
            return result;
        } 
    }
}
