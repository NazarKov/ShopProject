using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Modules.MappingServise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping
{
    internal static class OperationRecorderMappingExtensions
    {
        public static ShopProject.Model.Domain.OperationRecorder.OperationRecorder ToOperationRecorder(this OperationRecorderModel item)
        {
            var result = new ShopProject.Model.Domain.OperationRecorder.OperationRecorder()
            {
                Status = item.Status,
                TypeStatus = item.TypeStatus,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                ID = item.ID,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
            };
            if (item.ObjectOwner != null)
            {
                result.ObjectOwner = item.ObjectOwner.ToObjectOwner();
            }
            return result;
        }

        public static OperationRecorderModel ToOperationRecorderModel(this ShopProject.Model.Domain.OperationRecorder.OperationRecorder item)
        {
            var result = new OperationRecorderModel()
            {
                Status = item.Status,
                TypeStatus = item.TypeStatus,
                Address = item.Address,
                D_REG = item.D_REG,
                FiscalNumber = item.FiscalNumber,
                ID = item.ID,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
            };
            if (item.ObjectOwner != null)
            {
                result.ObjectOwner = item.ObjectOwner.ToObjectOwnerModel();
            }
            return result;
        }
        public static IEnumerable<OperationRecorderModel> ToOperationRecorderModel(this IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> items)
        {
            var result = new List<OperationRecorderModel> { };
            foreach (var item in items)
            {
                result.Add(ToOperationRecorderModel(item));
            }
            return result;
        }
        public static IEnumerable<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> ToOperationRecorder(this IEnumerable<OperationRecorderModel> items)
        {
            var result = new List<ShopProject.Model.Domain.OperationRecorder.OperationRecorder> { };
            foreach (var item in items)
            {
                result.Add(ToOperationRecorder(item));
            }
            return result;
        }
    }
}
