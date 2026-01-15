using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
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
                    context.WorkingShift.Load();
                    context.MediaAccessControls.Load();
                    context.Operations.Load();
                    context.Discounts.Load();
                    if (context.Operations != null)
                    {
                        if (item.MAC != null)
                        {
                            item.MAC = context.MediaAccessControls.Find(item.MAC.ID);
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
                        
                    }
                    context.SaveChanges();
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

        public OperationEntity GetLastItem(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                    if (context.Operations.Count() != 0)
                    {
                        return context.Operations.OrderBy(i=>i.ID).Last(i=>i.Shift.ID==shiftId);
                    }
                    else
                    {
                        return new OperationEntity();
                    }
                }
                return null;
            }
        }

        public decimal GetTotalAmountOfFundsIssuedForShift(int shiftId)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.Operations.Load();
                   
                    var operaionts = context.Operations.Where(o=>o.Shift.ID== shiftId);

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

                    var operaionts = context.Operations.Where(o => o.Shift.ID == shiftId);
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

                    var operaionts = context.Operations.Where(o => o.Shift.ID == shiftId);

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
