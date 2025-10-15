using Azure;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class OperaionRecorderMappingExtensions
    { 
        public static OperationsRecorderEntity ToOperationRecorderEntity(this CreateOperationRecorderDto item)
        {
            var result = new OperationsRecorderEntity()
            {
                Status = item.Status,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name, 
            };

            Enum.TryParse(item.TypeStatus.ToString(), out TypeStatusOperationRecorder type);
            result.TypeStatus = type; 
            return result;
        }
        public static IEnumerable<OperationsRecorderEntity> ToOperationRecordersEntity(this IEnumerable<CreateOperationRecorderDto> items)
        {
            var result = new List<OperationsRecorderEntity>();
            foreach (var item in items) 
            {
                result.Add(ToOperationRecorderEntity(item));
            }
            return result;
        }
        public static OperationRecorderDto ToOperationRecorderDto(this OperationsRecorderEntity item) 
        {
            var result = new OperationRecorderDto()
            {
                ID = item.ID.ToString(),
                Status = item.Status,
                TypeStatus = (int)item.TypeStatus,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
            };
            if (item.ObjectOwner != null) 
            {
                result.ObjectOwner_ID = item.ObjectOwner.ID.ToString();
            }
            return result;
        }
        public static IEnumerable<OperationRecorderDto> ToOperationRecorderDto(this IEnumerable<OperationsRecorderEntity> items) 
        {
            var result = new List<OperationRecorderDto>();
            foreach(var item in items)
            {
                result.Add(ToOperationRecorderDto(item));
            }
            return result;
        }
    }
}