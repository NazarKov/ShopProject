 
using ShopProjectWebServer.Api.DtoModels.WorkingShift; 
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;

namespace ShopProjectWebServer.Api.Mappings
{
    public static class WorkingShiftMappingExtensions
    {
        public static WorkingShiftEntity ToWorkingShiftEntity(this CreateWorkingShiftDto workingShift)
        {
            var entity = new WorkingShiftEntity();


            entity.TypeRRO = workingShift.TypeRRO;
            entity.FiscalNumberRRO = workingShift.FiscalNumberRRO;
            entity.UserOpenShift = new UserEntity() { ID = Guid.Parse(workingShift.UserOpenShiftID) };
            entity.DataPacketIdentifier = workingShift.DataPacketIdentifier;
            entity.FactoryNumberRRO = workingShift.FactoryNumberRRO; 
            entity.CreateAt = DateTime.Now;

            if (workingShift.MACCreateAt != null) 
            {
                entity.MACCreateAt = workingShift.MACCreateAt.ToMediaAccessEntity();
            }

            Enum.TryParse(workingShift.TypeShiftCrateAt.ToString(), out TypeWorkingShift type);
            entity.TypeShiftCrateAt = type; 
            return entity;
        }

        public static WorkingShiftEntity ToWorkingShiftEntity(this UpdateWorkingShiftDto workingShift)
        { 

            var shift = new WorkingShiftEntity();
            shift.ID = workingShift.ID;
            shift.AmountOfFundsIssued = workingShift.AmountOfFundsIssued;
            shift.AmountOfFundsReceived = workingShift.AmountOfFundsReceived;

            shift.AmountOfOfficialFundsIssuedCard = workingShift.AmountOfOfficialFundsIssuedCard;
            shift.AmountOfOfficialFundsReceivedCard = workingShift.AmountOfOfficialFundsReceivedCard;

            shift.AmountOfOfficialFundsReceivedCash = workingShift.AmountOfOfficialFundsReceivedCash;
            shift.AmountOfOfficialFundsIssuedCash = workingShift.AmountOfOfficialFundsIssuedCash;

            shift.DataPacketIdentifier = workingShift.DataPacketIdentifier;
            shift.FactoryNumberRRO = workingShift.FactoryNumberRRO;
            shift.FiscalNumberRRO = workingShift.FiscalNumberRRO;

            if (workingShift.MACCreateAt != null) 
            {
                shift.MACCreateAt = workingShift.MACCreateAt.ToMediaAccessEntity();
            }

            if (workingShift.MACEndAt != null)
            {
                shift.MACEndAt = workingShift.MACEndAt.ToMediaAccessEntity();
            } 

            shift.UserOpenShift = new UserEntity() { ID = Guid.Parse(workingShift.UserOpenShiftID) };
            shift.UserCloseShift = new UserEntity() { ID= Guid.Parse(workingShift.UserCloseShiftID) };

            shift.TotalCheckForShift = workingShift.TotalCheckForShift;
            shift.TotalReturnCheckForShift = workingShift.TotalReturnCheckForShift;

            shift.TypeRRO = workingShift.TypeRRO; 
            shift.EndAt = DateTime.Now;

            Enum.TryParse(workingShift.TypeShiftCrateAt.ToString(), out TypeWorkingShift type);
            shift.TypeShiftCrateAt = type;

            Enum.TryParse(workingShift.TypeShiftEndAt.ToString(), out TypeWorkingShift typeShift);
            shift.TypeShiftEndAt = typeShift; 
            return shift;
        }

        public static WorkingShiftDto ToWorkingShiftDto(this WorkingShiftEntity shift)
        {
            var result = new WorkingShiftDto()
            {
                TotalCheckForShift = shift.TotalCheckForShift,
                TotalReturnCheckForShift = shift.TotalReturnCheckForShift,
                TypeShiftCrateAt = (int)shift.TypeShiftCrateAt,
                TypeShiftEndAt = (int)shift.TypeShiftEndAt, 
                AmountOfFundsIssued = shift.AmountOfFundsIssued,
                AmountOfFundsReceived = shift.AmountOfFundsReceived,
                AmountOfOfficialFundsIssuedCard = shift.AmountOfOfficialFundsIssuedCard,
                AmountOfOfficialFundsIssuedCash = shift.AmountOfOfficialFundsIssuedCash,
                AmountOfOfficialFundsReceivedCard = shift.AmountOfOfficialFundsReceivedCard,
                AmountOfOfficialFundsReceivedCash = shift.AmountOfOfficialFundsReceivedCash,
                DataPacketIdentifier = shift.DataPacketIdentifier,
                FactoryNumberRRO = shift.FactoryNumberRRO,
                FiscalNumberRRO = shift.FiscalNumberRRO,
                ID = shift.ID, 
                TypeRRO = shift.TypeRRO,
            };

            if (shift.UserCloseShift != null)
            {
                result.UserCloseShiftID = shift.UserCloseShift.ID.ToString();
            }
            if (shift.UserOpenShift != null)
            {
                result.UserOpenShiftID = shift.UserOpenShift.ID.ToString();
            }

            if (shift.MACIdCreateAt != null) 
            {
                result.MACCreateAtID = shift.MACIdCreateAt.Value;
            }

            if(shift.MACIdEndAt != null)
            {
                result.MACEndAtID = shift.MACIdEndAt.Value;
            }

            return result;
        }
    }
}
