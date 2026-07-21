using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IWorkingShiftTableAccess
    {
        public Task<WorkingShiftEntity> Add(WorkingShiftEntity item);
        public Task<WorkingShiftEntity> Update(WorkingShiftEntity item); 
        void UpdateParameter(Guid id, string nameParameter, object valueParameter);
        void Delete(WorkingShiftEntity item);
        IEnumerable<WorkingShiftEntity> GetAll();  
        WorkingShiftEntity GetById(int id);

        public int GetLastId(string fisclaNumberRRo);


    }
}
