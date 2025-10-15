 
using ShopProject.Helpers;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing.QrCode.Internal;

namespace ShopProject.Model.ToolsPage
{
    internal class FormationProductModel
    {
 
        private List<ProductUnitEntity> _productUnitsList;
        private List<ProductCodeUKTZEDEntity> _codesUKTZEDList;


        public FormationProductModel()
        {
            _productUnitsList = new List<ProductUnitEntity>();
            _codesUKTZEDList = new List<ProductCodeUKTZEDEntity>();
 
        }

        public ProductEntity Search(string barCode)
        {
            try
            {
                ProductEntity item = new ProductEntity();
                Task task = Task.Run(async () =>
                {
                    //item = await MainWebServerController.MainDataBaseConntroller.ProductController.GetProductByBarCode(Session.Token, barCode);
 
                });
                task.Wait();
                return item;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void ContertToListProduct(IList list, List<ProductEntity> products)
        {
            foreach (var item in list)
            {
                products.Add((ProductEntity)item);
            }
        }

        public List<ProductEntity>? UpdateList(List<ProductEntity> productFormations, List<ProductEntity> removeProduct)
        {
            try
            {
                List<ProductEntity> products = new List<ProductEntity>();
                products.AddRange(productFormations);
                if (removeProduct.Count == 1)
                {
                    products.Remove(removeProduct[0]);
                    return products;
                }
                else
                {
                    foreach (ProductEntity product in removeProduct)
                    {
                        products.Remove(product);
                    }
                    return products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public bool AddProduct(ProductEntity product, List<ProductEntity> ProductListFormation)
        {
            try
            {

                product.CreatedAt = DateTime.Now;
                product.Status =  TypeStatusProduct.InStock;


                bool response = false;
                Task t = Task.Run(async () =>
                {
                    //response = await MainWebServerController.MainDataBaseConntroller.ProductController.AddProduct(Session.Token, product);
                });
                t.Wait();

                if (ProductListFormation.Count != 0)
                {
                    foreach (var item in ProductListFormation)
                    {
                        var itemCount = item.Count * product.Count;
                        if (itemCount != null)
                        {
                            t = Task.Run(async () =>
                            {
                               // await MainWebServerController.MainDataBaseConntroller.ProductController.UpdateParameterProduct(Session.Token, nameof(ProductEntity.Count),-itemCount ,item);
                            });

                        }
                    }
                }


                return response;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public List<ProductUnitEntity> GetUnits()
        {
            Task t = Task.Run(async () =>
            {
                //_productUnitsList = (await MainWebServerController.MainDataBaseConntroller.ProductUnitController.GetUnits(Session.Token)).ToList();
            });
            t.Wait();
            return _productUnitsList;
        }

        public List<ProductCodeUKTZEDEntity> GetCodeUKTZED()
        {
            Task t = Task.Run(async () =>
            {
               // _codesUKTZEDList = (await MainWebServerController.MainDataBaseConntroller.ProductCodeUKTZEDController.GetCodeUKTZED(Session.Token)).ToList();
            });
            t.Wait();

            return _codesUKTZEDList;
        }
    }
}
