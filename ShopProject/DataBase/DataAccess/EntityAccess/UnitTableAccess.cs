using ShopProject.DataBase.Context;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.DataBase.DataAccess.EntityAccess
{
    internal class UnitTableAccess : IEntityAccess<ProductUnitEntiti>
    {
        public void Add(ProductUnitEntiti item)
        {
            throw new NotImplementedException();
        }
        public void Delete(ProductUnitEntiti item)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ProductUnitEntiti> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.ProductUnits.Load();
                    if (context.ProductUnits != null)
                    {
                        if (context.ProductUnits.Any())
                        {
                            return context.ProductUnits.ToList();
                        }
                        else
                        {
                            throw new Exception("База даних пуста");
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }

                }
                else
                {
                    throw new Exception();
                }
            }
        }
        public void Update(ProductUnitEntiti item)
        {
            throw new NotImplementedException();
        }
    }
}
