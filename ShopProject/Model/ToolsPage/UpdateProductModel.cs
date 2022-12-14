using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductModel
    {
        ShopContext db;
        List<Product> products;

        public UpdateProductModel()
        {
            db = new ShopContext();
            products = new List<Product>();
        }
        void loadTableDb()
        {
            db.products.Load();
            products = db.products.Local.ToList();
        }

        public bool updateproduct(string name, string code, string description, double price, double purchase_price, int count, string units)
        {
            loadTableDb();
            try
            {
               
                for(int i = 0,index=products.Count; i <index;i++)
                {
                    if(products[i].code == code)
                    {
                        products.ElementAt(i).name = name;
                        products.ElementAt(i).description = description;
                        products.ElementAt(i).price = price;
                        products.ElementAt(i).purchase_prise = purchase_price;
                        products.ElementAt(i).count = count;
                        products.ElementAt(i).units = units;
                        products.ElementAt(i).created_at = new DateTimeOffset().LocalDateTime;
                           break;
                    }
                }
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
