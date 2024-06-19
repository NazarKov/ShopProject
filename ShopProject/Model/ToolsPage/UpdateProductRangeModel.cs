
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
    internal class UpdateProductRangeModel
    {
        private IEntityUpdate<ProductEntiti> _productRepositoryUpdate;
        private IEntityGet<ProductEntiti> _productRepositoryGet;
        private IEntityAccess<ProductUnitEntiti> _productUnitRepository;
        private IEntityAccess<CodeUKTZEDEntiti> _codeUKTZEDRepository;

        private List<ProductUnitEntiti> _productUnitsList;
        private List<CodeUKTZEDEntiti> _codesUKTZEDList;

        public UpdateProductRangeModel()
        {
            _productUnitsList = new List<ProductUnitEntiti>();
            _codesUKTZEDList = new List<CodeUKTZEDEntiti>();

            _productRepositoryUpdate = new ProductTableAccess();
            _productRepositoryGet = new ProductTableAccess();
            _productUnitRepository = new UnitTableAccess();
            _codeUKTZEDRepository = new CodeUKTZEDTableAccess();
        }
        public List<ProductEntiti> GetItem(List<ProductEntiti> items)
        {
            try
            {
                var result = new List<ProductEntiti>();
                foreach (var item in items)
                {
                    result.Add(_productRepositoryGet.GetById(item.ID));
                }
                return result;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error" , MessageBoxButton.OK,MessageBoxImage.Error);
                return new List<ProductEntiti>();
            }
        }

        public bool UpdateProduct(List<ProductEntiti> items)
        {
            try
            {
                //добавити валідацію на список
                _productRepositoryUpdate.UpdateRange(items);
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
