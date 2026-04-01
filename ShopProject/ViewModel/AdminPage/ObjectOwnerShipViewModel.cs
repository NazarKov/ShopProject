using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.CompositionRoot.Interface; 
using ShopProject.Model.Domain.Paginator;
using ShopProject.Model.Enum;
using ShopProject.Model.UI.ObjectOwner;
using ShopProject.Services.Modules.MappingServise;
using ShopProject.Services.Modules.ModelService.ObjectOwner.Interface;
using ShopProject.Views.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage
{
    internal class ObjectOwnerShipViewModel : ViewModel<ObjectOwnerShipViewModel> , IViewModelLoadResourse
    {
        private System.Windows.Forms.OpenFileDialog _openFileDialog;

        private static System.Threading.Timer _timer;
        private static string? _nameSearch;
        private bool _isReadyUpdateDataGriedView;

        private ICommand _openDialogWindowCommand;
        private ICommand _closeDialogWindowCommand;
        private ICommand _openKeyCommand;
        private ICommand _saveObjectOwnerCommand;
        private ICommand _deleteObjectCommand;
        private ICommand _openWindowDataObjectCommand;
        private ICommand _updateItemDataGridView;
        private ICommand _updateSizeGridCommand;

        private IObjectOwnerService _objectOwnerService;

        public ObjectOwnerShipViewModel(IObjectOwnerService objectOwnerService)
        {
            _objectOwnerService = objectOwnerService;
            _password = string.Empty;
            _paginator = new  PaginatorViewModel();
            _countShowList = new List<string>();
            _nameSearch = string.Empty;
            _statusObjectOwner = new List<string>();
            _objectList = new List<ObjectOwnerModel>();
            _visibilitiDialogWindow = Visibility.Hidden;
            _openFileDialog = new System.Windows.Forms.OpenFileDialog();
            _objectListDialogWindow = new List<ObjectOwnerDialogWindowModel>();

            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);
            _openDialogWindowCommand = new DelegateCommand(OpenDialogWindow);
            _closeDialogWindowCommand = new DelegateCommand(CloseDialogWindow);
            _openKeyCommand = new DelegateCommand(OpenKey);
            _saveObjectOwnerCommand = new DelegateCommand(SaveObjectOwner);
            _deleteObjectCommand = CreateCommandAsync(DeleteObject);
            _openWindowDataObjectCommand = new DelegateCommand(OpenWindowObjectData);
            _updateItemDataGridView = CreateCommandAsync(SetFieldPage);



            _timer = new System.Threading.Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite); 
        }

        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage);
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private List<ObjectOwnerModel> _objectList;
        public List<ObjectOwnerModel> ObjectList
        {
            get { return _objectList; }
            set { _objectList = value; OnPropertyChanged(nameof(ObjectList)); }
        }
        private List<ObjectOwnerDialogWindowModel> _objectListDialogWindow;
        public List<ObjectOwnerDialogWindowModel> ObjectListDialogWindow
        {
            get { return _objectListDialogWindow; }
            set { _objectListDialogWindow = value; OnPropertyChanged(nameof(ObjectListDialogWindow)); }
        }

        private Visibility _visibilitiDialogWindow;
        public Visibility VisibilitiDialogWindow
        {
            get { return _visibilitiDialogWindow; }
            set { _visibilitiDialogWindow = value; OnPropertyChanged(nameof(VisibilitiDialogWindow)); }
        }
        private Visibility _visibilitiFieldDialogWindow;
        public Visibility VisibilitiFieldDialogWindow
        {
            get { return _visibilitiFieldDialogWindow; }
            set { _visibilitiFieldDialogWindow = value; OnPropertyChanged(nameof(VisibilitiFieldDialogWindow)); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public ICommand UpdateItemDataGridView => _updateItemDataGridView;
        public ICommand OpenDialogWindowCommand => _openDialogWindowCommand;
        private void OpenDialogWindow()
        {
            if (ObjectListDialogWindow.Count != 0)
            {

                VisibilitiFieldDialogWindow = Visibility.Visible;
            }
            VisibilitiDialogWindow = Visibility.Visible;
        }
        public ICommand CloseDialogWindowCommand => _closeDialogWindowCommand;
        public void CloseDialogWindow()
        {
            VisibilitiDialogWindow = Visibility.Hidden;
            VisibilitiFieldDialogWindow = Visibility.Collapsed;
        }
        public ICommand OpenKeyCommand => _openKeyCommand;
        private async void OpenKey()
        {
            _openFileDialog.ShowDialog();


            List<ObjectOwnerDialogWindowModel> temp = new List<ObjectOwnerDialogWindowModel>();
            if (await _objectOwnerService.GetServerObjectOwner(_openFileDialog.FileName, Password))
            {
                VisibilitiFieldDialogWindow = Visibility.Visible;

                foreach (var item in _objectOwnerService.GetListObjectOwner())
                {
                    temp.Add(new ObjectOwnerDialogWindowModel(item));
                }

                ObjectListDialogWindow = new List<ObjectOwnerDialogWindowModel>(temp);

            }
        }
        public ICommand SaveObjectOwnerCommand => _saveObjectOwnerCommand;
        private void SaveObjectOwner()
        {
            if (_objectOwnerService.SaveDataBaseItem(ObjectListDialogWindow))
            {
                MessageBox.Show("Обєкти добавлені");
                ObjectListDialogWindow.Clear();
            }

            VisibilitiDialogWindow = Visibility.Hidden;
            VisibilitiFieldDialogWindow = Visibility.Collapsed;
            UpdateDataGridView();
        }
        public ICommand DeleteObjectCommand => _deleteObjectCommand;
        private async Task DeleteObject()
        {
            var item = ObjectList.ElementAt(SelectedItem);
            if (item != null)
            {
                if (await _objectOwnerService.DeleteItem(item.ToObjectOwner()))
                {
                    MessageBox.Show("Обєкт видалено");
                }
                else
                {
                    MessageBox.Show("Обєкт не вдалося видалити");
                }
                UpdateDataGridView();
            }
        }
        public ICommand OpenWindowDataObjectCommand => _openWindowDataObjectCommand;
        private void OpenWindowObjectData()
        {
            new ObjectOwnerShipData().ShowDialog();
        }


        private PaginatorViewModel _paginator;
        public PaginatorViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
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
                UpdateDataGridView();
            }
        }

        private List<string> _statusObjectOwner;
        public List<string> StatusObjectOwner
        {
            get { return _statusObjectOwner; }
            set { _statusObjectOwner = value; OnPropertyChanged(nameof(StatusObjectOwner)); }
        }

        private int _selectedStatusObjectOwner;
        public int SelectedStatusObjectOwner
        {
            get { return _selectedStatusObjectOwner; }
            set
            {
                _selectedStatusObjectOwner = value; OnPropertyChanged(nameof(SelectedStatusObjectOwner));
                UpdateDataGridView();
            }
        }
        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged(nameof(Heigth)); }
        }

        public async Task SetFieldPage()
        {
            SetFiledDialogWindow();
            SetComboBox();
            SetFielComboBoxTypeStatusProduct();
            await SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
        }

        private void SetFiledDialogWindow()
        {
            _visibilitiDialogWindow = Visibility.Hidden;
            VisibilitiDialogWindow = Visibility.Hidden;
            VisibilitiFieldDialogWindow = Visibility.Collapsed;
        }

        private void SetComboBox()
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

        private void SetFielComboBoxTypeStatusProduct()
        {
            if (StatusObjectOwner.Count == 0)
            {
                StatusObjectOwner.Add(TypeStatusObjectOwner.Unknown.ToString());
                StatusObjectOwner.Add(TypeStatusObjectOwner.Closed.ToString());
                StatusObjectOwner.Add(TypeStatusObjectOwner.Open.ToString());
            }
            SelectedStatusObjectOwner = 0;
        }


        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            var result = await _objectOwnerService.GetObjectOwnerPageColumn(page, countCoulmn, Enum.Parse<TypeStatusObjectOwner>(StatusObjectOwner.ElementAt(SelectedStatusObjectOwner)));
            if (reloadbutton)
            {
                if (result.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.CountButton = result.Pages;
                }
            }
            ObjectList = result.Data.ToObjectOwnerModel().ToList();
            _isReadyUpdateDataGriedView = true;
        }

        private void UpdateDataGridView(int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (ObjectList != null && ObjectList.Count > 0)
                {
                    ObjectList.Clear();
                }

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_nameSearch == string.Empty && _nameSearch == "")
                {
                    SetFieldDataGridView(countColumn, page, false);
                }
                else
                {
                    SearchByNameAndByBarCode(countColumn, page);
                }
            }
        }

        public ICommand SearchCommand { get => CreateCommandParameter<object>(SearchOperationRecorder); }

        private void SearchOperationRecorder(object parameter)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _nameSearch = parameter.ToString();

            _timer.Change(2000, Timeout.Infinite);// 2000 затримка в дві секунди для продовження ведення тексту
        }

        private void OnInputStopped(object state)
        {
            UpdateDataGridView();
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private async Task SearchByNameAndByBarCode(int countColumn, int page)
        {
            var result = (await _objectOwnerService.SearchByName(_nameSearch, page, countColumn, Enum.Parse<TypeStatusObjectOwner>(StatusObjectOwner.ElementAt(SelectedStatusObjectOwner))));
            if (!(Paginator.CountButton == result.Pages))
            {
                Paginator.CountButton = result.Pages;
            }
            ObjectList = result.Data.ToObjectOwnerModel().ToList();
        }
        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)System.Windows.Application.Current.MainWindow.ActualHeight - 340;
        }

        public ICommand UpdateUserDataGridView => _updateItemDataGridView; 
    }
}
