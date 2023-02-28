using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace ShopProject.Model.StoragePage
{

    internal class ArchiveModel
    {
        private ITableRepository<ProductArchive,TypeParameterSetTableProductArhive> _archiveRepository;
        private ITableRepository<Product,TypeParameterSetTableProduct> _productRepository;

        public ArchiveModel()
        {
            _productRepository = new ProductTableRepository();
            _archiveRepository = new ArhiveTableRepositories();
        }

        public List<ProductArchive> GetItems()
        {
            return (List<ProductArchive>)_archiveRepository.GetAll();
        }

        public List<ProductArchive>? SearchArhive(string itemSearch, TypeSearch type)
        {
            try
            {
                return Search.ArhiveDataBase(itemSearch, type, (List<ProductArchive>)_archiveRepository.GetAll());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return null;
            }
        }
        public bool DeleteRecordArhive(ProductArchive archive,Product product)
        {
            try
            {
                _archiveRepository.Delete(archive);
                _productRepository.Delete(product);
                return true;
                throw new Exception("Помилка видалення");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                return false;
            }
        }
        public void ConvertToList(IList collection, List<ProductArchive> itemConver)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                itemConver.Add((ProductArchive)collection[i]);
                itemConver[i].Product = ((ProductArchive)collection[i]).Product;
            }
        }
        
        public bool ReturnProductInStorage(ProductArchive archive)
        {
            try
            {
                _productRepository.SetParameter(archive.ID, "in_stock", TypeParameterSetTableProduct.Status);
                _archiveRepository.Delete(archive);
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
