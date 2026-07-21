using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Models.Domain.Paginator;
using OperationRecorderModel = ShopProjectWebServer.Models.Domain.OperationRecorder.OperationRecorder;

namespace ShopProjectWebServer.Services.Modules.Mapping.OperationRecorder
{
    public static class OpearationRecorderApiMappingExtensions
    {

        public static OperationRecorderModel ToOperationRecorder(this CreateOperationRecorderDto item)
        {
            var result = new OperationRecorderModel()
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
        public static IEnumerable<OperationRecorderModel> ToOperationRecordersEntity(this IEnumerable<CreateOperationRecorderDto> items)
        {
            var result = new List<OperationRecorderModel>();
            foreach (var item in items)
            {
                result.Add(ToOperationRecorder(item));
            }
            return result;
        }

        public static OperationRecorderDto ToOpeartionRecorderDto(this OperationRecorderModel item)
        {
            return new OperationRecorderDto()
            {
                Status = item.Status,
                D_REG = item.D_REG,
                ID = item.ID.ToString(),
                TypeStatus = (int)item.TypeStatus,
                Address = item.Address,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
            };
        }
        public static IEnumerable<OperationRecorderDto> ToOperationRecorderDto(this IEnumerable<OperationRecorderModel> items)
        {
            var result = new List<OperationRecorderDto>();
            foreach (var item in items)
            {
                result.Add(item.ToOpeartionRecorderDto());
            }
            return result;
        }
        public static PaginatorDto<OperationRecorderDto, int> ToPaginatorDto(this Paginator<OperationRecorderModel, int> paginator)
        {
            var result = new PaginatorDto<OperationRecorderDto, int>()
            {
                CountItemPage = paginator.CountItemPage,
                DataType = paginator.DataType,
                Page = paginator.Page,
                Pages = paginator.Pages,
            };
            if (paginator.Data != null)
            {
                result.Data = paginator.Data.ToOperationRecorderDto();
            }
            return result;
        }
        public static Paginator<OperationRecorderModel, int> ToPaginator(this PaginatorDto<OperationRecorderDto, int> paginator)
        {
            var result = new Paginator<OperationRecorderModel, int>()
            {
                CountItemPage = paginator.CountItemPage,
                DataType = paginator.DataType,
                Page = paginator.Page,
                Pages = paginator.Pages,
            }; 
            return result;
        }

        public static OperationRecorderModel ToOpeartionRecorder(this OperationRecorderDto item)
        {
            return new OperationRecorderModel()
            {
                Status = item.Status,
                D_REG = item.D_REG,
                ID = Guid.Parse(item.ID),
                TypeStatus = (TypeStatusOperationRecorder)item.TypeStatus,
                Address = item.Address,
                FiscalNumber = item.FiscalNumber,
                LocalNumber = item.LocalNumber,
                Name = item.Name,
            };
        }
        public static IEnumerable<OperationRecorderModel> ToOpeartionRecorder(this IEnumerable<OperationRecorderDto> items)
        {
            var result = new List<OperationRecorderModel>();
            foreach (var item in items)
            {
                result.Add(item.ToOpeartionRecorder());
            }
            return result;
        }
    }
}
