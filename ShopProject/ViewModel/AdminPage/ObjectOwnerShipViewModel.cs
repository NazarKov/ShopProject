using ShopProject.Model.Command;
using ShopProject.Model.AdminPage;
using System.Collections.Generic;
using System.Linq;
using System.Threading; 
using System.Windows.Input;
using ShopProject.Views.AdminPage;
using ShopProject.Helpers.DataGridViewHelperModel;
using System.Threading.Tasks;
using System.Timers; 
using ShopProject.ViewModel.TemplatePage; 
using ShopProject.Helpers.Template.Paginator;
using System;
using System.Windows; 
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProject.UIModel.ObjectOwnerPage;

namespace ShopProject.ViewModel.AdminPage
{
    internal class ObjectOwnerShipViewModel : ViewModel<ObjectOwnerShipViewModel>
    {
        private ObjectOwnerShipModel _model;
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

        public ObjectOwnerShipViewModel()
        {
            _model = new ObjectOwnerShipModel();
            _password = string.Empty;
            _paginator = new TemplatePaginatorButtonViewModel();
            _countShowList = new List<string>();
            _nameSearch = string.Empty;
            _statusObjectOwner = new List<string>();
            _objectList = new List<ObjectOwner>();
            _visibilitiDialogWindow = Visibility.Hidden;
            _openFileDialog = new System.Windows.Forms.OpenFileDialog();
            _objectListDialogWindow = new List<ObjectOwnerDialogWindow>();

            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);
            _openDialogWindowCommand = new DelegateCommand(OpenDialogWindow);
            _closeDialogWindowCommand = new DelegateCommand(CloseDialogWindow);
            _openKeyCommand = new DelegateCommand(OpenKey);
            _saveObjectOwnerCommand = new DelegateCommand(SaveObjectOwner);
            _deleteObjectCommand = new DelegateCommand(DeleteObject);
            _openWindowDataObjectCommand = new DelegateCommand(OpenWindowObjectData);
            _updateItemDataGridView = new DelegateCommand(() => { SetFieldPage(); });



            _timer = new System.Threading.Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);

            SetFieldPage();
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private List<ObjectOwner> _objectList;
        public List<ObjectOwner> ObjectList
        {
            get { return _objectList; }
            set { _objectList = value; OnPropertyChanged(nameof(ObjectList)); }
        }
        private List<ObjectOwnerDialogWindow> _objectListDialogWindow;
        public List<ObjectOwnerDialogWindow> ObjectListDialogWindow
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


            List<ObjectOwnerDialogWindow> temp = new List<ObjectOwnerDialogWindow>();
            if (await _model.GetServerObjectOwner(_openFileDialog.FileName, Password))
            {
                VisibilitiFieldDialogWindow = Visibility.Visible;

                foreach (var item in _model.GetListObjectOwner())
                {
                    temp.Add(new ObjectOwnerDialogWindow(item));
                }

                ObjectListDialogWindow = new List<ObjectOwnerDialogWindow>(temp);

            } 
        }
        public ICommand SaveObjectOwnerCommand => _saveObjectOwnerCommand;
        private void SaveObjectOwner()
        {
            if (_model.SaveDataBaseItem(ObjectListDialogWindow))
            {
                MessageBox.Show("Обєкти добавлені");
                ObjectListDialogWindow.Clear();
            }

            VisibilitiDialogWindow = Visibility.Hidden;
            VisibilitiFieldDialogWindow = Visibility.Collapsed;
            UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
        }
        public ICommand DeleteObjectCommand => _deleteObjectCommand;
        private void DeleteObject()
        {
            var item = ObjectList.ElementAt(SelectedItem);
            if (item != null)
            {
                Task t = Task.Run(async () => {
                    if (await _model.DeleteItem(item))
                    {
                        MessageBox.Show("Обєкт видалено");
                    }
                    else
                    {
                        MessageBox.Show("Обєкт не вдалося видалити");
                    }
                });
                t.ContinueWith(t => {
                    UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
                });
            }
        }
        public ICommand OpenWindowDataObjectCommand => _openWindowDataObjectCommand;
        private void OpenWindowObjectData()
        {
            new ObjectOwnerShipData().ShowDialog();
        }


        private TemplatePaginatorButtonViewModel _paginator;
        public TemplatePaginatorButtonViewModel Paginator
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
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
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
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            }
        }
        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged(nameof(Heigth)); }
        }

        public void SetFieldPage()
        {
            SetFiledDialogWindow();
            SetComboBox();
            SetFielComboBoxTypeStatusProduct();
            SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
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


        private void SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            PaginatorData<ObjectOwner> result = new PaginatorData<ObjectOwner>();
            Task t = Task.Run(async () => {

                result = await _model.GetObjectOwnerPageColumn(page, countCoulmn, Enum.Parse<TypeStatusObjectOwner>(StatusObjectOwner.ElementAt(SelectedStatusObjectOwner)));
            });
            t.ContinueWith(t => {
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
                Paginator.CountColumn = countCoulmn;
                ObjectList = result.Data.ToList();
                _isReadyUpdateDataGriedView = true;
            });
        }

        private void UpdateDataGridView(int countCoulmn, int page = 1)
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
                    SetFieldDataGridView(countCoulmn, page, false);
                }
                else
                {
                    SearchByNameAndByBarCode(countCoulmn, page);
                }
            }
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchOperationRecorder, CanRegister); }

        private void SearchOperationRecorder(object parameter)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _nameSearch = parameter.ToString();

            _timer.Change(2000, Timeout.Infinite);// 2000 затримка в дві секунди для продовження ведення тексту
        }

        private void OnInputStopped(object state)
        {
            UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void SearchByNameAndByBarCode(int countColumn, int page)
        {
            PaginatorData<ObjectOwner> result = new PaginatorData<ObjectOwner>();

            Task t = Task.Run(async () =>
            {
                result = await _model.SearchByName(_nameSearch, page, countColumn, Enum.Parse<TypeStatusObjectOwner>(StatusObjectOwner.ElementAt(SelectedStatusObjectOwner)));

            });
            t.ContinueWith(t =>
            {
                if (!(Paginator.CountButton == result.Pages))
                {
                    Paginator.CountButton = result.Pages;
                }
                Paginator.CountColumn = countColumn;
                ObjectList = result.Data.ToList();
            });
        }
        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)System.Windows.Application.Current.MainWindow.ActualHeight - 340;
        }

        public ICommand UpdateUserDataGridView => _updateItemDataGridView;
        private bool CanRegister(object parameter) => true;
    }
}
