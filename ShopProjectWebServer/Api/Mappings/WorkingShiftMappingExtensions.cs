 
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
            entity.UserOpenShift = new UserEntity() { ID = workingShift.UserOpenShiftID };
            entity.DataPacketIdentifier = workingShift.DataPacketIdentifier;
            entity.FactoryNumberRRO = workingShift.FactoryNumberRRO;
            entity.MACCreateAt = new MediaAccessControlEntity() { ID=workingShift.MACCreateAtID };
            entity.CreateAt = DateTime.Now;
            
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

            shift.MACCreateAt = new MediaAccessControlEntity() { ID = workingShift.MACCreateAtID };
            shift.MACEndAt = new MediaAccessControlEntity() { ID = workingShift.MACEndAtID };

            shift.UserOpenShift = new UserEntity() { ID = workingShift.UserOpenShiftID };
            shift.UserCloseShift = new UserEntity() { ID= workingShift.UserCloseShiftID };

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

    }
}
