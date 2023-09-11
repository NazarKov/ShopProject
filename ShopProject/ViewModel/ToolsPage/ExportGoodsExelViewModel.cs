﻿using Microsoft.Win32;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class ExportGoodsExelViewModel : ViewModel<ExportGoodsExelViewModel>
    {
        private ExportGoodsExelModel? model;
        private SaveFileDialog? saveFileDialog;
        private ICommand addProductList;
        private ICommand saveSelectItem;
        private ICommand saveAllItem;
       

        public ExportGoodsExelViewModel()
        {
            Products = new List<Goods>();
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
           
            addProductList = new DelegateCommand(AddItemExportList);
            saveSelectItem = new DelegateCommand(SaveExprotItem);
            saveAllItem = new DelegateCommand(SaveExprotItemAll);

            _products = new List<Goods>();
            _code = string.Empty;

            SetFieldFileDialog();
            new Thread(new ThreadStart(startModelThread)).Start();
        }
        private void startModelThread()
        {
            model = new ExportGoodsExelModel();
        }
        private void SetFieldFileDialog()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.Filter = "Exel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
        }

        private List<Goods> _products;
        public List<Goods> Products
        {
            get { return _products; }
            set { _products = value;  OnPropertyChanged("Products"); }
        }

        private int _sizeDataGrid;
        public int SizeDataGrid
        {
            set { _sizeDataGrid = value;  OnPropertyChanged("SizeDataGrid"); }
        }

        private string _code;
        public string Code 
        {
            get { return _code; } 
            set { _code = value; OnPropertyChanged("Code"); }
        }

        public ICommand AddProductList => addProductList;

        private void AddItemExportList()
        {
            if (model != null)
            {
                var resultProduct = model.GetItem(_code);
                if (resultProduct != null)
                {
                    List<Goods> tempProduct = new List<Goods>();

                    tempProduct.AddRange(_products);
                    tempProduct.Add(resultProduct);
                    Products = tempProduct;
                }
            }
        }

        public ICommand SaveSelectItem => saveSelectItem;

        private void SaveExprotItem()
        {
            Save(_products);
        }

        public ICommand SaveAllItem => saveAllItem;

        private void SaveExprotItemAll()
        {
            if(model != null)
               Save(model.GetItems());
        }
        private void Save(List<Goods> products)
        {
            if (saveFileDialog != null)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    string path = saveFileDialog.FileName;
                    if(model!=null)
                       if (model.Export(path, products))
                       {
                           MessageBox.Show("товар успішно експортовано");
                       }
                       else
                       {
                           MessageBox.Show("товар не експортовано");
                       }
                }
            }
        }

    }
}