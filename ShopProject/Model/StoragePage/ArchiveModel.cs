using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class ArchiveModel
    {
        //private IEntityAccessor<GoodsArchive,TypeParameterSetTableProductArhive> _archiveRepository;
        //private IEntityAccessor<Goods,TypeParameterSetTableProduct> _productRepository;

        public ArchiveModel()
        {
            //_productRepository = new ProductTableRepository();
            //_archiveRepository = new ArhiveTableRepositories();
        }

        public List<GoodsArchive> GetItems()
        {
            // (List<GoodsArchive>)_archiveRepository.GetAll();
            return null;
        }

        public List<GoodsArchive>? SearchArhive(string itemSearch, TypeSearch type)
        {
            try
            {
                return null;
                //return Search.ArhiveDataBase(itemSearch, type, (List<GoodsArchive>)_archiveRepository.GetAll());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public bool DeleteRecordArhive(GoodsArchive archive,Goods product)
        {
            try
            {
                //_archiveRepository.Delete(archive);
                //_productRepository.Delete(product);
                return true;
                throw new Exception("Помилка видалення");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        public void ConvertToList(IList collection, List<GoodsArchive> itemConver)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                itemConver.Add((GoodsArchive)collection[i]);
                //itemConver[i].goods = ((GoodsArchive)collection[i]).goods;
            }
        }
        
        public bool ReturnProductInStorage(GoodsArchive archive)
        {
            try
            {
                //_productRepository.SetParameter(archive.id, "in_stock", TypeParameterSetTableProduct.Status);
                //_archiveRepository.Delete(archive);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK);
                return false;
            }
        }
    }
}
