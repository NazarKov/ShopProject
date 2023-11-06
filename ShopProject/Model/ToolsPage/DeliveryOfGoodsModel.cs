using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class DeliveryOfGoodsModel
    {
        private IEntityAccessor<Goods> _goodsRepository; 
        public DeliveryOfGoodsModel() 
        {
            _goodsRepository = new GoodsTableAccess();
        }

        public bool SetCount(string barCode , decimal count)
        {
            try
            {
                _goodsRepository.UpdateParameter(_goodsRepository.GetItemBarCode(barCode).id, "count", count);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

    }
}
