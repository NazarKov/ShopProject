using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class OperationTableAccess : IOperationTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public OperationTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }
        public int Add(OperationEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                { 
                    if (context.Operations != null)
                    {
                        if (item.MAC != null)
                        {
                            item.MAC.WorkingShifts = context.WorkingShift.Find(item.MAC.WorkingShifts.ID);

                            item.MAC.SequenceNumber = context.MediaAccessControls.Where(m => m.WorkingShifts.ID == item.MAC.WorkingShifts.ID).Count(); 
                            item.MAC.OperationsRecorder =
                                context.OperationsRecorders.Find(item.MAC.OperationsRecorder.ID);
                            item.MAC.Operation = null;
                            context.MediaAccessControls.Add(item.MAC);
                        }
                        if (item.Shift != null)
                        {
                            item.Shift = context.WorkingShift.Find(item.Shift.ID);
                        }

                        if (item.Discount != null)
                        {
                            item.Discount = context.Discounts.Find(item.Discount.ID);
                        }

                        context.Operations.Add(item); 
                        context.SaveChanges();
                    } 
                }
                return item.ID;
            }
        }

        public void Delete(OperationEntity item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OperationEntity> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load(); 

                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.ToList();
                    }
                    else
                    {
                        return new List<OperationEntity>();
                    }
                }
                return null;
            }
        }

        public decimal GetAmountOfOfficialFundsIssuedCashForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();

                    var operaionts = context.Operations.Where(o => o.TypeOperation == TypeOperation.WithdrawalMoney).Where(o => o.Shift.ID == shiftId);

                    decimal result = decimal.Zero;
                    foreach (var o in operaionts)
                    {
                        result += o.TotalPayment;
                    }
                    return result;
                }
                return 0;
            }
        }

        public decimal GetAmountOfOfficialFundsReceivedCashForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();

                    var operaionts = context.Operations.Where(o => o.TypeOperation ==  TypeOperation.DepositMoney).Where(o => o.Shift.ID == shiftId);

                    decimal result = decimal.Zero;
                    foreach (var o in operaionts)
                    {
                        result += o.TotalPayment;
                    }
                    return result;
                }
                return 0;
            }
        }

        public OperationEntity GetLastItem(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.OrderBy(i=>i.ID).Where(t=>t.TypeOperation==TypeOperation.FiscalCheck).Last(i=>i.Shift.ID==shiftId);
                    }
                    else
                    {
                        return new OperationEntity();
                    }
                }
                return null;
            }
        }

        public OperationEntity GetLatsItem()
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                IQueryable<OperationEntity> query = context.Operations.AsNoTracking();

                query = query.Where(o => o.TypeOperation == TypeOperation.FiscalCheck);

                var result = query.ToList();
                return result.Last();
            }
        }

        public decimal GetTotalAmountOfFundsIssuedForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                   
                    var operaionts = context.Operations.Where(o => o.TypeOperation ==  TypeOperation.FiscalCheck).Where(o=>o.Shift.ID== shiftId);

                    decimal result = decimal.Zero;
                    foreach(var o in operaionts)
                    {
                        result += o.RestPayment;
                    }
                    return result;
                } 
                return 0;
            }
        }

        public decimal GetTotalOperationForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();

                    var operaionts = context.Operations.Where(o => o.Shift.ID == shiftId)
                        .Where(t=>t.TypeOperation == TypeOperation.FiscalCheck);
                    if (operaionts != null)
                    {
                        return operaionts.Count();
                    }
                }
                return 0;
            }
        }

        public decimal GetTotalReturnOperationForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();

                    var operaionts = context.Operations.Where(o => o.Shift.ID == shiftId)
                        .Where(t => t.TypeOperation == TypeOperation.ReturnCheck);
                    if (operaionts != null)
                    {
                        return operaionts.Count();
                    }
                }
                return 0;
            }
        }

        public decimal GetTotalSumForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();

                    var operaionts = context.Operations.Where(o=>o.TypeOperation== TypeOperation.FiscalCheck).Where(o => o.Shift.ID == shiftId);

                    decimal result = decimal.Zero;
                    foreach (var o in operaionts)
                    {
                        result += o.TotalPayment;
                    }
                    return result;
                }
                return 0;
            }
        }

        public void Update(OperationEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
