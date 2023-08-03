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
    internal class UnitTableAccess : IEntityAccessor<GoodsUnit>
    {
        public void Add(GoodsUnit item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<GoodsUnit> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(GoodsUnit item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<GoodsUnit> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsUnit> GetAll()
        {
            using (ContextDataBase context = new ContextDataBase())
            {
                if (context != null)
                {
                    context.goodsUnits.Load();
                    if (context.goodsUnits != null)
                    {
                        if (context.goodsUnits.Any())
                        {
                            return context.goodsUnits.ToList();
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

        public GoodsUnit GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public GoodsUnit GetItemId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(GoodsUnit item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(GoodsUnit item, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<GoodsUnit> items)
        {
            throw new NotImplementedException();
        }
    }
}
