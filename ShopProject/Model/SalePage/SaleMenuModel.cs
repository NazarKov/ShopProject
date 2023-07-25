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
        private List<Goods> _products;
        private ITableRepository<Goods,TypeParameterSetTableProduct> _tableProducts;
        private ITableRepository<Operation,TypeParameterSetTableOrder> _tableOrders;
        private ITableRepository<GoodsOperation,TypeParameterSetTableProductOrder> _tableProductOrders;

        private OrderCheck _orderCheck;
        public int localnumber;

        public SaleMenuModel()
        {
            _products = new List<Goods>();
            _tableProducts = new ProductTableRepository();
            _tableOrders = new OrderTableRepositories();
            _tableProductOrders = new ProductOrderTableRepositories();
            _DFSAPI = new DFSAPI();
            _orderCheck = new OrderCheck();
            localnumber = 0;
        }
        public bool SetOrderDataBase(List<Goods> productList,Operation order)
        {
            localnumber = (Convert.ToInt32(((List<Operation>)_tableOrders.GetAll()).Last().LocalNumber) + 1);
            order.LocalNumber = localnumber.ToString();
            try
            {
                if(order!= null)
                {
                    _tableOrders.Add(order);
                    if(productList != null)
                    {
                        foreach (Goods product in productList)
                        {
                            _tableProductOrders.Add(new GoodsOperation()
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

        public Goods Search(string barCode)
        {
            return (Goods)_tableProducts.GetItem(barCode);
        }

        public void PrintChek(List<Goods> products,Operation order,Messe mac,DateTime dateTime)
        {
            _orderCheck.PrintChek(products,"12346578910",order,mac,dateTime);
        }
        public void closeChange()
        {
            List<Operation> orders = (List<Operation>)_tableOrders.GetAll();

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
            MessageBox.Show("Зміна закрита");
        }
        public void OpenChange()
        {
            _DFSAPI.OpenShift();
        }
        public Messe SendChek(List<Goods> products,Operation order,DateTime dateTime)
        {
            order.LocalNumber = localnumber.ToString();
            long date = long.Parse(dateTime.ToString("yyyyMMddHHmmss"));
            return _DFSAPI.SendChek(products, order,date);
        }
    }
}
