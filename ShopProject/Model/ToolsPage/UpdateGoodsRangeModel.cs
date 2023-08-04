
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateGoodsRangeModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<GoodsUnit> _goodsUnitRepository;
        private IEntityAccessor<CodeUKTZED> _codeUKTZEDRepository;

        private List<GoodsUnit> _goodsUnitsList;
        private List<CodeUKTZED> _codesUKTZEDList;

        public UpdateGoodsRangeModel()
        {
            _goodsUnitsList = new List<GoodsUnit>();
            _codesUKTZEDList = new List<CodeUKTZED>();

            _goodsRepository = new GoodsTableAccess();
            _goodsUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }

        public bool UpdateGoods(List<Goods> items)
        {
            try
            {
                //добавити валідацію на список
                _goodsRepository.UpdateRange(items);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
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
