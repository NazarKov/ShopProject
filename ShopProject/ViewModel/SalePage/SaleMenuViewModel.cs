using ShopProject.DataBase.Model;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.SalePage
{
    internal class SaleMenuViewModel : ViewModel<SaleMenuViewModel>
    {
        private SaleMenuModel _model;

        private ICommand _searchBarCodeProdcutCommand;
        private ICommand _printingCheckCommand;

        public SaleMenuViewModel() 
        {
            _model = new SaleMenuModel();

            _searchBarCodeProdcutCommand = new DelegateCommand(SearchBarCodePrdocut);
            _printingCheckCommand = new DelegateCommand(PrintingCheck);
            
            _products = new List<Product>();
            _barCodeSearch = string.Empty;
            _sumaOrder = 0;
            _sumaUser = 0;
        }

        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged("BarCodeSearch"); }
        }

        private List<Product> _products;
        public List<Product> Products 
        {
            get {  return _products; }
            set { _products = value; OnPropertyChanged("Products");}
        }

        private double? _sumaOrder;
        public double? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value;OnPropertyChanged("SumaOrder"); }
        }

        private double? _sumaUser;
        public double? SumaUser
        {
            get { return _sumaUser; }
            set { _sumaUser = value; OnPropertyChanged("_sumaUser"); }
        }

        public ICommand SearchBarCodeProductCommand => _searchBarCodeProdcutCommand;

        private void SearchBarCodePrdocut()
        {
            if (BarCodeSearch.Length > 8)
            {
                var item = _model.Search(BarCodeSearch);

                if (item != null)
                {
                    Product product = new Product();
                    product = item;
                    product.count = 1;


                    List<Product> temp = new List<Product>();
                    temp = Products;

                    if (temp.Find(pr => pr.code == item.code) != null)
                    {
                        temp.Find(pr => pr.code == item.code).count += 1;
                    }
                    else
                    {
                        temp.Add(product);
                    }

                    //foreach(Product orderProduct in temp)
                    //{
                    //    SumaOrder = (orderProduct.price*orderProduct.count)+SumaOrder;
                    //}

                    Products = new List<Product>();
                    Products = temp;
                    BarCodeSearch = string.Empty;
                }
            }
           
        }
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private void PrintingCheck()
        {
            if(_model.SetOrderDataBase(Products,new Order() { created_at = DateTime.Now, sale=0,suma=0,rest=0,user=null }))
            {
                //_model.PrintChek(Products);
                MessageBox.Show("чек видано");
                Products = new List<Product>();
                BarCodeSearch = string.Empty;
            }
        }


    }
}
