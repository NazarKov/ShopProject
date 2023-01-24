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

        public ImportProductExelModel()
        {
            db = new ShopContext();
            db.products.Load();
        }

        public void SetItemDataBase(DataTable dataTable,params int[] column)
        {
            try
            {
                for (int i = 0, max = dataTable.Rows.Count; i < max; i++)
                {
                    product = new Product();

                    product.code = ChekParamsIsNull(column[0], i, dataTable);
                    product.name = ChekParamsIsNull(column[1], i, dataTable);
                    product.articule = ChekParamsIsNull(column[2], i, dataTable);
                     if (ChekParamsIsNull(column[4], i, dataTable) != string.Empty)
                    {
                        product.price = Convert.ToDouble(ChekParamsIsNull(column[5], i, dataTable));
                    }
                    else
                    {
                        product.price = 0;
                    }
                    if (ChekParamsIsNull(column[6], i, dataTable) != string.Empty)
                    {
                        product.startingPrise = Convert.ToDouble(ChekParamsIsNull(column[6], i, dataTable));
                    }
                    else
                    {
                        product.startingPrise = 0;
                    }
                    if (ChekParamsIsNull(column[6], i, dataTable) != string.Empty)
                    {
                        product.count = Convert.ToInt32(ChekParamsIsNull(column[6], i, dataTable));
                    }
                    else
                    {
                        product.count = 0;
                    }
                     
                    product.units = ChekParamsIsNull(column[7], i, dataTable);
                    product.created_at = DateTime.Now;
                    db.products.Add(product);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string ChekParamsIsNull(int column, int rows, DataTable table)
        {
            if (column != 0)
            {
                return table.Rows[rows].ItemArray.ElementAt(column - 1).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
