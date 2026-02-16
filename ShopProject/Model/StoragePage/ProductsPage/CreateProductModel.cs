using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.UIModel.StoragePage;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage.ProductsPage
{
    internal class CreateProductModel
    { 

        public CreateProductModel()  {  } 

        public async Task<bool> SaveItemDataBase(Product product)
        {
            return await MainWebServerController.MainDataBaseConntroller.ProductController.AddProduct(Session.User.Token, product.ToCreateProductDto());
        }  
    }
}
