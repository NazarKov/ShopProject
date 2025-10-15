using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class DeliveryProductModel
    {
        public DeliveryProductModel(){}

        public bool SetCount(string barCode, decimal count)
        {
            try
            {
                bool result = false;
                //Task t =Task .Run(async () => {

                //    result = await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(
                //    Session.Token,
                //    nameof(ProductEntity.Count),
                //    count,
                //    new ProductEntity() { Code = barCode }
                //    );
                
                //});

                //t.Wait();
                return result;
                
                throw new Exception("Товар не знайдено");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

    }
}
