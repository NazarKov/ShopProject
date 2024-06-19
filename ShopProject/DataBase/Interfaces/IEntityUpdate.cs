using System;
using System.Collections.Generic;

namespace ShopProject.DataBase.Interfaces
{
    public interface IEntityUpdate<T> : IEntityAccess<T>
    {
        // Specific methods for updating entities
        void UpdateRange(List<T> items);
        void UpdateParameter(Guid id, string nameParameter, object valueParameter);
    }
}
