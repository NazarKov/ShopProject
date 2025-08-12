using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers;
using ShopProjectSQLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductRangeModel
    {

        private List<ProductUnitEntity> _productUnitsList;
        private List<ProductCodeUKTZEDEntity> _codesUKTZEDList;

        public UpdateProductRangeModel()
        {
            _productUnitsList = new List<ProductUnitEntity>();
            _codesUKTZEDList = new List<ProductCodeUKTZEDEntity>();
 
        }

        public bool UpdateProduct(List<ProductEntity> items)
        {
            try
            {

                bool response = false;
                Task t = Task.Run(async () =>
                {
                    response = await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateProductRange(Session.Token,items);
                });
                t.Wait();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public List<ProductUnitEntity> GetUnits()
        {
            Task t = Task.Run(async () =>
            {
                _productUnitsList = (await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnits(Session.Token)).ToList();
            });
            t.Wait();
            return _productUnitsList;
        }

        public List<ProductCodeUKTZEDEntity> GetCodeUKTZED()
        {
            Task t = Task.Run(async () =>
            {
                _codesUKTZEDList = (await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZED(Session.Token)).ToList();
            });
            t.Wait();

            return _codesUKTZEDList;
        }

    }
}
