using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationTableAccess
    {
        int Add(OperationEntity item);
        void Update(OperationEntity item);
        void Delete(OperationEntity item);
        IEnumerable<OperationEntity> GetAll();
        OperationEntity GetLastItem(int shiftId);
        OperationEntity GetLatsItem();
        decimal GetTotalSumForShift(int shiftId);
        decimal GetTotalAmountOfFundsIssuedForShift(int shiftId);
        decimal GetTotalOperationForShift(int shiftId);
        decimal GetAmountOfOfficialFundsIssuedCashForShift(int shiftId);
        decimal GetAmountOfOfficialFundsReceivedCashForShift(int shiftId);
    }
}
