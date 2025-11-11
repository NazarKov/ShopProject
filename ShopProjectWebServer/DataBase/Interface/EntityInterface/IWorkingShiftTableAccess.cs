using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IWorkingShiftTableAccess
    {
        int Add(WorkingShiftEntity item);
        void Update(WorkingShiftEntity item);
        void UpdateParameter(Guid id, string nameParameter, object valueParameter);
        void Delete(WorkingShiftEntity item);
        IEnumerable<WorkingShiftEntity> GetAll();  
        WorkingShiftEntity GetById(int id);

    }
}
