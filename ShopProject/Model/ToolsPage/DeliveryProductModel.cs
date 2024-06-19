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
    internal class DeliveryProductModel
    {
        private IEntityUpdate<ProductEntiti> _goodsRepositoryUpdate;
        private IEntityGet<ProductEntiti> _productRepositoryGet;
        public DeliveryProductModel()
        {
            _goodsRepositoryUpdate = new ProductTableAccess();
            _productRepositoryGet = new ProductTableAccess();
        }

        public bool SetCount(string barCode , decimal count)
        {
            try
            {
                var item = _productRepositoryGet.GetByBarCode(barCode);
                if (item != null)
                {
                    _goodsRepositoryUpdate.UpdateParameter(item.ID,nameof(item.Count) , count);
                    return true;
                }
                throw new Exception("Товар не знайдено");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

    }
}
