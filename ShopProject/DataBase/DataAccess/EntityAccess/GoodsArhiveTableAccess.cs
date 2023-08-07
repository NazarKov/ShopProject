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
    internal class GoodsArhiveTableAccess : IEntityAccessor<GoodsArchive>
    {
        public void Add(GoodsArchive item)
        {
            using (ContextDataBase conext = new ContextDataBase())
            {
                if(conext != null)
                {
                    conext.goods.ToList();
                    conext.goodsArchives.ToList();
                    if (item != null)
                    {
                        if (conext.goodsArchives != null)
                        {
                            conext.goodsArchives.Add(item);
                        }
                    }
                }
                conext.SaveChanges();
            }
        }

        public void AddRange(List<GoodsArchive> items)
        {
            throw new NotImplementedException();
        }

        public void Delete(GoodsArchive item)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(List<GoodsArchive> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GoodsArchive> GetAll()
        {
            throw new NotImplementedException();
        }

        public GoodsArchive GetItemBarCode(string barCode)
        {
            throw new NotImplementedException();
        }

        public GoodsArchive GetItemId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(GoodsArchive item)
        {
            throw new NotImplementedException();
        }

        public void UpdateParameter(Guid id, string nameParameter, object valueParameter)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<GoodsArchive> items)
        {
            throw new NotImplementedException();
        }
    }
}
