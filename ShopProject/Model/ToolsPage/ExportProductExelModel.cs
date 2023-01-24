using ShopProject.DataBase.Context;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.ToolsPage
{
    internal class ExportProductExelModel
    {
        ShopContext db;
        List<Product> products;

        public ExportProductExelModel()
        {
            db = new ShopContext();
            products = new List<Product>();
            
            db.products.Load();
            if(db.products!=null)
            products = db.products.Local.ToList();
        }

        public Product? GetItem(string Code)
        {
            foreach(Product product in products)
            {
                if(product.code==Code)
                {
                    return product;
                }
            }
            return null;
        }

        public List<Product> GetItems()
        {
            return products;
        }
        
    }
}
