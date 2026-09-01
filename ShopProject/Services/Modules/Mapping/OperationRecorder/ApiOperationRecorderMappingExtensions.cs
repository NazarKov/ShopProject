using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.OperationRecorder;
using ShopProject.Services.Modules.Mapping.TaxObject;
using System;
using System.Collections.Generic;

namespace ShopProject.Services.Modules.Mapping.OperationRecorder
{
    public static class ApiOperationRecorderMappingExtensions
    {
        public static CreateOperationRecorderDto ToCreateOperationRecorderDto(this ShopProject.Model.Domain.OperationRecorder.OperationRecorder item)
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
        public static OperationRecorderDto ToOperationRecorderDto(this ShopProject.Model.Domain.OperationRecorder.OperationRecorder item)
        {
            return new OperationRecorderDto()
            {
                Status = item.Status,
                TypeStatus = (int)item.TypeStatus,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
                ID = item.ID.ToString(), 
            };
        }

        public static ShopProject.Model.Domain.OperationRecorder.OperationRecorder ToOperationRecorder(this OperationRecorderDto item) 
        {
            var result = new ShopProject.Model.Domain.OperationRecorder.OperationRecorder()
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
            if(item.TaxObject != null)
            {
                result.TaxObject = item.TaxObject.ToTaxObject();
            }
            return result;
        }
        public static IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> ToOperationRecorder(this IEnumerable<OperationRecorderDto> items)
        {
            var result = new List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder>();
            foreach (var item in items)
            {
                result.Add(ToOperationRecorder(item));
            }
            return result;
        }
        public static List<CreateOperationRecorderDto> ToCreateOperationRecorderDto(this IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> items)
        {
            var result = new List<CreateOperationRecorderDto>();
            foreach (var item in items)
            {
                result.Add(ToCreateOperationRecorderDto(item));
            }
            return result;
        }
        public static List<OperationRecorderDto> ToOperationRecorderDto(this IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> items)
        {
            var result = new List<OperationRecorderDto>();
            foreach (var item in items)
            {
                result.Add(ToOperationRecorderDto(item));
            }
            return result;
        }
    }
}
