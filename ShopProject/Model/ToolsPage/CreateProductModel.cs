using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ShopProject.Model.ToolsPage
{
    internal class CreateProductModel
    {
        ShopContext db;

        public CreateProductModel()
        {
            db = new ShopContext();
            new Thread(new ThreadStart(loadTableDb)).Start();
        }
        void loadTableDb()
        {
            db.products.Load();
        }

        public bool addProduct(string name,string code,string description, double price, double purchase_price,int count,string units)
        {
            try
            {
                Product product = new Product();
                product.name = name;
                product.code = code;
                product.description = description;
                product.price = price;
                product.purchase_prise = purchase_price;
                product.count = count;
                product.units = units;
                product.sales = 0;
                product.created_at = new DateTimeOffset().LocalDateTime;
                product.mark_up = (price / purchase_price) * 100;
                db.products.Add(product);
                db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
