﻿using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using ShopProjectDataBase.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.SalePage
{
    internal class SaleGoodsMenuViewModel : ViewModel<SaleGoodsMenuViewModel>
    {
        private SaleGoodsMenuModel _model;

        private ICommand _searchBarCodeGoodsCommand;
        private ICommand _printingCheckCommand;
        private ICommand _clearFieldDataGrid;

        private ICommand _updateSize;


        public SaleGoodsMenuViewModel() 
        {
            _model = new SaleGoodsMenuModel();

            _searchBarCodeGoodsCommand = new DelegateCommand(SearchBarCodeGoods);
            _printingCheckCommand = new DelegateCommand(PrintingCheck);
            _updateSize = new DelegateCommand(UpdateSizes);
            _clearFieldDataGrid = new DelegateCommand(ClearField);

            _goods = new List<ProductEntity>();
            _barCodeSearch = string.Empty;

            _typeOplatu = new List<string>();
            _typeOplatu.Add("готівкою");
            _typeOplatu.Add("безготівкові форми оплати");

            //DrawingCheck = _model.IsDrawinfChek;
            IsFiscalCheck = true;

            ClearField();
        }

        private string _barCodeSearch;
        public string BarCodeSearch
        {
            get { return _barCodeSearch; }
            set { _barCodeSearch = value; OnPropertyChanged("BarCodeSearch"); }
        }

        private List<ProductEntity> _goods;
        public List<ProductEntity> Goods
        {
            get { return _goods; }
            set { _goods = value; OnPropertyChanged("Goods"); }
        }

        private decimal? _sumaOrder;
        public decimal? SumaOrder
        {
            get { return _sumaOrder; }
            set { _sumaOrder = value;OnPropertyChanged("SumaOrder"); }
        }

        private decimal? _sumaUser;
        public decimal? SumaUser
        {
            get { return _sumaUser; }
            set { _sumaUser = value; OnPropertyChanged("SumaUser"); }
        }
        private List<string> _typeOplatu;
        public List<string> TypeOplatu
        {
            get { return _typeOplatu; }
            set { _typeOplatu = value; OnPropertyChanged("_typeOplatu"); }
        }
        private int _selectIndex;
        public int SelectIndex
        {
            get { return _selectIndex; }
            set { _selectIndex = value;OnPropertyChanged("_selectIndex"); }
        }

        public ICommand UpdateSize => _updateSize;

        private int _widght;
        public int Widght
        {
            get { return _widght; }
            set { _widght = value;OnPropertyChanged("Widght"); }
        }


        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }

        private void UpdateSizes()
        {
            Widght = (int)Application.Current.MainWindow.ActualWidth - 175;
            Height = (int)Application.Current.MainWindow.ActualHeight - 290;
        }
        public ICommand ClearFieldDataGid => _clearFieldDataGrid;
        private void ClearField()
        {
            Goods = new List<ProductEntity>();
            SumaUser = 0;
            SumaOrder = 0;
            _selectIndex = 0;
        }
        private bool _draingCheck;
        public bool DrawingCheck
        {
            get { return _draingCheck; }
            set { _draingCheck = value; OnPropertyChanged("DrawingCheck"); }
        }
        private bool _isFiscalCheck;
        public bool IsFiscalCheck
        {
            get { return _isFiscalCheck; }
            set { _isFiscalCheck = value; OnPropertyChanged("IsFiscalCheck"); }
        }
        public int Tag;

        public ICommand SearchBarCodeGoodsCommand => _searchBarCodeGoodsCommand;

        private void SearchBarCodeGoods()
        {
            List<ProductEntity> temp;
            if (BarCodeSearch.Length > 12)
            {
                if (BarCodeSearch != "2")
                {
                    var item = _model.Search(BarCodeSearch);
                    if (item != null)
                    {
                        item.Count = 1;
                        temp = new List<ProductEntity>();
                        temp = Goods;

                        if (temp.Find(pr => pr.Code == item.Code) != null)
                        {
                            temp.Find(pr => pr.Code == item.Code).Count += 1;
                        }
                        else
                        {
                            temp.Add(item);
                        }

                        CountingSumaOrder(temp);

                        Goods = new List<ProductEntity>();
                        Goods = temp;
                        BarCodeSearch = string.Empty;
                    }
                }
                else
                {
                    if (Goods.Count() != 0)
                    {
                        if (Goods.ElementAt(Goods.Count - 1).Count == 1)
                        {
                            temp = new List<ProductEntity>();
                            temp = Goods;

                            temp.Remove(temp.ElementAt(temp.Count - 1));
                            Goods = new List<ProductEntity>();
                            Goods = temp;
                            CountingSumaOrder(Goods);
                        }
                        else
                        {
                            temp = new List<ProductEntity>();
                            temp = Goods;


                            temp.ElementAt(Goods.Count - 1).Count -= 1;
                            Goods = new List<ProductEntity>();
                            Goods = temp;
                            CountingSumaOrder(Goods);
                        }
                        BarCodeSearch = string.Empty;
                    }
                }
            }
        }

        private void CountingSumaOrder(List<ProductEntity> products)
        {
            SumaOrder = 0;
            foreach (ProductEntity orderProduct in products)
            {
                SumaOrder += (orderProduct.Price * orderProduct.Count);
            }
        }
        public ICommand PrintingCheckCommand => _printingCheckCommand;
        private void PrintingCheck()
        {
            //_model.IsDrawinfChek = DrawingCheck;

            if (SumaUser >= SumaOrder)
            {
                double rest = ((double)(SumaUser - SumaOrder));


                decimal typeOperration;
                if (IsFiscalCheck)
                {
                    typeOperration = 0;
                }
                else
                {
                    typeOperration = 200;
                }

                if (_model.SendCheck(Goods, new OperationEntity()
                {
                    DataPacketIdentifier = 1,
                    TypeRRO = 0,
                    FiscalNumberRRO = Session.FocusDevices.FiscalNumber,
                    TaxNumber = Session.User.TIN,
                    FactoryNumberRRO = "v1",
                    TypeOperation = typeOperration,
                    MAC = _model.GetMac(),
                    CreatedAt = DateTime.Now,
                    NumberPayment = _model.GetLocalNumber(),
                    GoodsTax = "0",
                    RestPayment = Convert.ToDecimal(rest),
                    TotalPayment = (decimal)SumaOrder,
                    BuyersAmount = (decimal)SumaUser,
                    FormOfPayment = SelectIndex,
                    VersionDataPaket = 1,

                }))
                {
                    MessageBox.Show("Решта: " + rest, "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    Goods = new List<ProductEntity>();
                    BarCodeSearch = string.Empty;
                    SumaUser = new decimal();
                    SumaUser = 0;
                    SumaOrder = 0;

                    if (Tag != 0)
                    {
                        Session.Tabs.RemoveAt(Tag);
                    }
                }

            }
            else
            {
                MessageBox.Show("Сума внеску не може бути менша ніж сума чеку");
            }
        }


    }
}
