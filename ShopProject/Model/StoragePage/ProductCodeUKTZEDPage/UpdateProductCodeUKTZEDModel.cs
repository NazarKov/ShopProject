using ShopProject.Helpers; 
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
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

        public async Task<bool> UpdateItemDataBase(ProductCodeUKTZED unit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.UpdateCodeUKTZED(Session.User.Token, unit.ToUpdateProductCodeUKTZED());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
