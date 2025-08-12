using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage.ProductCodeUKTZEDPage
{
    internal class UpdateProductCodeUKTZEDModel
    {
        public UpdateProductCodeUKTZEDModel() { }

        public async Task<bool> UpdateItemDataBase(ProductCodeUKTZEDEntity unit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.UpdateCodeUKTZED(Session.Token, unit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
