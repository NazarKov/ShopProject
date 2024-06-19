using System;
using System.Collections.Generic;

namespace ShopProject.DataBase.Interfaces
{
    public interface IEntityAccess<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
    }
}
