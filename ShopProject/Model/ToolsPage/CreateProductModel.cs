using ShopProject.DataBase.Context;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateProductModel
    {
        private IEntityAccess<ProductEntiti> _productsRepository;
        private IEntityGet<ProductEntiti> _productsGet;
        private IEntityAccess<ProductUnitEntiti> _productsUnitRepository;
        private IEntityAccess<CodeUKTZEDEntiti> _codeUKTZEDRepository;

        private List<ProductUnitEntiti> _productsUnitsList;
        private List<CodeUKTZEDEntiti> _codesUKTZEDList;

        public CreateProductModel()
        {
            _productsUnitsList = new List<ProductUnitEntiti>();
            _codesUKTZEDList = new List<CodeUKTZEDEntiti>();

            _productsGet = new ProductTableAccess();
            _productsRepository = new ProductTableAccess();
            _productsUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }

        public bool SaveItemDataBase(string name, string code, string articled, decimal price, decimal count, string units,string codeUKTZED)
        {
            try
            {
                if (Validation.TextField(name, code, articled, price, count,units, (bool)AppSettingsManager.GetParameterFiles("IsValidCreateProduct")))
                {
                    if (_productsGet.GetByBarCode(code) != null)//перевірка на наявність товару по штрих коду
                    {
                        throw new Exception("Товар існує");
                    }

                    var unit = _productsUnitsList.Where(item => item.ShortNameUnit == units).FirstOrDefault();
                    var UKTZED = _codesUKTZEDList.Where(item => item.NameCode==codeUKTZED).FirstOrDefault();
                    if (unit != null)
                    {
                        if (UKTZED != null)
                        {
                            _productsRepository.Add(new ProductEntiti()
                            {
                                NameProduct = name,
                                Code = code,
                                Articule = articled,
                                Price = price,
                                Count = count,
                                Unit = unit,
                                CodeUKTZED = UKTZED,
                                CreatedAt = DateTime.Now,
                                Status = "in_stock",
                                Sales = 0
                            });
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
        public List<string>? GetUnitList()
        {
            try
            {
                List<string> result = new List<string>();
                _productsUnitsList = (List<ProductUnitEntiti>)_productsUnitRepository.GetAll();
                foreach (ProductUnitEntiti unit in _productsUnitsList)
                {
                    result.Add(unit.ShortNameUnit.ToString());
                }
                return result;
            }
            catch(Exception ex)
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
                foreach(CodeUKTZEDEntiti code in  _codesUKTZEDList)
                {
                    result.Add(code.NameCode.ToString());
                }
                return result;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Помилка",MessageBoxButton.OK,MessageBoxImage.Error);
                return null;
            }
        }
       
    }
}
