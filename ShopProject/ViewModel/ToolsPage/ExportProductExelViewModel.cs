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
        ExportProductExelModel model;
        SaveFileDialog saveFileDialog;
        private ICommand addProductList;
        private ICommand saveSelectItem;
        private ICommand saveAllItem;
       

        public ExportProductExelViewModel()
        {
            Products = new List<Product>();
            SizeDataGrid = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            saveFileDialog = new SaveFileDialog();

            addProductList = new DelegateCommand(addItemList);
            saveSelectItem = new DelegateCommand(saveItem);
            saveAllItem = new DelegateCommand(saveItemAll);

            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|Exel files(*.csv)|*.csv|All files(*.*)|*.*";


            new Thread(new ThreadStart(startModelThread)).Start();
        }
        void startModelThread()
        {
            model = new ExportProductExelModel();

        }

        private List<Product> _products;
        public List<Product> Products
        {
            set { _products = value;  OnPropertyChanged("Products"); }
            get { return _products; }
        }

        private int _sizeDataGrid;
        public int SizeDataGrid
        {
            set
            {
                _sizeDataGrid = value;
                OnPropertyChanged("SizeDataGrid");
            }
        }

        private string _code;
        public string Code { get { return _code; } set { _code = value; OnPropertyChanged("Code"); } }

        public ICommand AddProductList => addProductList;

        private void addItemList()
        {
            var resultProduct = model.GetItem(_code);
            if (resultProduct != null)
            {
                List<Product> tempProduct = new List<Product>();

                tempProduct.AddRange(_products);
                tempProduct.Add(resultProduct);
                Products = tempProduct;
                
            }
            else
            {
                MessageBox.Show("Товар не знайдено");
            }
        }

        public ICommand SaveSelectItem => saveSelectItem;

        private void saveItem()
        {
            save(_products);
        }

        public ICommand SaveAllItem => saveAllItem;

        private void saveItemAll()
        {
            save(model.GetItems());
        }
        private void save(List<Product> products)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                string path = saveFileDialog.FileName;

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
