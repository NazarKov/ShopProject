using ShopProject.Model.Domain.Product;
using ShopProject.Model.Navigation;
using ShopProject.Services.Infrastructure.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.Product
{
    public class ProductForSaleModel
    {
        public Guid Cannnel { get; set; }
        public Domain.Product.Product Product { get; set; }
        public decimal Count
        {
            get { return Product.Count; }
            set
            {
                if (value <= 0)
                {
                    MediatorService.ExecuteEvent(NavigationButton.RemoveProduct.ToString() + "" + Cannnel, this);
                }
                else
                {
                    Product.Count = value; MediatorService.ExecuteEvent(NavigationButton.CountingSumaOrder.ToString() + "" + Cannnel);
                }
            }
        }

        public ProductForSaleModel(Domain.Product.Product product)
        {
            Product = product;
        }
    }
}
