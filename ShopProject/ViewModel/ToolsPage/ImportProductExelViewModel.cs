using Microsoft.Win32;
using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class ImportProductExelViewModel : ViewModel<ImportProductExelViewModel>
    {
        private ImportProductExelModel _model;
        private FileExel db;

        private ICommand openFileExel;
        private ICommand saveItemDb;
        private ICommand updateTablePage;

        public ImportProductExelViewModel()
        {
            _model= new ImportProductExelModel();

            openFileExel = new DelegateCommand(SetItemDataGrid);
            saveItemDb = new DelegateCommand(saveItem);
            updateTablePage = new DelegateCommand(UpdatePage);
            TableName = new List<string>();
            SelectIndex = 0;
        }

        private DataTable _productTemp;
        public DataTable ProductTemp 
        {
            get { return _productTemp; }
            set { _productTemp = value; OnPropertyChanged("ProductTemp");}
        }

        private int _indexCode;
        public int IndexCode
        {
            get { return _indexCode; }
            set { _indexCode = value; }
        }

        private int _indexName;
        public int IndexName
        {
            get { return _indexName; }
            set { _indexName = value; }
        }

        private int _indexArticule;
        public int IndexArticule
        {
            get { return _indexArticule; }
            set { _indexArticule = value;OnPropertyChanged("IndexArticule"); }
        }

        private int _indexDescription;
        public int IndexDescription
        {
            get { return _indexDescription; }
            set { _indexDescription = value; }
        }

        private int _indexPrice;
        public int IndexPrice
        {
            get { return _indexPrice; }
            set { _indexPrice = value; }
        }

        private int _indexPurchasePrice;
        public int IndexPurchasePrice
        {
            get { return _indexPurchasePrice; }
            set { _indexPurchasePrice = value; }
        }

        private int _indexCount;
        public int IndexCount
        {
            get { return _indexCount; }
            set { _indexCount = value; }
        }

        private int _indexUnit;
        public int IndexUnit
        {
            get { return _indexUnit; }
            set { _indexUnit = value; }
        }

        private int _indexTop;
        public int IndexTop
        {
            get { return _indexTop; }
            set { _indexTop = value; }
        }

        private int _indexBottom;
        public int IndexBottom
        {
            get { return _indexBottom; }
            set { _indexTop = value; }
        }

        private List<string> _tableName;
        public List<string> TableName
        {
            get { return _tableName; }
            set { _tableName = value; OnPropertyChanged("TableName"); }
        }

        private int _selectIndex;
        public int SelectIndex
        {
            get { return _selectIndex; }
            set { _selectIndex = value; OnPropertyChanged("SelectIndex"); }
        }

        public ICommand OpenFileExel => openFileExel;
        private void SetItemDataGrid()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                db = new FileExel(openFileDialog.FileName);
                ProductTemp = db.GetTabel(_selectIndex);
                TableName = db.GetTableName();
            }
        }
        public ICommand UpdateTablePage => updateTablePage;
        private void UpdatePage()
        {
            ProductTemp = db.GetTabel(_selectIndex);
        }


        public ICommand SaveItemDb => saveItemDb;
        private void saveItem()
        {
            _model.SetItemDataBase(_productTemp, _indexCode,_indexArticule, _indexName, _indexDescription, _indexPrice, _indexPurchasePrice, _indexCount, _indexUnit);
        }

        public ICommand ExitWindow { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;


    }
}
