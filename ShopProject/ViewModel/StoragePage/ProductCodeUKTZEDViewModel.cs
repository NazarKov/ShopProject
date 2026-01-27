using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.Helpers.Command;
using ShopProject.Model.StoragePage;
using ShopProject.UIModel.StoragePage;
using ShopProject.View.StoragePage.ProductCodeUKTZEDPage;
using ShopProject.View.StoragePage.ProductUnitPage;
using ShopProject.ViewModel.TemplatePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class ProductCodeUKTZEDViewModel : ViewModel<ProductCodeUKTZEDViewModel>
    {
        private ProductCodeUKTZEDModel _model;

        private bool _isReadyUpdateDataGriedView;
        private static Timer _timer;
        private static string _itemSearch;

        private ICommand _openCreateProductCodeUKTZEDPageCommand;
        private ICommand _updateGridViewCommad;
        private ICommand _updateSizeGridCommand;

        public ProductCodeUKTZEDViewModel()
        {
            _model = new ProductCodeUKTZEDModel();
            _openCreateProductCodeUKTZEDPageCommand = new DelegateCommand(() => { new CreateProductCodeUKTZEDView().Show(); });

            _updateGridViewCommad = new DelegateCommand(() => { SetFieldPage(); });
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);

            _heigth = 150;

            _codeUKTZED = new List<ProductCodeUKTZED>();
            _statusCodeUKTZED = new List<string>();
            _paginator = new TemplatePaginatorButtonViewModel();
            _countShowList = new List<string>();
            _isReadyUpdateDataGriedView = false;
            _itemSearch = string.Empty;

            Paginator.Callback = UpdateDataGridView;


            _timer = new Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);
            SetFieldPage();

            MediatorService.AddEvent("ReloadCodeUKTEDGriedView", (object obj) => { UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList))); });
        }

        private List<ProductCodeUKTZED> _codeUKTZED;
        public List<ProductCodeUKTZED> CodeUKTZED
        {
            get { return _codeUKTZED; }
            set
            {
                _codeUKTZED = value; OnPropertyChanged(nameof(CodeUKTZED));
            }
        }

        private List<string> _countShowList;
        public List<string> CountShowList
        {
            get { return _countShowList; }
            set { _countShowList = value; OnPropertyChanged(nameof(CountShowList)); }
        }

        private int _selectIndexCountShowList;
        public int SelectIndexCountShowList
        {
            get { return _selectIndexCountShowList; }
            set
            {
                _selectIndexCountShowList = value; OnPropertyChanged(nameof(SelectIndexCountShowList));
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            }
        }

        private List<string> _statusCodeUKTZED;
        public List<string> StatusCodeUKTZED
        {
            get { return _statusCodeUKTZED; }
            set { _statusCodeUKTZED = value; OnPropertyChanged(nameof(StatusCodeUKTZED)); }
        }

        private int _selectedStatusCodeUKTZED;
        public int SelectedStatusCodeUKTZED
        {
            get { return _selectedStatusCodeUKTZED; }
            set
            {
                _selectedStatusCodeUKTZED = value; OnPropertyChanged(nameof(SelectedStatusCodeUKTZED));
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            }
        }

        private TemplatePaginatorButtonViewModel _paginator;
        public TemplatePaginatorButtonViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
        }

        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged(nameof(Heigth)); }
        }

        private void SetFieldComboBox()
        {
            if (CountShowList.Count == 0)
            {
                CountShowList.Add("10");
                CountShowList.Add("25");
                CountShowList.Add("50");
                CountShowList.Add("100");
                CountShowList.Add("250");
                CountShowList.Add("500");
                CountShowList.Add("1000");
            }
            SelectIndexCountShowList = 0;
        }

        private void SetFielComboBoxTypeStatusCodeUKTZED()
        {
            SelectedStatusCodeUKTZED = 0;
            if (StatusCodeUKTZED.Count == 0)
            {
                StatusCodeUKTZED.Add(TypeStatusUnit.Unknown.ToString());
                StatusCodeUKTZED.Add(TypeStatusUnit.Favorite.ToString());
                StatusCodeUKTZED.Add(TypeStatusUnit.UnFavorite.ToString());
            }
        }

        private void SetFieldPage()
        {
            SetFieldComboBox();
            SetFielComboBoxTypeStatusCodeUKTZED();
            SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
        }

        private void SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            PaginatorData<ProductCodeUKTZED> result = new PaginatorData<ProductCodeUKTZED>();
            Task t = Task.Run(async () =>
            { 
                result = await _model.GetCodeUKTZEDPageColumn(page, countCoulmn, Enum.Parse<TypeStatusCodeUKTZED>(StatusCodeUKTZED.ElementAt(SelectedStatusCodeUKTZED)));
            });
            t.ContinueWith(t =>
            {
                if (reloadbutton)
                {
                    if(result.Pages == 0)
                    {
                        Paginator.CountButton = 1;
                    }
                    else
                    {
                        Paginator.CountButton = result.Pages;
                    }
                } 
                CodeUKTZED = result.Data.ToList();
                _isReadyUpdateDataGriedView = true;
            });
        }

        private void UpdateDataGridView(int countCoulmn, int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (CodeUKTZED.Count > 0)
                {
                    CodeUKTZED.Clear();
                } 

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_itemSearch == string.Empty && _itemSearch == "")
                {
                    SetFieldDataGridView(countCoulmn, page, true);
                }
                else
                {
                    SearchByNameAndByBarCode(countCoulmn, page);
                }
            }
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchCodeUKTZED, CanRegister); }

        private void SearchCodeUKTZED(object parameter)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _itemSearch = parameter.ToString();

            _timer.Change(2000, Timeout.Infinite);// 2000 затримка в дві секунди для продовження ведення тексту
        }

        private void OnInputStopped(object state)
        {
            UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void SearchByNameAndByBarCode(int countColumn, int page)
        {
            PaginatorData<ProductCodeUKTZED> result = new PaginatorData<ProductCodeUKTZED>();

            Task t = Task.Run(async () =>
            {
                if (Regex.Matches(_itemSearch, "[0-9]").Count == _itemSearch.Length)
                {
                    result.Data = new List<ProductCodeUKTZED>() { (await _model.SearchByBarCode(_itemSearch, Enum.Parse<TypeStatusCodeUKTZED>(StatusCodeUKTZED.ElementAt(SelectedStatusCodeUKTZED)))) };
                }
                else
                {
                    result = await _model.SearchByName(_itemSearch, page, countColumn, Enum.Parse<TypeStatusCodeUKTZED>(StatusCodeUKTZED.ElementAt(SelectedStatusCodeUKTZED)));
                }

            });
            t.ContinueWith(t =>
            {
                if (result.Pages == 0)
                {
                    Paginator.CountButton = 1; 
                }
                else
                {
                    Paginator.CountButton = result.Pages;
                }
                CodeUKTZED = result.Data.ToList();
            });
        }

        private bool CanRegister(object parameter) => true;

        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateProductCodeUKTZEDPageCommand => _openCreateProductCodeUKTZEDPageCommand;

        public ICommand OpenUpdateProductCodeUKTZEDPageCommand { get => new DelegateParameterCommand(UpdateCodeUKTZED, CanRegister); }

        private void UpdateCodeUKTZED(object parameter)
        {
            var items = (parameter as IList);
            if (items != null && items.Count > 0)
            {
                Session.ProductCodeUKTZEDEntity = (ProductCodeUKTZED)(items[0]);
                new UpdateProductCodeUKTZEDView().Show();
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }
        }

        public ICommand DeleteProductCodeUKTZEDCommand { get => new DelegateParameterCommand(DeleteCodeUKTZED, CanRegister); }

        private void DeleteCodeUKTZED(object parameter)
        {
            var items = parameter as IList;


            if (items != null && items.Count > 0)
            {
                bool result = false;
                Task t = Task.Run(async () =>
                {

                    result = await _model.Delete((ProductCodeUKTZED)items[0]);
                });
                t.ContinueWith(t =>
                {
                    if (result)
                    {
                        MessageBox.Show("Одиницю видалено");
                    }
                    UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
                });
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }

        }

        public ICommand UpdateStatusToFavoriteProductCodeUKTZEDCommand { get => new DelegateParameterCommand(UpdateToFavoriteStatus, CanRegister); }

        private void UpdateToFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            {
                bool result = false;
                Task t = Task.Run(async () =>
                { 
                    result = await _model.ChangeStatus((ProductCodeUKTZED)items[0], TypeStatusCodeUKTZED.Favorite);
                });
                t.ContinueWith(t =>
                {
                    if (result)
                    {
                        MessageBox.Show("Одиницю оновлено");
                    }
                    UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
                });
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }

        }

        public ICommand UpdateStatusToUnFavoriteProductCodeUKTZEDCommand { get => new DelegateParameterCommand(UpdateToUnFavoriteStatus, CanRegister); }

        private void UpdateToUnFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            {
                bool result = false;
                Task t = Task.Run(async () =>
                {

                    result = await _model.ChangeStatus((ProductCodeUKTZED)items[0], TypeStatusCodeUKTZED.UnFavorite);
                });
                t.ContinueWith(t =>
                {
                    if (result)
                    {
                        MessageBox.Show("Одиницю оновлено");
                    }
                    UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
                });
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }

        }

        public ICommand UpdateSizeCommand => _updateSizeGridCommand;
        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 340;
        }
    }
}
