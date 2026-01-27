using Microsoft.Win32; 
using ShopProject.Model;
using ShopProject.Helpers.Command;
using ShopProject.Model.StoragePage.ExcelPage.ExportExcelPage;
using ShopProject.UIModel.StoragePage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ExcelPage.ExportExcelPage
{
    internal class ExportProductExelViewModel : ViewModel<ExportProductExelViewModel>
    {
        private ExportProductExelModel? _model;
        private SaveFileDialog? _saveFileDialog;
        private ICommand _addProductCommand;
        private ICommand _saveItemInFileCommand;
        private ICommand _saveAllItemInFileCommand;



        public ExportProductExelViewModel()
        {
            _model = new ExportProductExelModel();


            _addProductCommand = new DelegateCommand(AddItemExportList);
            _saveItemInFileCommand = new DelegateCommand(SaveExprotItem);
            _saveAllItemInFileCommand = new DelegateCommand(SaveExprotItemAll);
  
            _products = new List<Product>();
            _searchCode = string.Empty;

            SetFieldFileDialog();
        }

        private void SetFieldFileDialog()
        {
            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".xlsx";
            _saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }


        private string _searchCode;
        public string SearchCode
        {
            get { return _searchCode; }
            set { _searchCode = value; OnPropertyChanged(nameof(SearchCode)); }
        }

        public ICommand AddProductCommand => _addProductCommand;

        private void AddItemExportList()
        {

            List<Product> _tempProduct = new List<Product>();
            if (SearchCode.Length >= 12)
            {
                if (_model != null)
                {
                    if (SearchCode != "0000000000000")
                    {
                        Task t = Task.Run(async () => {

                            var resultProduct = await _model.GetItem(SearchCode);
                            if (resultProduct.Count != 0)
                            {
                                _tempProduct.AddRange(Products);

                                if (_tempProduct.Count != 0)
                                {
                                    if (_tempProduct.Where(item => item.ID == resultProduct.ID).FirstOrDefault() != null)
                                    {
                                        _tempProduct.Where(item => item.ID == resultProduct.ID).FirstOrDefault().Count += 1;
                                    }
                                    else
                                    {
                                        _tempProduct.Add(resultProduct);
                                    }
                                }
                                else
                                {
                                    _tempProduct.Add(resultProduct);
                                }
                            }

                        });
                        t.ContinueWith(t =>
                        {
                            UpdateDataGridView(_tempProduct);
                        });
                        
                    }
                    else
                    {
                        _tempProduct.AddRange(Products);

                        if (_tempProduct.Count != 0)
                        {
                            _tempProduct.RemoveAt(_tempProduct.Count - 1);
                        }
                        UpdateDataGridView(_tempProduct);
                    } 
                }
            }
        }
        private void UpdateDataGridView(List<Product> products )
        {
            Products.Clear();
            Products = products;

            SearchCode = string.Empty;
        }
        public ICommand SaveItemInFileCommand => _saveItemInFileCommand;

        private void SaveExprotItem()
        {
            _saveFileDialog.ShowDialog();
            if (_model != null)
            { 
                if (_model.Save(Products, _saveFileDialog.FileName))
                {
                    MessageBox.Show("товар успішно експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("товар не експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public ICommand SaveAllItemInFileCommand => _saveAllItemInFileCommand;

        private void SaveExprotItemAll()
        {
            _saveFileDialog.ShowDialog();

            Task.Run(async () => {
                if (_model != null)
                {
                    if (await _model.Save(_saveFileDialog.FileName))
                    {
                        MessageBox.Show("товар успішно експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("товар не експортовано", "informations", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            });
        }
    }
}
