using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.Services.Modules.Mapping.TaxObject;

namespace ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder
{
    public static class OpearationRecorderEntityMappingExtensions
    {
        public static ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder ToOperationRecorder(this OperationsRecorderEntity item)
        {
            var result = new Models.Domain.OperationRecorder.OperationRecorder()
            {
                ID = item.ID,
                D_REG = item.D_REG,
                Address = item.Address,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
                Status = item.Status,
                TypeStatus = (ShopProjectWebServer.Models.Domain.Enum.TypeStatusOperationRecorder)item.TypeStatus,
            };
            if (item.TaxObject != null)
            {
                result.TaxObject = item.TaxObject.ToTaxObject();
            }
            return result;
        }
        public static IEnumerable<ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder> ToOperationRecorder(this IEnumerable<OperationsRecorderEntity> items)
        {
            var result = new List<ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder>();
            foreach (var item in items) 
            {
                result.Add(item.ToOperationRecorder());
            }
            return result;
        }

        public static OperationsRecorderEntity ToOperationRecorderEntity(this ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder item)
        {
            return new OperationsRecorderEntity()
            {
                ID = item.ID,
                D_REG = item.D_REG,
                Address = item.Address,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
                Status = item.Status,
                TypeStatus = (TypeStatusOperationRecorder)item.TypeStatus,
            };
        }
        public static IEnumerable<OperationsRecorderEntity> ToOperationRecorderEntity(this IEnumerable<ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder> items)
        {
            var result = new List<OperationsRecorderEntity>();
            foreach (var item in items)
            {
                result.Add(item.ToOperationRecorderEntity());
            }
            return result;
        }
    }
}
