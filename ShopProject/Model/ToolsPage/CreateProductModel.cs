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
 
        readonly ShopContext? db;
        Product? product;

        public CreateProductModel()
        {
            db = new ShopContext();
            new Thread(new ThreadStart(LoadProductDataBase)).Start();
        }
        private void LoadProductDataBase()
        {
            if(db != null)
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

        public bool CreateNewProduct(string name, string code, string articule, string description, double price, double purchase_price, int count, string units)
        {
            try
            {

                product = new Product();
                FieldValidation(name, code, articule, description, price, purchase_price, count, units, true);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK);
                return false;
            }
        }
        private void FieldValidation(string name, string code, string articule, string description, double price, double purchase_price, int count, string units,bool validation)
        {
            if (validation)
            {
                if (product != null)
                {
                    product.name = ItemChekIsNull(name,typeof(string), "Назва").ToString();
                    product.code = ItemChekIsNull(code, typeof(string), "Штрихкод").ToString();
                    product.articule = ItemChekIsNull(articule, typeof(string), "Артикуль").ToString();
                    product.description = ItemChekIsNull(description, typeof(string), "Опис").ToString();
                    product.price = Convert.ToDouble(ItemChekIsNull(price, typeof(double), "Ціна"));
                    product.purchase_prise = Convert.ToDouble(ItemChekIsNull(purchase_price, typeof(double), "Початкова ціна"));
                    product.count = Convert.ToInt32(ItemChekIsNull(count, typeof(int), "Кількість")); ;
                    product.units = ItemChekIsNull(units, typeof(string), "Одиниці").ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                    product.mark_up = (price / purchase_price) * 100;
                }
            }
            else
            {
                if (product != null)
                {
                    product.name = name.ToString();
                    product.code = code.ToString();
                    product.articule = articule.ToString();
                    product.description = description.ToString();
                    product.price = price;
                    product.purchase_prise = purchase_price;
                    product.count = count;
                    product.units = units.ToString();
                    product.sales = 0;
                    product.created_at = new DateTimeOffset().LocalDateTime;
                    product.mark_up = (price / purchase_price) * 100;
                }
            }
        }

        private object ItemChekIsNull(object? item, Type type, string messeges)
        {
            if (item != null)
            {
                if (type.Equals(typeof(string))&&(string)item != string.Empty)
                {
                    return item;
                }
                else if (type.Equals(typeof(int))&&(int)item != 0)
                {
                    return item;
                }
                else if (type.Equals(typeof(double))&&(double)item != 0)
                {
                    return item;
                }
                else
                {
                    throw new Exception("Заповніть поле " + messeges);
                }
            }
            else
            {
                throw new Exception("Заповніть поле " + messeges);
            }

        }
    }
}
