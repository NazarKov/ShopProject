using ShopProject.Helpers.Navigation;
using ShopProject.UIModel.StoragePage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.SalePage
{
    public class ProductForSale  
    {
        public Guid Cannnel { get;set; }
        public Product Product {  get; set; }
        public decimal Count { 
            get { return Product.Count; }
            set { 
                if(value <= 0)
                {
                    MediatorService.ExecuteEvent(NavigationButton.RemoveProduct.ToString() + "" + Cannnel,this);
                }
                else
                {
                    Product.Count = value; MediatorService.ExecuteEvent(NavigationButton.CountingSumaOrder.ToString()+""+Cannnel);  }
                }
        }

        public ProductForSale(Product product) 
        {
            Product = product;
        } 
    }
}
