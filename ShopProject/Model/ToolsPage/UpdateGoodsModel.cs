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
    internal class UpdateGoodsModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<GoodsUnit> _goodsUnitRepository;
        private IEntityAccessor<CodeUKTZED> _codeUKTZEDRepository;

        private List<GoodsUnit> _goodsUnitsList;
        private List<CodeUKTZED> _codesUKTZEDList;

        public UpdateGoodsModel()
        {
            _goodsUnitsList = new List<GoodsUnit>();
            _codesUKTZEDList = new List<CodeUKTZED>();

            _goodsRepository = new GoodsTableAccess();
            _goodsUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }
      
        public bool UpdateItemDataBase(Guid id,string name, string code,string articule, decimal price, decimal count, string units,string codeUKTZED)
        {
            try
            {
                if (Validation.TextField(name, code, articule, price, count, units, (bool)AppSettingsManager.GetParameterFiles("IsValidUpdateProduct")))
                {
                    var unit = _goodsUnitsList.Where(item => item.shortName == units).FirstOrDefault();
                    var UKTZED = _codesUKTZEDList.Where(item => item.name == codeUKTZED).FirstOrDefault();
                    if (unit != null)
                    {
                        if (UKTZED != null)
                        {
                            _goodsRepository.Update(new Goods()
                            {
                                id =id,
                                name = name,
                                code = code,
                                articule = articule,
                                price = price,
                                count = count,
                                unit = unit,
                                codeUKTZED = UKTZED,
                                sales = 0,
                                status = "in_stock",
                                
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
