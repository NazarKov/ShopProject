using ShopProject.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class ArchiveModel
    {
        //private IEntityAccess<ProductEntiti> _goodsRepository;
        //private List<ProductEntiti> _arhiveGoods;

        //public ArchiveModel()
        //{
        //    _goodsRepository = new ProductTableAccess();
        //    _arhiveGoods = new List<ProductEntiti>();
        //    try
        //    {
        //        //_arhiveGoods = (List<ProductEntiti>)_goodsRepository.GetAll("arhived");
        //    }
        //    catch (Exception ex) 
        //    {
        //        MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
        //    }
        //}

        //public List<ProductEntiti> GetItems()
        //{
        //    return _arhiveGoods;
        //}

        //public List<ProductEntiti>? SearchArhive(string itemSearch)
        //{
        //    try
        //    {
        //        //return Search.GoodsDataBase(itemSearch, (List<ProductEntiti>) _goodsRepository.GetAll("arhived"));
        //        return new List<ProductEntiti>();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
        //        return null;
        //    }
        //}
        //public bool DeleteRecordArhive(ProductEntiti product)
        //{
        //    try
        //    {
        //        _goodsRepository.Delete(product);
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
        //        return false;
        //    }
        //}
        //public void ConvertToList(IList collection, List<ProductEntiti> itemConver)
        //{
        //    foreach(ProductEntiti item in collection)
        //    {
        //        itemConver.Add(item);
        //    }
        //}
        
        //public bool ReturnGoodsInStorage(ProductEntiti archive)
        //{
        //    try
        //    {
        //        //_goodsRepository.UpdateParameter(archive.id, "status", "in_stock");
        //        //_goodsRepository.UpdateParameter(archive.id, "arhived", null);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK);
        //        return false;
        //    }
        //}
    }
}
