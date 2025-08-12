using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ShopProjectSQLDataBase.Entities;

namespace ShopProject.Model.StoragePage.ProductUnitPage
{
    internal class UpdateProductUnitModel
    {
        public UpdateProductUnitModel() { }

        public async Task<bool> UpdateItemDataBase(ProductUnitEntity unit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.UpdateUnit(Session.Token, unit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
