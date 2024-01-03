using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;

namespace ShopProject.Model.StoragePage
{

    internal class StorageModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private static List<Goods>? _goods;
        private Thread _threadSearch;
        private static Timer timer;
        private static string temp;
        public StorageModel()
        {
            _goods = new List<Goods>();
            _goodsRepository = new GoodsTableAccess();
            new Thread(new ThreadStart(ChekedGoodinCountNull)).Start();
        }

        public List<Goods> SearchGoods(string item)
        {
            try
            {
                if (_goods.Count != 0)
                {
                    _goods.Clear();
                }
                _goods = (List<Goods>)_goodsRepository.GetAll("in_stock");
                if (item != "")
                {
                    return Search.GoodsDataBase(item, _goods);
                }
                else
                {
                    return _goods;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return new List<Goods>();
            }
        }
        public int GetCount()
        {
            return _goodsRepository.GetAll("in_stock").Count();
        }

        public bool DeleteGoods(Goods productDelete)
        {
            try
            {
                _goodsRepository.Delete(productDelete);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public bool SetGoodsInArhive(Goods item)
        {
            try
            {
                _goodsRepository.UpdateParameter(item.id, "status", "arhived");
                _goodsRepository.UpdateParameter(item.id, "arhived", DateTime.Now);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        public bool SetGoodsOutOfStok(Goods item)
        {
            try
            {
                _goodsRepository.UpdateParameter(item.id, "status", "outStock");
                _goodsRepository.UpdateParameter(item.id, "outStock", DateTime.Now);
                _goodsRepository.UpdateParameter(item.id, "count", 0);

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        public void ContertToListGoods(IList list, List<Goods> goods)
        {
            foreach(Goods item in list)
            {
                goods.Add(item);
            }
        }

        private void ChekedGoodinCountNull()
        {
            Parallel.ForEach(_goodsRepository.GetAll("in_stock"), item =>
            {
                if (item.count<=0)
                {
                    SetGoodsOutOfStok(item);
                }
            });

        }
    }
}
