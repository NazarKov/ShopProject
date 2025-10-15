using ShopProject.Helpers;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.Model.Command;
using ShopProject.Model.StoragePage;
using ShopProject.UIModel.StoragePage;
using ShopProject.View.StoragePage.ProductUnitPage; 
using ShopProject.ViewModel.TemplatePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.StoragePage
{
    internal class UnitsOfMeasureViewModel : ViewModel<UnitsOfMeasureViewModel>
    {
        private UnitsOfMeasureModel _model;


        private ICommand _openCreateProductUnitPageCommand;
        private ICommand _updateGridViewCommad;
        private ICommand _updateSizeGridCommand;

        private bool _isReadyUpdateDataGriedView;
        private static Timer _timer;
        private static string _itemSearch;

        public UnitsOfMeasureViewModel()
        {

            _model = new UnitsOfMeasureModel();


            _openCreateProductUnitPageCommand = new DelegateCommand(() => { new CreateProductUnitView().Show(); });
            _updateGridViewCommad = new DelegateCommand(() => { SetFieldPage(); });
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);
            _heigth = 150;

            _units = new List<ProductUnit>();
            _statusUnits = new List<string>();
            _paginator = new TemplatePaginatorButtonViewModel();
            _countShowList = new List<string>();
            _isReadyUpdateDataGriedView = false;
            _itemSearch = string.Empty;

            Paginator.Callback = UpdateDataGridView;


            _timer = new Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);
            SetFieldPage();

            Mediator.Subscribe("ReloadUnitsGriedView", (object obj) => { UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList))); });
        }

        private List<ProductUnit> _units;
        public List<ProductUnit> Units
        {
            get { return _units; }
            set
            {
                _units = value; OnPropertyChanged(nameof(Units));
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

        private List<string> _statusUnits;
        public List<string> StatusUnits
        {
            get { return _statusUnits; }
            set { _statusUnits = value; OnPropertyChanged(nameof(_statusUnits)); }
        }

        private int _selectedStatusUnit;
        public int SelectedStatusUnit
        {
            get { return _selectedStatusUnit; }
            set
            {
                _selectedStatusUnit = value; OnPropertyChanged(nameof(SelectedStatusUnit));
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

        private void SetFielComboBoxTypeStatusUnits()
        {
            SelectedStatusUnit = 0;
            if (StatusUnits.Count == 0)
            {
                StatusUnits.Add(TypeStatusUnit.Unknown.ToString());
                StatusUnits.Add(TypeStatusUnit.Favorite.ToString());
                StatusUnits.Add(TypeStatusUnit.UnFavorite.ToString());
            }
        }

        private void SetFieldPage()
        {
            SetFieldComboBox();
            SetFielComboBoxTypeStatusUnits();
            SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
        }

        private void SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            PaginatorData<ProductUnit> result = new PaginatorData<ProductUnit>();
            Task t = Task.Run(async () =>
            {

                result = await _model.GetUnitsPageColumn(page, countCoulmn, Enum.Parse<TypeStatusUnit>(StatusUnits.ElementAt(SelectedStatusUnit)));
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
                Units = result.Data.ToList();
                _isReadyUpdateDataGriedView = true;
            });
        }

        private void UpdateDataGridView(int countCoulmn, int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (Units.Count > 0)
                {
                    Units.Clear();
                } 

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_itemSearch == string.Empty && _itemSearch == "")
                {
                    SetFieldDataGridView(countCoulmn, page, false);
                }
                else
                {
                    SearchByNameAndByBarCode(countCoulmn, page);
                }
            }
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchProduct, CanRegister); }

        private void SearchProduct(object parameter)
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
            PaginatorData<ProductUnit> result = new PaginatorData<ProductUnit>();

            Task t = Task.Run(async () =>
            {
                if (Regex.Matches(_itemSearch, "[0-9]").Count == _itemSearch.Length)
                {
                    result.Data = new List<ProductUnit>() { (await _model.SearchByBarCode(_itemSearch, Enum.Parse<TypeStatusUnit>(StatusUnits.ElementAt(SelectedStatusUnit)))) };
                }
                else
                {
                    result = await _model.SearchByName(_itemSearch, page, countColumn, Enum.Parse<TypeStatusUnit>(StatusUnits.ElementAt(SelectedStatusUnit)));
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
                Units = result.Data.ToList();
            });
        }

        private bool CanRegister(object parameter) => true;

        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateProductUnitPageCommand => _openCreateProductUnitPageCommand;

        public ICommand OpenUpdateProductUnitPageCommand { get => new DelegateParameterCommand(UpdateUnit, CanRegister); }

        private void UpdateUnit(object parameter)
        {
            var items = (parameter as IList);
            if (items != null && items.Count > 0)
            {
                Session.ProductUnit = (ProductUnit)(items[0]);
                new UpdateProductUnitView().ShowDialog();
            }
            else
            {
                MessageBox.Show("Ви не обрали елемент");
            }
        }

        public ICommand DeleteProductUnitCommand { get => new DelegateParameterCommand(DeleteUnit, CanRegister); }

        private void DeleteUnit(object parameter)
        {
            var items = parameter as IList;


            if (items != null && items.Count > 0)
            {
                bool result = false;
                Task t = Task.Run(async () =>
                {

                    result = await _model.Delete((ProductUnit)items[0]);
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

        public ICommand UpdateStatusToFavoriteProductUnitCommand { get => new DelegateParameterCommand(UpdateToFavoriteStatus, CanRegister); }

        private void UpdateToFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            {
                bool result = false;
                Task t = Task.Run(async () =>
                {

                    result = await _model.ChangeStatus((ProductUnit)items[0], TypeStatusUnit.Favorite);
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

        public ICommand UpdateStatusToUnFavoriteProductUnitCommand { get => new DelegateParameterCommand(UpdateToUnFavoriteStatus, CanRegister); }

        private void UpdateToUnFavoriteStatus(object parameter)
        {
            var items = parameter as IList;

            if (items != null && items.Count > 0)
            {
                bool result = false;
                Task t = Task.Run(async () =>
                {

                    result = await _model.ChangeStatus((ProductUnit)items[0], TypeStatusUnit.UnFavorite);
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
