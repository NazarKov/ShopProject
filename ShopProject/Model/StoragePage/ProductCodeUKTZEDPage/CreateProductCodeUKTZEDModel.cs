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
    internal class CreateProductCodeUKTZEDModel
    {
        public async Task<bool> SaveItemDataBase(ProductCodeUKTZED codeUKTZED)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.AddProductCodeUKTZED(Session.User.Token, codeUKTZED.ToProductCodeUKTZED());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
