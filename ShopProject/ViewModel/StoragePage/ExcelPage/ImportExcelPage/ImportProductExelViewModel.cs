 using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage.ExcelPage.ImportExcelPage;
using ShopProject.UIModel.StoragePage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage.ExcelPage.ImportExcelPage
{
    internal class ImportProductExelViewModel : ViewModel<ImportProductExelViewModel>
    {
        private ImportProductExelModel _model;
        private ICommand _openFileExelCommand;
        private ICommand _saveItemDbCommand; 
        private ICommand _openMappingPanelCommand;

        public ImportProductExelViewModel()
        {
            _model = new ImportProductExelModel();
            _openFileExelCommand = new DelegateCommand(SetItemDataGrid);
            _saveItemDbCommand = new DelegateCommand(SaveItem); 
            _openMappingPanelCommand = new DelegateCommand(OpenMappingPanel);

            _table = new DataTable();
            _nameFile = string.Empty;
            _headers = new ObservableCollection<string>();
            _pages = new ObservableCollection<string>();
            MappingPanel = Visibility.Collapsed;
            _selectColumns = new SelectExcelColumn();

            SetFieldPage();
        }

        private Visibility _mappingPanel {  get; set; }
        public Visibility MappingPanel
        {
            get { return _mappingPanel; }
            set { _mappingPanel = value;OnPropertyChanged(nameof(MappingPanel)); }
        }
        private string _nameFile;
        public string NameFile
        {
            get { return _nameFile; }
            set { _nameFile = value;OnPropertyChanged(nameof(NameFile));}
        }

        private DataTable _table;
        public DataTable Table 
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(nameof(Table));}
        }

        private ObservableCollection<string> _headers;
        public ObservableCollection<string> Headers
        {
            get { return _headers; }
            set { _headers = value; OnPropertyChanged(nameof(Headers));}
        }

        private SelectExcelColumn _selectColumns;
        public SelectExcelColumn SelectColumns
        {
            get { return _selectColumns; }
            set { _selectColumns = value; OnPropertyChanged(nameof(SelectColumns)); }
        } 
        private ObservableCollection<string> _pages;
        public ObservableCollection<string> Pages
        {
            get{ return _pages; }
            set { _pages= value; OnPropertyChanged(nameof(Pages));}
        }

        private int _selectPage;
        public int SelectPage
        {
            get { return _selectPage; }
            set { _selectPage = value; OnPropertyChanged(nameof(SelectPage)); UpdateDataGridView(_selectPage); }
        }

        private int _topRow;
        public int TopRow
        {
            get { return _topRow;}
            set { _topRow = value; OnPropertyChanged(nameof(TopRow)); }
        }
        private int _bottomRow;
        public int BottomRow
        {
            get { return _bottomRow; }
            set { _bottomRow = value; OnPropertyChanged(nameof(BottomRow)); }
        }

        private void SetFieldPage()
        {
            Pages.Add("оберіть сторінку");
            NameFile = "Файл не вибрано";
            Headers.Add("Оберіть совпчик");
            _selectPage = 0;
        } 

        public ICommand OpenFileExel => _openFileExelCommand;
        private void SetItemDataGrid()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _model.SetPath(openFileDialog.FileName);
                if (_model.LoadFile())
                {
                    NameFile = openFileDialog.FileName;
                    var result  = _model.GetTableName();
                   
                    Pages.Clear(); 
                    Pages.Add("оберіть сторінку");
                    foreach (var item in result) 
                    {
                        Pages.Add(item);
                    }
                    SelectPage = 0; 
                }
            }
        }

        private void UpdateDataGridView(int i = 0)
        {
            i--;
            if (i >= 0)
            {
                var result = _model.GetTable(i);
                if (result != null)
                {
                    Table = result; 
                    BottomRow = _model.GetMaxRow(i);
                }
                TopRow = 0;

                var headers = _model.GetTableHeaders(i);
                if (headers != null) 
                { 
                    foreach (var item in headers)
                    {
                        Headers.Add(item);
                    }  
                }
                Headers.Add("Стовпчик відсутній");
            }
            if (i < 0)
            {
                Table = new DataTable();
                Headers.Clear();
                Headers = new ObservableCollection<string>() { "Оберіть совпчик" };
            }
            SelectColumns = new SelectExcelColumn();
        }

        public ICommand SaveItemDb => _saveItemDbCommand;
        private void SaveItem()
        {
            Task.Run(async () => {
                if (await _model.SaveItem(SelectPage-1,SelectColumns,TopRow,BottomRow))
                {
                    System.Windows.MessageBox.Show("Товари добалено", "Informations", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Помилка добавленя", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                } 
            });
        }

        public ICommand OpenMappingPanelCommand => _openMappingPanelCommand;

        public void OpenMappingPanel()
        {
            if (MappingPanel == Visibility.Visible)
            {
                MappingPanel = Visibility.Collapsed;
            }
            else
            {
                MappingPanel = Visibility.Visible;
            }
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
