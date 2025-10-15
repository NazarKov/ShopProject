using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage.ProductsPage
{
    internal class UpdateProductModel
    { 
        public UpdateProductModel() {   }

        public async Task<bool> UpdateItemDataBase(Product product)
        {
            try
            {  
                return await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateProduct(Session.User.Token, product.ToUpdateProductDto()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }  
    }
}
