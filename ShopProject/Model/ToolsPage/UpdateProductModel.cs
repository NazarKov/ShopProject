using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductModel
    {
        private ShopContext db;
        private List<Product> products;
        private Product? product;

        public UpdateProductModel()
        {
            db = new ShopContext();
            products = new List<Product>();
        }
        private void LoadTableDataBase()
        {
            db.products.Load();
            if(db!=null)
                if(db.products!=null)
            products = db.products.Local.ToList();
        }

        public bool SaveProduct()
        {
            LoadTableDataBase();
            try
            {

                SearchAndSaveProduct();
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }

        private void SearchAndSaveProduct()
        {
            if(product!=null)
            for (int i = 0, index = products.Count; i < index; i++)
            {
                if (products[i].code == product.code)
                {
                    products.ElementAt(i).name = product.name;
                    products.ElementAt(i).description = product.description;
                    products.ElementAt(i).price = product.price;
                    products.ElementAt(i).purchase_prise = product.purchase_prise;
                    products.ElementAt(i).count = product.count;
                    products.ElementAt(i).units = product.units;
                    products.ElementAt(i).created_at = new DateTimeOffset().LocalDateTime;
                    break;
                }
            }
        }

        public bool UpdateProduct(string name, string code,string articule, string description, double price, double purchase_price, int count, string units)
        {
            try
            {

                product = new Product();
                Validation.TextField(product,name, code, articule, description, price, purchase_price, count, units, true);
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
