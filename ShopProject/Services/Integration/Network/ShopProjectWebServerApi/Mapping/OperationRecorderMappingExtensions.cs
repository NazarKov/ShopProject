using ShopProject.Model.Domain.ObjectOwner;
using ShopProject.Model.Domain.OperationRecorder;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ShopProject.Services.Integration.Network.ShopProjectWebServerApi.Mapping
{
    public static class OperationRecorderMappingExtensions
    {
        public static CreateOperationRecorderDto ToOperationRecorder(this OperationRecorder item)
        {
            return new CreateOperationRecorderDto()
            {
                Status = item.Status,
                TypeStatus = (int)item.TypeStatus,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
            };
        }
        public static OperationRecorder ToOperationRecorder(this OperationRecorderDto item) 
        {
            var result = new OperationRecorder()
            {
                ID = Guid.Parse(item.ID),
                Status = item.Status,
                TypeStatus = (TypeStatusOperationRecorder)item.TypeStatus,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name, 
            };
            if(item.ObjectOwner_ID != null && item.ObjectOwner_ID != string.Empty)
            {
                result.ObjectOwner = new ObjectOwner() { ID = Guid.Parse(item.ObjectOwner_ID) };
            }
            return result;
        }
        public static IEnumerable<OperationRecorder> ToOperationRecorder(this IEnumerable<OperationRecorderDto> items)
        {
            var result = new List<OperationRecorder>();
            foreach (var item in items)
            {
                result.Add(ToOperationRecorder(item));
            }
            return result;
        }
        public static List<CreateOperationRecorderDto> ToOperationRecorder(this List<OperationRecorder> items)
        {
            var result = new List<CreateOperationRecorderDto>();
            foreach (var item in items)
            {
                result.Add(ToOperationRecorder(item));
            }
            return result;
        }
    }
}
