using NPOI.SS.Formula.Functions;
using NPOI.Util;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
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
        private IEntityGet<ProductEntiti> _productRepositoryGet;
        private IEntityUpdate<ProductEntiti> _productRepositoryUpdate;
        private IEntityAccess<ProductEntiti> _productRepository;

        private IEntityAccess<ProductUnitEntiti> _productUnitRepository;
        private IEntityAccess<CodeUKTZEDEntiti> _codeUKTZEDRepository;

        private List<ProductUnitEntiti> _productUnitsList;
        private List<CodeUKTZEDEntiti> _codesUKTZEDList;


        public FormationProductModel()
        {
            _productUnitsList = new List<ProductUnitEntiti>();
            _codesUKTZEDList = new List<CodeUKTZEDEntiti>();

            _productRepository = new ProductTableAccess();
            _productRepositoryGet = new ProductTableAccess();
            _productRepositoryUpdate = new ProductTableAccess();

            _productUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }

        public ProductEntiti Search(string barCode)
        {
            try
            {
                return _productRepositoryGet.GetByBarCode(barCode);
                return new ProductEntiti();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void ContertToListProduct(IList list, List<ProductEntiti> products)
        {
            foreach (var item in list)
            {
                products.Add((ProductEntiti)item);
            }
        }

        public List<ProductEntiti>? UpdateList(List<ProductEntiti> productFormations, List<ProductEntiti> removeProduct)
        {
            try
            {
                List<ProductEntiti> products = new List<ProductEntiti>();
                products.AddRange(productFormations);
                if (removeProduct.Count == 1)
                {
                    products.Remove(removeProduct[0]);
                    return products;
                }
                else
                {
                    foreach (ProductEntiti product in removeProduct)
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
        public bool AddProduct(string name, string code, string articule, decimal price, int count, string units,string codeUKTZED, List<ProductEntiti> goodsFormation)
        {
            try
            {
                if (Validation.TextField(name, code, articule, price, count, units, (bool)AppSettingsManager.GetParameterFiles("IsValidFormationProduct")))
                {

                    if (Validation.CodeCoincidenceinDatabase(code, _productRepositoryGet.GetAll("in_stock")))
                    {
                        throw new Exception("Товар існує");
                    }
                    var unit = _productUnitsList.Where(item => item.ShortNameUnit== units).FirstOrDefault();
                    var UKTZED = _codesUKTZEDList.Where(item => item.NameCode == codeUKTZED).FirstOrDefault();

                    if(unit!=null)
                    {
                        if(UKTZED!=null)
                        {
                            ProductEntiti goods = new ProductEntiti();
                            
                            goods.Code = code;
                            goods.NameProduct = name;
                            goods.Articule = articule;
                            goods.Price = price;
                            goods.Count = count;
                            goods.Unit = unit;
                            goods.CodeUKTZED = UKTZED;
                            goods.CreatedAt = DateTime.Now;
                            goods.Status = "in_stock";
                            goods.Sales = 0;

                            _productRepository.Add(goods);
                        }
                    }

                    if (goodsFormation.Count != 0)
                    {
                        foreach(var item in goodsFormation)
                        {
                            var itemCount = item.Count * count;
                            if (itemCount != null)
                            {
                                _productRepositoryUpdate.UpdateParameter(item.ID, "count", -itemCount);
                            }
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<string>? GetUnitList()
        {
            try
            {
                List<string> result = new List<string>();
                _productUnitsList = (List<ProductUnitEntiti>)_productUnitRepository.GetAll();
                foreach (ProductUnitEntiti unit in _productUnitsList)
                {
                    result.Add(unit.ShortNameUnit.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public List<string>? GetCodeUKTZEDList()
        {
            try
            {
                List<string> result = new List<string>();
                _codesUKTZEDList = (List<CodeUKTZEDEntiti>)_codeUKTZEDRepository.GetAll();
                foreach (CodeUKTZEDEntiti code in _codesUKTZEDList)
                {
                    result.Add(code.NameCode.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
