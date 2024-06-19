using System;
using System.Collections.Generic;

namespace ShopProject.DataBase.Interfaces
{
    public interface IEntityGet<T> : IEntityAccess<T>
    {
        // Specific methods for getting entities
        T GetById(Guid id);
        T GetByBarCode(string barCode);
        IEnumerable<T> GetAll(string statusGoods);
    }
}
