using Microsoft.Win32;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
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
        private ExportGoodsExelModel? _model;
        private SaveFileDialog? _saveFileDialog;
        private ICommand _addGoodsCommand;
        private ICommand _saveItemInFileCommand;
        private ICommand _saveAllItemInFileCommand;

      

        public ExportGoodsExelViewModel()
        {
            _model = new ExportGoodsExelModel();

    
            _addGoodsCommand = new DelegateCommand(AddItemExportList);
            _saveItemInFileCommand = new DelegateCommand(SaveExprotItem);
            _saveAllItemInFileCommand = new DelegateCommand(SaveExprotItemAll);

            Goods = new List<HelperClassExportGoodsInFile>();

            _goods = new List<HelperClassExportGoodsInFile>();
            _searchCode = string.Empty;

            SetFieldFileDialog();
        }
    
        private void SetFieldFileDialog()
        {
            _saveFileDialog = new SaveFileDialog();
            _saveFileDialog.DefaultExt = ".xlsx";
            _saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
        }

        private List<HelperClassExportGoodsInFile> _goods;
        public List<HelperClassExportGoodsInFile> Goods
        {
            get { return _goods; }
            set { _goods = value;  OnPropertyChanged("Goods"); }
        }


        private string _searchCode;
        public string SearchCode 
        {
            get { return _searchCode; } 
            set { _searchCode = value; OnPropertyChanged("SearchCode"); }
        }

        public ICommand AddGoodsCommand => _addGoodsCommand;

        private void AddItemExportList()
        {
            List<HelperClassExportGoodsInFile> _tempGoods = new List<HelperClassExportGoodsInFile>();


            if (SearchCode.Length > 12)
            {
                if (_model != null)
                {
                    if (SearchCode != "0000000000000")
                    {
                        var resultGoods = _model.GetItem(SearchCode);
                        if (resultGoods.goodsCount != 0)
                        {
                            _tempGoods.AddRange(Goods);

                            if (_tempGoods.Count != 0)
                            {
                                if (_tempGoods.Where(item => item.goods.id == resultGoods.goods.id).FirstOrDefault() != null)
                                {
                                    _tempGoods.Where(item => item.goods.id == resultGoods.goods.id).FirstOrDefault().goodsCount += 1;
                                }
                                else
                                {
                                    _tempGoods.Add(resultGoods);
                                }
                            }
                            else
                            {
                                _tempGoods.Add(resultGoods);
                            }


                            Goods.Clear();
                            Goods = _tempGoods;

                            SearchCode = string.Empty;
                        }
                    }
                    else
                    {
                        _tempGoods.AddRange(Goods);
                        
                        if (_tempGoods.Count != 0)
                        {
                            _tempGoods.RemoveAt(_tempGoods.Count - 1);
                        }

                        Goods.Clear();
                        Goods = _tempGoods;

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
                _model.Save(Goods, _saveFileDialog.FileName);
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
