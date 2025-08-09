using ShopProjectDataBase.DataBase.Entities;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using ShopProjectWebServer.DataBase.Helpers;

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationRecorderTableAccess
    {
        void Add(OperationsRecorderEntity item);
        void AddRange(IEnumerable<OperationsRecorderEntity> items);
        void AddBinding(Guid idoperationrecoreder, Guid idobjectowner);
        void Update(OperationsRecorderEntity item);
        void Delete(OperationsRecorderEntity item);
        IEnumerable<OperationsRecorderEntity> GetAll();

        PaginatorData<OperationsRecorderEntity> GetAllPageColumn(double page, double countColumn, TypeStatusOperationRecorder status);
        PaginatorData<OperationsRecorderEntity> GetOperationRecorderByNamePageColumn(string name, double page, double countColumn, TypeStatusOperationRecorder status);
    }
} 
