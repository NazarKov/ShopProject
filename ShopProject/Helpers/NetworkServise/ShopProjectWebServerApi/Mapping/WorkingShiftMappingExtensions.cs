using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.DtoModels.WorkingShift;
using ShopProject.UIModel.SalePage;
using ShopProjectDataBase.Entities;
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
                TypeShiftCrateAt = TypeWokingShiftDto.OpenShift,
                TypeRRO = workingShift.TypeRRO,
                UserOpenShiftID = workingShift.UserOpenShift.ID,
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

            shift.UserOpenShiftID = workingShift.UserOpenShift.ID;
            shift.UserCloseShiftID = workingShift.UserCloseShift.ID;

            shift.TotalCheckForShift = workingShift.TotalCheckForShift;
            shift.TotalReturnCheckForShift = workingShift.TotalReturnCheckForShift;

            shift.TypeRRO = workingShift.TypeRRO; 

            switch (workingShift.TypeShiftCrateAt)
            {
                case TypeWorkingShift.OpenShift:
                    {
                        shift.TypeShiftCrateAt = TypeWokingShiftDto.OpenShift;
                        break;
                    }
                default:
                    {
                        shift.TypeShiftCrateAt = TypeWokingShiftDto.None;
                        break;
                    }
            }

            switch (workingShift.TypeShiftEndAt)
            {
                case TypeWorkingShift.CloseShift:
                    {
                        shift.TypeShiftEndAt = TypeWokingShiftDto.CloseShift;
                        break;
                    }
                default:
                    {
                        shift.TypeShiftCrateAt = TypeWokingShiftDto.None;
                        break;
                    }
            }
            return shift;
        }
    }
}
