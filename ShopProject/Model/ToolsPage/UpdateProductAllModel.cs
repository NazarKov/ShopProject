using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.ToolsPage
{
    internal class UpdateProductAllModel
    {
        private ShopContext db;

        public UpdateProductAllModel()
        {
            db = new ShopContext();
            db.products.Load();
        }

        public bool UpdateProduct(List<Product> list)
        {
            try
            {
                if(db.products!=null)
                foreach (Product product in db.products)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (product.code == list[i].code)
                        {
                            product.code = list[i].code;
                            product.price = list[i].price;
                            product.articule = list[i].articule;
                            product.units = list[i].units;
                            product.count = list[i].count;
                            product.startingPrise = list[i].startingPrise;
                            product.sales = list[i].sales;
                            product.markUp = list[i].markUp;
                            product.name = list[i].name;
                        }
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
