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

namespace ShopProject.Model.StoragePage.ProductUnitPage
{
    internal class CreateProductUnitModel
    { 
        public async Task<bool> SaveItemDataBase(ProductUnit unit)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.AddProductUnit(Session.User.Token, unit.ToProductUnit());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
