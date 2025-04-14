using ShopProject.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectSQLDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class StorageModel
    {
        private static List<ProductEntity>? _products;
        

        public StorageModel()
        {
            _products = new List<ProductEntity>();
        }

        public List<ProductEntity> GetProducts()
        {
            Task t = Task.Run(async () =>
            {
                _products = (await MainWebServerController.MainDataBaseConntroller.ProductController.GetProducts(Session.Token)).ToList();
            });
            t.Wait();

            return _products;
        }

        public List<ProductEntity> SearchItems(string item)
        {
            try
            {
                if (item != "")
                {
                    return Search(item, _products);
                }
                return _products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return new List<ProductEntity>();
            }
        }

        private List<ProductEntity> Search(string item, List<ProductEntity> _productsList)
        {
            var result = new List<ProductEntity>();

            var count = _productsList.Count;
            for (int i = 0; i < count; i++)
            {
                if (_productsList[i].Code.Contains(item))
                {
                    result.Add(_productsList[i]);
                }
                else if (_productsList[i].NameProduct.ToLower().Contains(item.ToLower()))
                {
                    result.Add(_productsList[i]);
                }
                else if (_productsList[i].Articule.ToLower().ToLower().Contains(item.ToLower()))
                {
                    result.Add(_productsList[i]);
                }
            }
            return result;
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
