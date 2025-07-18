using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage.ProductUnitPage
{
    internal class CreateProductUnitModel
    { 
        public async Task<bool> SaveItemDataBase(ProductUnitEntity unit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.AddProductUnit(Session.Token, unit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
