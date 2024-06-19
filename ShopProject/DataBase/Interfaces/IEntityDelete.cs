using System.Collections.Generic;

namespace ShopProject.DataBase.Interfaces
{
    public interface IEntityDelete<T> : IEntityAccess<T>
    {
        // Specific methods for deleting entities
        void DeleteRange(List<T> items);
    }
}
