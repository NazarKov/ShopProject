using System.Collections.Generic;

namespace ShopProject.DataBase.Interfaces
{
    public interface IEntityAdd<T> : IEntityAccess<T>
    {
        // Specific methods for adding entities
        void AddRange(List<T> items);
    }
}
