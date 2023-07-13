using ShopProject.DataBase.Model;
using ShopProject.Helpers.DFSAPI;
using ShopProject.Interfaces.InterfacesRepository;
using ShopProject.Model.Command;
using ShopProject.Model.ModelRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.Model.SalePage
{
    internal class SaleMenuModel
    {
        private DFSAPI _DFSAPI;
        private List<Product> _products;
        private ITableRepository<Product,TypeParameterSetTableProduct> _tableProducts;
        private ITableRepository<Order,TypeParameterSetTableOrder> _tableOrders;
        private ITableRepository<ProductOrder,TypeParameterSetTableProductOrder> _tableProductOrders;

        private OrderCheck _orderCheck;
        public int localnumber;

        public SaleMenuModel()
        {
            _products = new List<Product>();
            _tableProducts = new ProductTableRepository();
            _tableOrders = new OrderTableRepositories();
            _tableProductOrders = new ProductOrderTableRepositories();
            _DFSAPI = new DFSAPI();
            _orderCheck = new OrderCheck();
            localnumber = 0;
        }
        public bool SetOrderDataBase(List<Product> productList,Order order)
        {
            localnumber = (Convert.ToInt32(((List<Order>)_tableOrders.GetAll()).Last().LocalNumber) + 1);
            order.LocalNumber = localnumber.ToString();
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

        public void PrintChek(List<Product> products,Order order,Messe mac,DateTime dateTime)
        {
            _orderCheck.PrintChek(products,"12346578910",order,mac,dateTime);
        }
        public void closeChange()
        {
            List<Order> orders = (List<Order>)_tableOrders.GetAll();

            int count = 0;
            for(int i = 0; i < orders.Count; i++)
            {
                if (orders[i].created_at.Value.Date.ToString("yyyyMMdd")==DateTime.Now.ToString("yyyyMMdd"))
                {
                    count++;
                }
            }
            count = 5;

            _DFSAPI.CloseShift(count);
        }
        public void OpenChange()
        {
            _DFSAPI.OpenShift();
        }
        public Messe SendChek(List<Product> products,Order order,DateTime dateTime)
        {
            order.LocalNumber = localnumber.ToString();
            long date = long.Parse(dateTime.ToString("yyyyMMddHHmmss"));
            return _DFSAPI.SendChek(products, order,date);
        }
    }
}
