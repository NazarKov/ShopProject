using NPOI.SS.Formula.Functions;
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
    internal class ExportGoodsExelModel
    {
        private IEntityAccessor<Goods> _goodsRepository;
        private FileExel? fileExel;

        public ExportGoodsExelModel()
        {
            _goodsRepository = new GoodsTableAccess();
        }

        public HelperClassExportGoodsInFile GetItem(string itemSearch)
        {

            try
            {
                return  new HelperClassExportGoodsInFile() 
                {
                    goods = _goodsRepository.GetItemBarCode(itemSearch),
                    goodsCount = 1
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new HelperClassExportGoodsInFile();
            }
        }

        public List<Goods> GetItems()
        {
            try
            {
                return (List<Goods>)_goodsRepository.GetAll("in_stock");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        public void Save(List<HelperClassExportGoodsInFile> products ,string path)
        {
            if (Export(path, products))
            {
                MessageBox.Show("Товар успішно експортовано","informations",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Товар не експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void Save(string path)
        {

            List<HelperClassExportGoodsInFile> goods = new List<HelperClassExportGoodsInFile>();

            var list = GetItems();
            if (list != null)
            {
                foreach (var item in list)
                {
                    goods.Add(new HelperClassExportGoodsInFile() { goods = item,goodsCount = item.count });
                }
            }

            if (Export(path, goods))
            {
                MessageBox.Show("товар успішно експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("товар не експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool Export(string path,List<HelperClassExportGoodsInFile> goods)
        {
            try
            {
                fileExel = new FileExel();
                fileExel.Write(path,goods);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
    }
}
