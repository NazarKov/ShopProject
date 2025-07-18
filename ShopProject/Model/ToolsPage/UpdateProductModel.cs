using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductModel
    {
        private List<ProductUnitEntity> _productsUnitsList;
        private List<ProductCodeUKTZEDEntity> _codeUKTZEDEList;

        public UpdateProductModel() {   }

        public bool UpdateItemDataBase(ProductEntity product)
        {
            try
            { 
                product.Status = ShopProjectSQLDataBase.Helper.TypeStatusProduct.InStock;

                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateProduct(Session.Token, product);
                });
                t.Wait();


                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public List<ProductUnitEntity> GetUnits()
        {
            Task t = Task.Run(async () =>
            {
                _productsUnitsList = (await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnits(Session.Token)).ToList();
            });
            t.Wait();
            return _productsUnitsList;
        }

        public List<ProductCodeUKTZEDEntity> GetCodeUKTZED()
        {
            Task t = Task.Run(async () =>
            {
                _codeUKTZEDEList = (await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZED(Session.Token)).ToList();
            });
            t.Wait();

            return _codeUKTZEDEList;
        }


    }
}
