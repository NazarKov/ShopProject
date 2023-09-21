using NPOI.SS.Formula.Functions;
using NPOI.Util;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
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
        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<GoodsUnit> _goodsUnitRepository;
        private IEntityAccessor<CodeUKTZED> _codeUKTZEDRepository;

        private List<GoodsUnit> _goodsUnitsList;
        private List<CodeUKTZED> _codesUKTZEDList;


        public FormationProductModel()
        {
            _goodsUnitsList = new List<GoodsUnit>();
            _codesUKTZEDList = new List<CodeUKTZED>();

            _goodsRepository = new GoodsTableAccess();
            _goodsUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }

        public Goods Search(string barCode)
        {
            try
            {
                return _goodsRepository.GetItemBarCode(barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void ContertToListProduct(IList list, List<Goods> products)
        {
            foreach (var item in list)
            {
                products.Add((Goods)item);
            }
        }

        public List<Goods>? UpdateList(List<Goods> productFormations, List<Goods> removeProduct)
        {
            try
            {
                List<Goods> products = new List<Goods>();
                products.AddRange(productFormations);
                if (removeProduct.Count == 1)
                {
                    products.Remove(removeProduct[0]);
                    return products;
                }
                else
                {
                    foreach (Goods product in removeProduct)
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
        public bool AddProduct(string name, string code, string articule, decimal price, int count, string units,string codeUKTZED, List<Goods> goodsFormation)
        {
            try
            {
                if (Validation.TextField(name, code, articule, price, count, units, (bool)AppSettingsManager.GetParameterFiles("IsValidFormationProduct")))
                {

                    if (Validation.CodeCoincidenceinDatabase(code,_goodsRepository.GetAll("in_stock")))
                    {
                        throw new Exception("Товар існує");
                    }
                    var unit = _goodsUnitsList.Where(item => item.shortName == units).FirstOrDefault();
                    var UKTZED = _codesUKTZEDList.Where(item => item.name == codeUKTZED).FirstOrDefault();

                    if(unit!=null)
                    {
                        if(UKTZED!=null)
                        {
                            Goods goods = new Goods();
                            
                            goods.code = code;
                            goods.name = name;
                            goods.articule = articule;
                            goods.price = price;
                            goods.count = count;
                            goods.unit = unit;
                            goods.codeUKTZED = UKTZED;
                            goods.createdAt = DateTime.Now;
                            goods.status = "in_stock";
                            goods.sales = 0;

                            _goodsRepository.Add(goods);
                        }
                    }

                    if (goodsFormation.Count != 0)
                    {
                        foreach(var item in goodsFormation)
                        {
                            var itemCount = item.count * count;
                            if (itemCount != null)
                            {
                                _goodsRepository.UpdateParameter(item.id, "count", -itemCount);
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
                _goodsUnitsList = (List<GoodsUnit>)_goodsUnitRepository.GetAll();
                foreach (GoodsUnit unit in _goodsUnitsList)
                {
                    result.Add(unit.shortName.ToString());
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
                _codesUKTZEDList = (List<CodeUKTZED>)_codeUKTZEDRepository.GetAll();
                foreach (CodeUKTZED code in _codesUKTZEDList)
                {
                    result.Add(code.name.ToString());
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
