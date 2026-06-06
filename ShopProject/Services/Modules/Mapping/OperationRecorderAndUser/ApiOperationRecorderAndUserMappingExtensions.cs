using ShopProject.Model.Domain.OperationRecorder;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorderUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.OperationRecorderAndUser
{
    public static class ApiOperationRecorderAndUserMappingExtensions
    {
        public static BindingUserToOperationRecorderDto ToOperationRecordersEntity(this ShopProject.Model.Domain.OperationRecorder.OperationRecorder item)
        {
            return new BindingUserToOperationRecorderDto()
            {
                ID = item.ID.ToString()
            };
        }
        public static List<BindingUserToOperationRecorderDto> ToOperationRecordersEntity(this List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> items)
        {
            var result = new List<BindingUserToOperationRecorderDto>();
            foreach (var item in items) 
            {
                result.Add(ToOperationRecordersEntity(item));
            }
            return result;
        }
    }
}
