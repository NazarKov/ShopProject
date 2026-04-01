using ShopProject.Model.Domain.OperationRecorder;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorderUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping
{
    public static class OperationRecorderAndUserMappingExtensions
    {
        public static BindingUserToOperationRecorderDto ToOperationRecordersEntity(this OperationRecorder item)
        {
            return new BindingUserToOperationRecorderDto()
            {
                ID = item.ID.ToString()
            };
        }
        public static List<BindingUserToOperationRecorderDto> ToOperationRecordersEntity(this List<OperationRecorder> items)
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
