using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Helper.ProductContoller;
using ShopProject.Helpers.Template.Paginator;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class StorageModel
    { 
        public StorageModel() {  }

        public async Task<PaginatorData<ProductEntity>> GetProductsPageColumn(int page , int countColumn, TypeStatusProduct statusProduct)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductsPageColumn(Session.Token, page, countColumn, statusProduct);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductEntity>();
            }
        }

        public async Task<PaginatorData<ProductEntity>> SearchByName(string item, int page, int countColumn, TypeStatusProduct statusProduct)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByNamePageColumn(Session.Token,item, page, countColumn, statusProduct);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<ProductEntity>();
            }
        }
        public async Task<ProductEntity> SearchByBarCode(string item, TypeStatusProduct statusProduct)
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(Session.Token, item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ProductEntity();
            } 
        }
        public async Task<ProductInfo> GetProductInfo()
        {
            try
            {
                return await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductInfo(Session.Token);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return  new ProductInfo();
            }
        }

        public bool SetItemInArhive(ProductEntity item)
        {
            try
            {
                Task t = Task.Run(async () =>
                {
                   await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(Session.Token, nameof(item.Status), TypeStatusProduct.Archived, item);
                });
                t.Wait();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public bool SetItemOutOfStock(ProductEntity item)
        {
            try
            {
                Task t = Task.Run(async () =>
                {
                    await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(Session.Token, nameof(item.Status), TypeStatusProduct.OutStock, item);
                });
                t.Wait();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public void ContertIListToList(IList list, List<ProductEntity> _products)
        {
            foreach (ProductEntity item in list)
            {
                _products.Add(item);
            }
        }
    }
}
