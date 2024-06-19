using Microsoft.Win32;
using ShopProject.DataBase.Model;
using ShopProject.Helpers.DataGridViewHelperModel;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
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

            Product = new List<ExportProductInFileHelper>();

            _Product = new List<ExportProductInFileHelper>();
            _searchCode = string.Empty;

            SetFieldFileDialog();
        }
    
        private void SetFieldFileDialog()
        {
            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".xlsx";
            _saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
        }

        private List<ExportProductInFileHelper> _Product;
        public List<ExportProductInFileHelper> Product
        {
            get { return _Product; }
            set { _Product = value;  OnPropertyChanged("Product"); }
        }


        private string _searchCode;
        public string SearchCode 
        {
            get { return _searchCode; } 
            set { _searchCode = value; OnPropertyChanged("SearchCode"); }
        }

        public ICommand AddProductCommand => _addProductCommand;

        private void AddItemExportList()
        {
            List<ExportProductInFileHelper> _tempProduct = new List<ExportProductInFileHelper>();


            if (SearchCode.Length > 12)
            {
                if (_model != null)
                {
                    if (SearchCode != "0000000000000")
                    {
                        var resultProduct = _model.GetItem(SearchCode);
                        if (resultProduct.ProductCount != 0)
                        {
                            _tempProduct.AddRange(Product);

                            if (_tempProduct.Count != 0)
                            {
                                if (_tempProduct.Where(item => item.Product.ID == resultProduct.Product.ID).FirstOrDefault() != null)
                                {
                                    _tempProduct.Where(item => item.Product.ID == resultProduct.Product.ID).FirstOrDefault().ProductCount += 1;
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


                            Product.Clear();
                            Product = _tempProduct;

                            SearchCode = string.Empty;
                        }
                    }
                    else
                    {
                        _tempProduct.AddRange(Product);
                        
                        if (_tempProduct.Count != 0)
                        {
                            _tempProduct.RemoveAt(_tempProduct.Count - 1);
                        }

                        Product.Clear();
                        Product = _tempProduct;

                        SearchCode = string.Empty;
                    }
                   
                }
            }
        }

        public ICommand SaveItemInFileCommand => _saveItemInFileCommand;

        private void SaveExprotItem()
        {
            _saveFileDialog.ShowDialog();
            if (_model != null)
            {
                _model.Save(Product, _saveFileDialog.FileName);
            }
        }

        public ICommand SaveAllItemInFileCommand => _saveAllItemInFileCommand;

        private void SaveExprotItemAll()
        {
            _saveFileDialog.ShowDialog();
            if(_model!=null)
            {
                _model.Save(_saveFileDialog.FileName);
            }
        }
     

    }
}
