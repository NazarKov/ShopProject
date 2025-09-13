﻿using ShopProjectDataBase.Entities; 

namespace ShopProjectWebServer.DataBase.Interface.EntityInterface
{
    public interface IOperationTableAccess
    {
        void Add(OperationEntity item);
        void Update(OperationEntity item);
        void Delete(OperationEntity item);
        IEnumerable<OperationEntity> GetAll();
        OperationEntity GetLastItem(int shiftId);
    }
}
