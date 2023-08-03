using NPOI.SS.Formula.Functions;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.StoragePage
{
    internal class OutOfStockModel
    {
        //private IEntityAccessor<GoodsOutOfStock, TypeParameterSetTableOutOfStock> _outOfStockPeoductRepository;
        //private IEntityAccessor<Goods,TypeParameterSetTableProduct> _productRepository;
       
        public OutOfStockModel() 
        {
            ////_outOfStockPeoductRepository = new OutOfStockTableRepositories();
            ////_productRepository = new ProductTableRepository();
        }
        public List<GoodsOutOfStock> GetAll()
        {
            return null;
           // return (List<GoodsOutOfStock>)_outOfStockPeoductRepository.GetAll();
        }

        public List<GoodsOutOfStock>? SearchProducts(string itemSearch, TypeSearch type)
        {
            try
            {
                return null;
                //return Search.OutOfStockProductDataBase(itemSearch, type, (List<GoodsOutOfStock>)_outOfStockPeoductRepository.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public void ConvertToList(IList collection, List<GoodsOutOfStock> itemConver)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                itemConver.Add((GoodsOutOfStock)collection[i]);
                //itemConver[i].goods = ((GoodsOutOfStock)collection[i]).goods;
            }
        }

        public bool ReturnProductInStorage(GoodsOutOfStock productsOutOfStock)
        {
            try
            {
                //_productRepository.SetParameter(productsOutOfStock.ID, "in_stock", TypeParameterSetTableProduct.Status);
                //_outOfStockPeoductRepository.Delete(productsOutOfStock);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

        public bool DeleteRecordArhive(GoodsOutOfStock productOutOfStock, Goods product)
        {
            try { 
            //{
            //    _outOfStockPeoductRepository.Delete(productOutOfStock);
            //    _productRepository.Delete(product);
                return true;
                throw new Exception("Помилка видалення");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }

    }
}
