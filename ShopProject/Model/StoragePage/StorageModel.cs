using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi; 
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi.Mapping;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.UIModel.StoragePage; 
using ShopProjectDataBase.Helper; 
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class StorageModel
    {
        private readonly string _token; 
        public StorageModel()
        {
            _token = Session.User.Token;
        }

        public async Task<PaginatorData<Product>> GetProductsPageColumn(int page , int countColumn, TypeStatusProduct statusProduct)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductsPageColumn(_token, page, countColumn, statusProduct);

                var paginator = new PaginatorData<Product>()
                {
                    Data = result.Data.ToProduct(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<Product>();
            }
        }

        public async Task<PaginatorData<Product>> SearchByName(string item, int page, int countColumn, TypeStatusProduct statusProduct)
        {
            try
            {
                var result = await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByNamePageColumn(_token, item, page, countColumn, statusProduct);

                var paginator = new PaginatorData<Product>()
                {
                    Data = result.Data.ToProduct(),
                    DataType = result.DataType,
                    Page = result.Page,
                    Pages = result.Pages,
                };
                return paginator;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new PaginatorData<Product>();
            }
        }
        public async Task<Product> SearchByBarCode(string item, TypeStatusProduct statusProduct)
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(_token, item , statusProduct)).ToProduct();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Product();
            } 
        }
        public async Task<ProductsInfo> GetProductInfo()
        {
            try
            {
                return (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductInfo(_token)).ToProductsInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return  new ProductsInfo();
            }
        }

        public async Task<bool> SetItemInArhive(Product item)
        {
            try
            { 
                return await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(_token, nameof(item.Status), TypeStatusProduct.Archived, item.ToUpdateProductDto());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public async Task<bool> SetItemOutOfStock(Product item)
        {
            try
            { 
                return await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(_token, nameof(item.Status), TypeStatusProduct.OutStock, item.ToUpdateProductDto());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public void ContertIListToList(IList list, List<Product> _products)
        {
            foreach (Product item in list)
            {
                _products.Add(item);
            }
        }
    }
}
