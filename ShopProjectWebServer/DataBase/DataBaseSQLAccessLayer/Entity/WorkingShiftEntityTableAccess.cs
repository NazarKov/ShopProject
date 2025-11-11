using Microsoft.EntityFrameworkCore;
using ShopProjectDataBase.Context;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;

namespace ShopProjectWebServer.DataBase.DataBaseSQLAccessLayer.Entity
{
    public class WorkingShiftEntityTableAccess : IWorkingShiftTableAccess
    {
        private DbContextOptions<ContextDataBase> _option;
        public WorkingShiftEntityTableAccess(DbContextOptions<ContextDataBase> option)
        {
            _option = option;
        }

        public int Add(WorkingShiftEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.WorkingShift.Load();
                    context.Users.Load();
                    if (context.WorkingShift != null)
                    {
                        item.UserOpenShift = context.Users.FirstOrDefault(i => i.ID == item.UserOpenShift.ID);
                        
                        if(item.MACCreateAt != null && item.MACCreateAt.ID != null)
                        {
                            item.MACCreateAt = context.MediaAccessControls.Find(item.MACCreateAt.ID);
                        }
                        context.WorkingShift.Add(item);

                        context.SaveChanges();
                        var items = context.WorkingShift.Where(i=>i.UserOpenShift.ID == item.UserOpenShift.ID).ToList();

                        return items.Last().ID;
                    }
                }
            }
            return 0;
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
            using (ContextDataBase context = new ContextDataBase(_option))
            { 
                if(context != null)
                {
                    context.WorkingShift.Load();
                    context.Users.Load();
                    context.MediaAccessControls.Load();

                    var result = context.WorkingShift.Where(w => w.ID == id).First();
                    return result;
                }
                return null;
            }
        }

        public void Update(WorkingShiftEntity item)
        {
            using (ContextDataBase context = new ContextDataBase(_option))
            {
                if (context != null)
                {
                    context.WorkingShift.Load();
                    context.Users.Load();
                    context.MediaAccessControls.Load();
                    if (context.WorkingShift != null)
                    {  
                        var shift = context.WorkingShift.Find(item.ID);

                        shift.AmountOfFundsIssued = item.AmountOfFundsIssued;
                        shift.AmountOfFundsReceived = item.AmountOfFundsReceived;

                        shift.AmountOfOfficialFundsIssuedCard = item.AmountOfOfficialFundsIssuedCard;
                        shift.AmountOfOfficialFundsReceivedCard = item.AmountOfOfficialFundsReceivedCard;
                        
                        shift.AmountOfOfficialFundsReceivedCash = item.AmountOfOfficialFundsReceivedCash;
                        shift.AmountOfOfficialFundsIssuedCash = item.AmountOfOfficialFundsIssuedCash;

                        shift.DataPacketIdentifier = item.DataPacketIdentifier;
                        shift.FactoryNumberRRO = item.FactoryNumberRRO;
                        shift.FiscalNumberRRO = item.FiscalNumberRRO;

                   
                        shift.UserOpenShift = context.Users.Find(item.UserOpenShift.ID);
                        shift.UserCloseShift = context.Users.Find(item.UserCloseShift.ID);

                        shift.TotalCheckForShift = item.TotalCheckForShift;
                        shift.TotalReturnCheckForShift = item.TotalReturnCheckForShift;
                        
                        shift.TypeRRO = item.TypeRRO;
                        shift.TypeShiftCrateAt = item.TypeShiftCrateAt;
                        shift.TypeShiftEndAt = item.TypeShiftEndAt;
                        shift.EndAt = item.EndAt;
                    }
                    context.SaveChanges();
                }
            }
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }
    }
}
