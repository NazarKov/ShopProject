using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class ImportProductExelModel 
    {
        private ShopContext? db;
        private Product? product;
        private FileExel? fileExel;
        private string path;

        public ImportProductExelModel()
        {
            db = new ShopContext();
            db.products.Load();
            path = string.Empty;
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
        public bool LoadFile()
        {
            try
            {
                fileExel = new FileExel(path);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
        public DataTable? GetTable(int i)
        {
            return fileExel.GetTabel(i);
        }
        public List<string> GetNableName()
        {
            return fileExel.GetTableName();
        }

        public bool SetItemDataBase(DataTable dataTable,params int[] column)
        {
            try
            {
                SaveItem(dataTable, column[0], column[1], column[2], column[3], column[4], column[5], column[6], column[7], column[8]);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void SaveItem(DataTable dataTable,int code ,int name,int articule,int price,int startingPrice,int count,int units,int indexTop,int intdexBottom)
        {
            int i = Validation.ChekNull(indexTop);
            int max = Validation.ChekNull(intdexBottom,dataTable.Rows.Count);

            for (; i < max; i++)
            {
                product = new Product();
                product.code = Validation.ChekParamsIsNull(code, i, dataTable);
                product.name = Validation.ChekParamsIsNull(name, i, dataTable);
                product.articule = Validation.ChekParamsIsNull(articule, i, dataTable);
                product.price = Validation.ChekEmpty(price, i, dataTable);
                product.startingPrise = Validation.ChekEmpty(startingPrice, i, dataTable);
                product.count = Convert.ToInt32(Validation.ChekEmpty(count, i, dataTable));
                product.units = Validation.ChekParamsIsNull(units, i, dataTable);
                product.created_at = DateTime.Now;
                if(db!=null)
                    if(db.products!=null)
                        db.products.Add(product);
            }
            if (db != null)
                db.SaveChanges();
        }
    }
}
