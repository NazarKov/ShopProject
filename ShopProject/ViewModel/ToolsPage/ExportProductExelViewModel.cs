using Microsoft.Win32;
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
    internal class ExportProductExelViewModel : ViewModel<ExportProductExelViewModel>
    {
        private ExportProductExelModel? model;
        private SaveFileDialog? saveFileDialog;
        private ICommand addProductList;
        private ICommand saveSelectItem;
        private ICommand saveAllItem;
       

        public ExportProductExelViewModel()
        {
            Products = new List<Product>();
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
           
            addProductList = new DelegateCommand(AddItemExportList);
            saveSelectItem = new DelegateCommand(SaveExprotItem);
            saveAllItem = new DelegateCommand(SaveExprotItemAll);

            _products = new List<Product>();
            _code = string.Empty;

            SetFieldFileDialog();
            new Thread(new ThreadStart(startModelThread)).Start();
        }
        private void startModelThread()
        {
            model = new ExportProductExelModel();
        }
        private void SetFieldFileDialog()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".cvs";
            saveFileDialog.Filter = "Exel files(*.cvs)|*.cvs|All files(*.*)|*.*";
        }

        private List<Product> _products;
        public List<Product> Products
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
                    List<Product> tempProduct = new List<Product>();

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
        private void Save(List<Product> products)
        {
            if (saveFileDialog != null)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    string path = saveFileDialog.FileName;
                    if(model!=null)
                       if (FileCVS.writeFile(path, products))
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
