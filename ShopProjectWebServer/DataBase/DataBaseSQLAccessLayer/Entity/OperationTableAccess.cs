using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationTableAccess : IOperationTableAccess
    {
        private readonly ContextDataBase _contextDataBase;
        public OperationTableAccess(ContextDataBase contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }
        public int Add(OperationEntity item)
        {
            if (item.MAC != null)
            {
                item.MAC.WorkingShifts = _contextDataBase.WorkingShift.Find(item.MAC.WorkingShifts.ID); 
                item.MAC.SequenceNumber = _contextDataBase.MediaAccessControls.Where(m => m.WorkingShifts.ID == item.MAC.WorkingShifts.ID).Count();
                item.MAC.OperationsRecorder = _contextDataBase.OperationsRecorders.Find(item.MAC.OperationsRecorder.ID);
                item.MAC.Operation = null;
                _contextDataBase.MediaAccessControls.Add(item.MAC);
            }
            if (item.Shift != null)
            {
                item.Shift = _contextDataBase.WorkingShift.Find(item.Shift.ID);
            }

            if (item.Discount != null)
            {
                item.Discount = _contextDataBase.Discounts.Find(item.Discount.ID);
            }

            _contextDataBase.Operations.Add(item);
            _contextDataBase.SaveChanges();
            return item.ID; 
        }

        public void Delete(OperationEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationEntity> GetAll()
        {
            return _contextDataBase.Operations.AsNoTracking().ToList();
        }

        public decimal GetAmountOfOfficialFundsIssuedCashForShift(int shiftId)
        {
            var result = _contextDataBase.Operations
                           .Where(o => o.TypeOperation == TypeOperation.WithdrawalMoney && o.Shift.ID == shiftId)
                           .Sum(o => o.TotalPayment);
            return result; 
        }

        public decimal GetAmountOfOfficialFundsReceivedCashForShift(int shiftId)
        {
            var result = _contextDataBase.Operations
                           .Where(o => o.TypeOperation == TypeOperation.DepositMoney && o.Shift.ID == shiftId)
                           .Sum(o => o.TotalPayment);
            return result; 
        }

        public OperationEntity GetLastItem(int shiftId)
        {
            return _contextDataBase.Operations.Include(d=>d.Discount).OrderBy(i => i.ID).Where(t => t.TypeOperation == TypeOperation.FiscalCheck).Last(i => i.Shift.ID == shiftId);
        }

        public OperationEntity GetLatsItem()
        {
            IQueryable<OperationEntity> query = _contextDataBase.Operations.Include(d => d.Discount).AsNoTracking(); 
            return query.Where(o => o.TypeOperation == TypeOperation.FiscalCheck).Last();
        }

        public decimal GetTotalAmountOfFundsIssuedForShift(int shiftId)
        { 
            var result = _contextDataBase.Operations
                            .Where(o => o.TypeOperation == TypeOperation.FiscalCheck && o.Shift.ID == shiftId)
                            .Sum(o => o.RestPayment);
            return result; 
        }

        public decimal GetTotalOperationForShift(int shiftId)
        { 
            var operaionts = _contextDataBase.Operations.Where(o => o.Shift.ID == shiftId)
                .Where(t => t.TypeOperation == TypeOperation.FiscalCheck);
            if (operaionts != null)
            {
                return operaionts.Count();
            }
            return 0;
        }

        public decimal GetTotalReturnOperationForShift(int shiftId)
        {
            var operaionts = _contextDataBase.Operations.Where(o => o.Shift.ID == shiftId).Where(t => t.TypeOperation == TypeOperation.ReturnCheck);
            if (operaionts != null)
            {
                return operaionts.Count();
            }
            return 0;
        }

        public decimal GetTotalSumForShift(int shiftId)
        {
            var result = _contextDataBase.Operations
                            .Where(o => o.TypeOperation == TypeOperation.FiscalCheck && o.Shift.ID == shiftId)
                            .Sum(o => o.TotalPayment); 
            return result;
        }

        public void Update(OperationEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
