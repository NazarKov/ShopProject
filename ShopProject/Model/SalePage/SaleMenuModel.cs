using ShopProject.DataBase.Model;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Model.SalePage
{
    internal class SaleMenuModel
    {
        private List<Product> _products;
        private ITableRepository<Product,TypeParameterSetTableProduct> _tableProducts;
        private ITableRepository<Order,TypeParameterSetTableOrder> _tableOrders;
        private ITableRepository<ProductOrder,TypeParameterSetTableProductOrder> _tableProductOrders;

        private OrderCheck _orderCheck;

        public SaleMenuModel()
        {
            _products = new List<Product>();
            _tableProducts = new ProductTableRepository();
            _tableOrders = new OrderTableRepositories();
            _tableProductOrders = new ProductOrderTableRepositories();

            _orderCheck = new OrderCheck();
        }
        public bool SetOrderDataBase(List<Product> productList,Order order)
        {
            try
            {
                if(order!= null)
                {
                    _tableOrders.Add(order);
                    if(productList != null)
                    {
                        foreach (Product product in productList)
                        {
                            _tableProductOrders.Add(new ProductOrder()
                            {
                                Order = order,
                                Product = product,
                                count = (int)product.count,
                            });
                        }
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Product Search(string barCode)
        {
            return (Product)_tableProducts.GetItem(barCode);
        }

        public void PrintChek(List<Product> products)
        {
            _orderCheck.PrintChek(products);
        }
    }
}
