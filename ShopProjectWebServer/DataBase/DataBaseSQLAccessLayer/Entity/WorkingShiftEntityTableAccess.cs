using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class WorkingShiftEntityTableAccess : IWorkingShiftTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public WorkingShiftEntityTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public int Add(WorkingShiftEntity item)
        {
            _contextDataBase.WorkingShift.Load();
            _contextDataBase.Users.Load();
            if (_contextDataBase.WorkingShift != null)
            {
                item.UserOpenShift = _contextDataBase.Users.FirstOrDefault(i => i.ID == item.UserOpenShift.ID);

                if (item.MACCreateAt != null)
                {
                    var mac = _contextDataBase.MediaAccessControls.Find(item.MACCreateAt.ID);
                    if (mac != null)
                    {
                        item.MACCreateAt = mac;
                    }
                    else
                    {
                        item.MACCreateAt.Operation = null;
                        item.MACCreateAt.OperationsRecorder = _contextDataBase.OperationsRecorders.Find(item.MACCreateAt.OperationsRecorder.ID);
                    }
                }
                _contextDataBase.WorkingShift.Add(item);

                _contextDataBase.SaveChanges();

                var mediaAccessControls = _contextDataBase.MediaAccessControls.Find(item.MACCreateAt.ID);
                if (mediaAccessControls != null)
                {
                    mediaAccessControls.WorkingShifts = item;
                }

                _contextDataBase.SaveChanges();
            }
            return item.ID;
        }

        public void Delete(WorkingShiftEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkingShiftEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public WorkingShiftEntity GetById(int id)
        {
            _contextDataBase.WorkingShift.Load();
            _contextDataBase.Users.Load();
            _contextDataBase.MediaAccessControls.Load();

            var result = _contextDataBase.WorkingShift.Where(w => w.ID == id).First();
            return result;
        }

        public void Update(WorkingShiftEntity item)
        {
            _contextDataBase.WorkingShift.Load();
            _contextDataBase.Users.Load();
            _contextDataBase.MediaAccessControls.Load();
            if (_contextDataBase.WorkingShift != null)
            {
                var shift = _contextDataBase.WorkingShift.Find(item.ID);

                if (shift != null)
                {
                    shift.AmountOfFundsIssued = item.AmountOfFundsIssued;
                    shift.AmountOfFundsReceived = item.AmountOfFundsReceived;

                    shift.AmountOfOfficialFundsIssuedCard = item.AmountOfOfficialFundsIssuedCard;
                    shift.AmountOfOfficialFundsReceivedCard = item.AmountOfOfficialFundsReceivedCard;

                    shift.AmountOfOfficialFundsReceivedCash = item.AmountOfOfficialFundsReceivedCash;
                    shift.AmountOfOfficialFundsIssuedCash = item.AmountOfOfficialFundsIssuedCash;

                    shift.DataPacketIdentifier = item.DataPacketIdentifier;
                    shift.FactoryNumberRRO = item.FactoryNumberRRO;
                    shift.FiscalNumberRRO = item.FiscalNumberRRO;

                    if (item.UserOpenShift != null)
                    {
                        shift.UserOpenShift = _contextDataBase.Users.Find(item.UserOpenShift.ID);
                    }
                    if (item.UserCloseShift != null)
                    {
                        shift.UserCloseShift = _contextDataBase.Users.Find(item.UserCloseShift.ID);
                    }

                    shift.TotalCheckForShift = item.TotalCheckForShift;
                    shift.TotalReturnCheckForShift = item.TotalReturnCheckForShift;

                    shift.TypeRRO = item.TypeRRO;
                    shift.TypeShiftCrateAt = item.TypeShiftCrateAt;
                    shift.TypeShiftEndAt = item.TypeShiftEndAt;
                    shift.EndAt = item.EndAt;

                    if (item.MACEndAt != null)
                    {
                        shift.MACEndAt = item.MACEndAt;
                        shift.MACEndAt.WorkingShifts = shift;
                        shift.MACEndAt.SequenceNumber = _contextDataBase.MediaAccessControls.Where(m => m.WorkingShifts.ID == item.ID).Count();
                        shift.MACEndAt.OperationsRecorder = _contextDataBase.OperationsRecorders.Find(item.MACEndAt.OperationsRecorder.ID);
                        shift.MACEndAt.Operation = null;

                        _contextDataBase.MediaAccessControls.Add(shift.MACEndAt);
                        _contextDataBase.SaveChanges();


                    }
                }
            }
            _contextDataBase.SaveChanges();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }
    }
}
