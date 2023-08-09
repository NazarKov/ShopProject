using ShopProject.DataBase.Context;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
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

        public Goods? GetItem(string itemSearch)
        {

            try
            {
                return _goodsRepository.GetItemBarCode(itemSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erorr",MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
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
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        
        public bool Export(string path,List<Goods> goods)
        {
            try
            {
                fileExel = new FileExel();
                fileExel.Write(path,goods);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
    }
}
