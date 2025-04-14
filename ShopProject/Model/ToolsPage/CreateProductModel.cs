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
        private List<ProductUnitEntity> _productsUnitsList;
        private List<CodeUKTZEDEntity> _codeUKTZEDEList;

        public CreateProductModel()
        {
            _productsUnitsList = new List<ProductUnitEntity>();
            _codeUKTZEDEList= new List<CodeUKTZEDEntity>();
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

        public List<CodeUKTZEDEntity> GetCodeUKTZED()
        {
            Task t = Task.Run(async () =>
            {
                _codeUKTZEDEList = (await MainWebServerController.MainDataBaseConntroller.CodeUKTZEDController.GetCodeUKTZED(Session.Token)).ToList();
            });
            t.Wait();

            return _codeUKTZEDEList;
        }

        public bool SaveItemDataBase(ProductEntity product)
        {
            try
            {
                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.ProductController.AddProduct(Session.Token, product);
                });
                t.Wait();

                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }  
    }
}
