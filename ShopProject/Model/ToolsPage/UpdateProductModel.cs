using ShopProject.DataBase.Context;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductModel
    {
        private IEntityAccess<ProductEntiti> _productsRepository;
        private IEntityAccess<ProductUnitEntiti> _productsUnitRepository;
        private IEntityAccess<CodeUKTZEDEntiti> _codeUKTZEDRepository;

        private List<ProductUnitEntiti> _productsUnitsList;
        private List<CodeUKTZEDEntiti> _codesUKTZEDList;

        public UpdateProductModel()
        {
            _productsUnitsList = new List<ProductUnitEntiti>();
            _codesUKTZEDList = new List<CodeUKTZEDEntiti>();

            _productsRepository = new ProductTableAccess();
            _productsUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }
      
        public bool UpdateItemDataBase(Guid id,string name, string code,string articule, decimal price, decimal count, string units,string codeUKTZED)
        {
            try
            {
                if (Validation.TextField(name, code, articule, price, count, units, (bool)AppSettingsManager.GetParameterFiles("IsValidUpdateProduct")))
                {
                    var unit = _productsUnitsList.Where(item => item.ShortNameUnit == units).FirstOrDefault();
                    var UKTZED = _codesUKTZEDList.Where(item => item.NameCode == codeUKTZED).FirstOrDefault();
                    if (unit != null)
                    {
                        if (UKTZED != null)
                        {
                            _productsRepository.Update(new ProductEntiti()
                            {
                                ID =id,
                                NameProduct = name,
                                Code = code,
                                Articule = articule,
                                Price = price,
                                Count = count,
                                Unit = unit,
                                CodeUKTZED = UKTZED,
                                Sales = 0,
                                Status = "in_stock",
                                
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
