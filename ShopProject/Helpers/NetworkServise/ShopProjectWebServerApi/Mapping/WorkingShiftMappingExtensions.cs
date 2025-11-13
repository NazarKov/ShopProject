using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.WorkingShift;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping
{
    public static class WorkingShiftMappingExtensions
    {
        public static CreateWorkingShiftDto ToCreateWorkingShiftDto(this WorkingShift workingShift)
        {
            var item = new CreateWorkingShiftDto()
            {
                TypeShiftCrateAt = (int)TypeWorkingShift.OpenShift,
                TypeRRO = workingShift.TypeRRO,
                UserOpenShiftID = workingShift.UserOpenShift.ID.ToString(),
                DataPacketIdentifier = workingShift.DataPacketIdentifier,
                FactoryNumberRRO = workingShift.FactoryNumberRRO,
                FiscalNumberRRO = workingShift.FiscalNumberRRO,
                MACCreateAtID = workingShift.MACCreateAt.ID,
            }; 
            return item;
        }
        public static UpdateWorkingShiftDto ToUpdateWorkingShiftDto(this WorkingShift workingShift) 
        { 
            var shift = new UpdateWorkingShiftDto();
            shift.ID= workingShift.ID;
            shift.AmountOfFundsIssued = workingShift.AmountOfFundsIssued;
            shift.AmountOfFundsReceived = workingShift.AmountOfFundsReceived;

            shift.AmountOfOfficialFundsIssuedCard = workingShift.AmountOfOfficialFundsIssuedCard;
            shift.AmountOfOfficialFundsReceivedCard = workingShift.AmountOfOfficialFundsReceivedCard;

            shift.AmountOfOfficialFundsReceivedCash = workingShift.AmountOfOfficialFundsReceivedCash;
            shift.AmountOfOfficialFundsIssuedCash = workingShift.AmountOfOfficialFundsIssuedCash;

            shift.DataPacketIdentifier = workingShift.DataPacketIdentifier;
            shift.FactoryNumberRRO = workingShift.FactoryNumberRRO;
            shift.FiscalNumberRRO = workingShift.FiscalNumberRRO;

            shift.MACCreateAtID = workingShift.MACCreateAt.ID;
            shift.MACEndAtID = workingShift.MACEndAt.ID;

            if (workingShift.UserOpenShift != null)
            {
                shift.UserOpenShiftID = workingShift.UserOpenShift.ID.ToString();
            }
            if(workingShift.UserCloseShift != null)
            {
                shift.UserCloseShiftID = workingShift.UserCloseShift.ID.ToString();
            }

            shift.TotalCheckForShift = workingShift.TotalCheckForShift;
            shift.TotalReturnCheckForShift = workingShift.TotalReturnCheckForShift;

            shift.TypeRRO = workingShift.TypeRRO; 

            shift.TypeShiftCrateAt = (int) workingShift.TypeShiftCrateAt; 
            shift.TypeShiftEndAt = (int)workingShift.TypeShiftEndAt; 
            return shift;
        }
        public static WorkingShift ToWorkingShift(this WorkingShiftDto workingShift)
        {
            var shift = new WorkingShift();
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

            shift.MACCreateAt = new MediaAccessControl() { ID = workingShift.MACCreateAtID }; 
            shift.MACEndAt = new MediaAccessControl() { ID = workingShift.MACEndAtID };

            if (workingShift.UserOpenShiftID != null)
            {
                shift.UserOpenShift = new User() { ID = Guid.Parse(workingShift.UserOpenShiftID) }; 
            }

            if(workingShift.UserCloseShiftID != null)
            {
                shift.UserCloseShift = new User() { ID = Guid.Parse(workingShift.UserCloseShiftID) }; 
            }

            shift.TotalCheckForShift = workingShift.TotalCheckForShift;
            shift.TotalReturnCheckForShift = workingShift.TotalReturnCheckForShift;

            shift.TypeRRO = workingShift.TypeRRO;

            shift.TypeShiftCrateAt = (TypeWorkingShift)workingShift.TypeShiftCrateAt;
            shift.TypeShiftEndAt = (TypeWorkingShift)workingShift.TypeShiftEndAt;
            return shift;
        }
    }
}
