using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.Network.ShopProjectWebServerApi.DtoModels.WorkingShift;
using ShopProject.Services.Modules.Mapping.MediaAccess; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Modules.Mapping.WorkingShift
{
    public static class ApiWorkingShiftMappingExtensions
    {
        public static CreateWorkingShiftDto ToCreateWorkingShiftDto(this ShopProject.Model.Domain.WorkingShift.WorkingShift workingShift)
        {
            var item = new CreateWorkingShiftDto()
            {
                TypeShiftCrateAt = (int)TypeWorkingShift.OpenShift,
                TypeRRO = workingShift.TypeRRO,
                UserOpenShiftID = workingShift.UserOpenShift.ID.ToString(),
                DataPacketIdentifier = workingShift.DataPacketIdentifier,
                FactoryNumberRRO = workingShift.FactoryNumberRRO,
                FiscalNumberRRO = workingShift.FiscalNumberRRO,
                MACCreateAt = workingShift.MACCreateAt.ToCreatMediaAccessControlDto(),
            }; 
            return item;
        }
        public static UpdateWorkingShiftDto ToUpdateWorkingShiftDto(this ShopProject.Model.Domain.WorkingShift.WorkingShift workingShift) 
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

            shift.MACCreateAt = workingShift.MACCreateAt.ToCreatMediaAccessControlDto();
            shift.MACEndAt = workingShift.MACEndAt.ToCreatMediaAccessControlDto() ;

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
        public static ShopProject.Model.Domain.WorkingShift.WorkingShift ToWorkingShift(this WorkingShiftDto workingShift)
        {
            var shift = new ShopProject.Model.Domain.WorkingShift.WorkingShift();
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
                shift.UserOpenShift = new ShopProject.Model.Domain.User.User() { ID = Guid.Parse(workingShift.UserOpenShiftID) }; 
            }

            if(workingShift.UserCloseShiftID != null)
            {
                shift.UserCloseShift = new ShopProject.Model.Domain.User.User() { ID = Guid.Parse(workingShift.UserCloseShiftID) }; 
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
