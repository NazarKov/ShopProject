using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ShopProjectDataBase.Entities;
using ShopProject.UIModel.StoragePage;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;

namespace ShopProject.Model.StoragePage.ProductsPage
{
    internal class UpdateProductRangeModel
    { 

        public UpdateProductRangeModel() {  }

        public async Task<bool> UpdateProduct(List<Product> items)
        {
            try
            { 
                return await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateProductRange(Session.User.Token, items.ToUpdateProductDto()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        } 

    }
}
