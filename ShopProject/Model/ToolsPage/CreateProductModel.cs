using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateProductModel
    {

        private readonly ShopContext? db;
        private Product? product;

        public CreateProductModel()
        {
            db = new ShopContext();
            db.products.Load();
        }

        public bool SaveItemDataBase()
        {
            try
            {
                if(db!=null)
                if (this.product != null && db.products != null)
                {
                    db.products.Add(this.product);
                    db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        public bool CreateNewProduct(string name, string code, string articule, double price, double statingPrice, int count, string units)
        {
            try
            {

                product = new Product();
                if(db!= null)
                if (Validation.CodeCoincidenceinDatabase(code, db))
                {
                        throw new Exception("Товар існує");
                }
                Validation.TextField(product,name, code, articule, price, statingPrice, count, units, true);
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
