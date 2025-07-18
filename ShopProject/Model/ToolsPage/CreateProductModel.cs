using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateProductModel
    { 

        public CreateProductModel()  {  }

        public async Task<IEnumerable<ProductUnitEntity>> GetUnits()
        { 
            return await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnits(Session.Token);
        }

        public async Task<IEnumerable<ProductCodeUKTZEDEntity>> GetCodeUKTZED()
        {
            return await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZED(Session.Token);
        }

        public async Task<bool> SaveItemDataBase(ProductEntity product)
        {
            try
            {  
                return await MainWebServerController.MainDataBaseConntroller.ProductController.AddProduct(Session.Token, product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }  
    }
}
