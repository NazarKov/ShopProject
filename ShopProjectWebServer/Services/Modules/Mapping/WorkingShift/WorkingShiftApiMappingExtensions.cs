using ShopProjectWebServer.Api.DtoModels.WorkingShift;
using ShopProjectWebServer.Api.Mappings;
using ShopProjectWebServer.Models.Domain.Enum;
using ShopProjectWebServer.Models.Domain.WorkingShift;
using ShopProjectWebServer.Services.Modules.Mapping.MediaAccessControl;
using WorkingShiftModel = ShopProjectWebServer.Models.Domain.WorkingShift.WorkingShift;

namespace ShopProjectWebServer.Services.Modules.Mapping.WorkingShift
{
    public static class WorkingShiftApiMappingExtensions
    {
        public static WorkingShiftModel ToWorkingShift(this CreateWorkingShiftDto workingShift)
        {
            var result = new WorkingShiftModel()
            {
                TypeRRO = workingShift.TypeRRO,
                FiscalNumberRRO = workingShift.FiscalNumberRRO,
                UserOpenShift = new Models.Domain.User.User() { ID = Guid.Parse(workingShift.UserOpenShiftID) },
                DataPacketIdentifier = workingShift.DataPacketIdentifier,
                FactoryNumberRRO = workingShift.FactoryNumberRRO,
                TypeShiftCrateAt = (TypeWorkingShift)workingShift.TypeShiftCrateAt,
            };
            result.CreateAt = DateTime.Now;

            if (workingShift.MACCreateAt != null)
            {
                result.MACCreateAt = workingShift.MACCreateAt.ToMediaAccessControl();
            } 
            return result;
        }

        public static WorkingShiftModel ToWorkingShift(this UpdateWorkingShiftDto workingShift)
        {

            var shift = new WorkingShiftModel()
            {
                ID = workingShift.ID,
                AmountOfFundsIssued = workingShift.AmountOfFundsIssued,
                AmountOfFundsReceived = workingShift.AmountOfFundsReceived,
                AmountOfOfficialFundsIssuedCard = workingShift.AmountOfOfficialFundsIssuedCard,
                AmountOfOfficialFundsIssuedCash = workingShift.AmountOfOfficialFundsIssuedCash,
                AmountOfOfficialFundsReceivedCard = workingShift.AmountOfOfficialFundsReceivedCard,
                AmountOfOfficialFundsReceivedCash = workingShift.AmountOfOfficialFundsReceivedCash,
                DataPacketIdentifier = workingShift.DataPacketIdentifier,
                FactoryNumberRRO = workingShift.FactoryNumberRRO,
                FiscalNumberRRO = workingShift.FiscalNumberRRO,
                TypeRRO = workingShift.TypeRRO,
                TypeShiftCrateAt = (TypeWorkingShift)workingShift.TypeShiftCrateAt,
                TypeShiftEndAt = (TypeWorkingShift)workingShift.TypeShiftEndAt,
                TotalCheckForShift = workingShift.TotalCheckForShift,
                TotalReturnCheckForShift = workingShift.TotalReturnCheckForShift,
            };

            if (workingShift.MACCreateAt != null)
            {
                shift.MACCreateAt = workingShift.MACCreateAt.ToMediaAccessControl();
            }

            if (workingShift.MACEndAt != null)
            {
                shift.MACEndAt = workingShift.MACEndAt.ToMediaAccessControl();
            }
            if (!string.IsNullOrEmpty(workingShift.UserOpenShiftID))
            {
                shift.UserOpenShift = new Models.Domain.User.User() { ID = Guid.Parse(workingShift.UserOpenShiftID) };
            }
            shift.UserCloseShift = new Models.Domain.User.User() { ID = Guid.Parse(workingShift.UserCloseShiftID) };
              
             
            shift.EndAt = DateTime.Now; 
            return shift;
        }



        public static WorkingShiftDto ToWorkingShiftDto(this WorkingShiftModel item)
        {
            var result = new WorkingShiftDto()
            {
                ID = item.ID,
                DataPacketIdentifier = item.DataPacketIdentifier,
                AmountOfFundsIssued = item.AmountOfFundsIssued,
                AmountOfFundsReceived = item.AmountOfFundsReceived,
                AmountOfOfficialFundsIssuedCard = item.AmountOfOfficialFundsIssuedCard,
                AmountOfOfficialFundsIssuedCash = item.AmountOfOfficialFundsIssuedCash,
                AmountOfOfficialFundsReceivedCard = item.AmountOfOfficialFundsReceivedCard,
                AmountOfOfficialFundsReceivedCash = item.AmountOfOfficialFundsReceivedCash,
                FactoryNumberRRO = item.FactoryNumberRRO,
                TotalCheckForShift = item.TotalCheckForShift,
                TotalReturnCheckForShift = item.TotalReturnCheckForShift,
                TypeRRO = item.TypeRRO,
                TypeShiftCrateAt = (int)item.TypeShiftCrateAt,
                TypeShiftEndAt = (int)item.TypeShiftEndAt,
                FiscalNumberRRO = item.FiscalNumberRRO, 
            };
            if(item.MACCreateAt != null)
            {
                result.MACCreateAtID = item.MACCreateAt.ID;
            }
            if (item.MACEndAt != null) 
            {
                result.MACEndAtID = item.MACEndAt.ID;
            }
            if(item.UserOpenShift != null)
            {
                result.UserOpenShiftID = item.UserOpenShift.ID.ToString();
            }
            if (item.UserCloseShift != null) 
            {
                result.UserCloseShiftID = item.UserCloseShift.ID.ToString();
            }
            return result;
        }

        public static WorkingShiftResourseDto ToWorkingShiftDto(this WorkingShiftResourse item)
        {
            return new WorkingShiftResourseDto()
            {
                ID = item.ID,
                MediaAccessControl = item.MediaAccessControl.ToMediaAccessControlDto(),
                OperationNumber = item.OperationNumber,
            };
        }
    }
}
