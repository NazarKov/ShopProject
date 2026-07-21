using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl;
using ShopProjectWebServer.Services.Modules.Mapping.User;
using WorkingShiftModel = ShopProjectWebServer.Models.Domain.WorkingShift.WorkingShift;

namespace ShopProjectWebServer.Services.Modules.Mapping.WorkingShift
{
    public static class WorkingShiftEntityMappingExtensions
    {

        public static WorkingShiftModel ToWorkicingShift(this WorkingShiftEntity item)
        {
            var result = new WorkingShiftModel()
            {
                ID = item.ID,
                DataPacketIdentifier = item.DataPacketIdentifier,
                AmountOfFundsIssued = item.AmountOfFundsIssued,
                AmountOfFundsReceived = item.AmountOfFundsReceived,
                AmountOfOfficialFundsIssuedCard = item.AmountOfOfficialFundsIssuedCard,
                AmountOfOfficialFundsIssuedCash = item.AmountOfOfficialFundsIssuedCash,
                AmountOfOfficialFundsReceivedCard = item.AmountOfOfficialFundsReceivedCard,
                AmountOfOfficialFundsReceivedCash = item.AmountOfOfficialFundsReceivedCash,
                CreateAt = item.CreateAt,
                EndAt = item.EndAt,
                FactoryNumberRRO = item.FactoryNumberRRO,
                FiscalNumberRRO = item.FiscalNumberRRO,
                TotalCheckForShift = item.TotalCheckForShift,
                TotalReturnCheckForShift = item.TotalReturnCheckForShift,
                TypeRRO = item.TypeRRO,
                TypeShiftCrateAt = (TypeWorkingShift)item.TypeShiftCrateAt,
                TypeShiftEndAt = (TypeWorkingShift)item.TypeShiftEndAt,
            };

            if (item.UserOpenShift != null)
            {
                result.UserOpenShift = item.UserOpenShift.ToUser();
            }
            if (item.UserCloseShift != null)
            {
                result.UserCloseShift = item.UserCloseShift.ToUser();
            }
            if (item.MACCreateAt != null)
            {
                result.MACCreateAt = item.MACCreateAt.ToMediaAccessControl();
            }
            if (item.MACEndAt != null)
            {
                result.MACEndAt = item.MACEndAt.ToMediaAccessControl();
            }

            return result;
        }

        public static WorkingShiftEntity ToWorkingShiftEntity(this WorkingShiftModel item)
        {
            var result = new WorkingShiftEntity()
            {
                ID = item.ID,
                DataPacketIdentifier = item.DataPacketIdentifier,
                AmountOfFundsIssued = item.AmountOfFundsIssued,
                AmountOfFundsReceived = item.AmountOfFundsReceived,
                AmountOfOfficialFundsIssuedCard = item.AmountOfOfficialFundsIssuedCard,
                AmountOfOfficialFundsIssuedCash = item.AmountOfOfficialFundsIssuedCash,
                AmountOfOfficialFundsReceivedCard = item.AmountOfOfficialFundsReceivedCard,
                AmountOfOfficialFundsReceivedCash = item.AmountOfOfficialFundsReceivedCash,
                CreateAt = item.CreateAt,
                EndAt = item.EndAt,
                FactoryNumberRRO = item.FactoryNumberRRO,
                FiscalNumberRRO = item.FiscalNumberRRO,
                TotalCheckForShift = item.TotalCheckForShift,
                TotalReturnCheckForShift = item.TotalReturnCheckForShift,
                TypeRRO = item.TypeRRO,
                TypeShiftCrateAt = (ShopProjectDataBase.Helper.TypeWorkingShift)item.TypeShiftCrateAt,
                TypeShiftEndAt = (ShopProjectDataBase.Helper.TypeWorkingShift)item.TypeShiftEndAt,
            };

            if (item.UserOpenShift != null)
            {
                result.UserOpenShift = item.UserOpenShift.ToUserEntity();
            }
            if (item.UserCloseShift != null)
            {
                result.UserCloseShift = item.UserCloseShift.ToUserEntity();
            }
            if (item.MACCreateAt != null)
            {
                result.MACCreateAt = item.MACCreateAt.ToMediaAccessControlEntity();
            }
            if (item.MACEndAt != null)
            {
                result.MACEndAt = item.MACEndAt.ToMediaAccessControlEntity();
            } 
            return result;
        }

    }
}
