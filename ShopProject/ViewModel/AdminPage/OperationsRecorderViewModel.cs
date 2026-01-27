using ShopProject.Helpers;
using ShopProject.Helpers.Command;
using ShopProject.Model.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows;
using MessageBox = System.Windows.MessageBox; 
using ShopProject.ViewModel.TemplatePage;  
using ShopProject.Helpers.Template.Paginator; 
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProject.UIModel.ObjectOwnerPage;
using ShopProject.View.UserPage;
using ShopProject.View.AdminPage.OperationRecorderPage;

namespace ShopProject.ViewModel.AdminPage
{
    internal class OperationsRecorderViewModel : ViewModel<OperationsRecorderViewModel>
    {
        private OperationsRecorderModel _model;
        private OpenFileDialog _openFileDialog;

        private ICommand _openDialogWindowCommand;
        private ICommand _closeDialogWindowCommand;
        private ICommand _openKeyCommand;
        private ICommand _saveObjectOwnerCommand;
        private ICommand _deleteObjectCommand;
        private ICommand _openWindowDataObjectCommand;
        private ICommand _bindingObjectOwnerCommandl;
        private ICommand _saveBindingObjectOwnerCommand;
        private ICommand _updateItemDataGridView;
        private ICommand _updateSizeGridCommand;

        private static System.Threading.Timer _timer; 
        private bool _isReadyUpdateDataGriedView;
        private static string _nameSearch;


        public OperationsRecorderViewModel()
        {
            _model = new OperationsRecorderModel();
            _objectList = new List<OperationRecorder>();
            _visibilitiDialogWindow = Visibility.Hidden;
            _openFileDialog = new OpenFileDialog();
            _objectListDialogWindow = new List<OperationRecorderDialogWindow>();
            _countShowList = new List<string>();
            _paginator = new TemplatePaginatorButtonViewModel();
            _password = string.Empty;
            _statusOperationRecorder = new List<string>();
            _selectedStatusOperationRecorder = 0;
            _nameSearch = string.Empty;

            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);
            _openDialogWindowCommand = new DelegateCommand(OpenDialogWindow);
            _closeDialogWindowCommand = new DelegateCommand(CloseDialogWindow);
            _openKeyCommand = new DelegateCommand(OpenKey);
            _saveObjectOwnerCommand = new DelegateCommand(SaveObjectOwner);
            _deleteObjectCommand = new DelegateCommand(DeleteObject);
            _openWindowDataObjectCommand = new DelegateCommand(OpenWindowObjectData);
            _bindingObjectOwnerCommandl = new DelegateCommand(BindingObjectOwner);
            _saveBindingObjectOwnerCommand = new DelegateCommand(SaveBindingObjectOwner);

            _updateItemDataGridView = new DelegateCommand(() => { SetFieldPage(); });
            _objectOwners = new List<ObjectOwnerDialogWindow>();


            _timer = new System.Threading.Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);

            SetFieldPage();
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        private List<OperationRecorder> _objectList;
        public List<OperationRecorder> ObjectList
        {
            get { return _objectList; }
            set { _objectList = value; OnPropertyChanged(nameof(ObjectList)); }
        }
        private List<OperationRecorderDialogWindow> _objectListDialogWindow;
        public List<OperationRecorderDialogWindow> ObjectListDialogWindow
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

        private List<ObjectOwnerDialogWindow> _objectOwners;
        public List<ObjectOwnerDialogWindow> ObjectOwners
        {
            get { return _objectOwners; }
            set { _objectOwners = value; OnPropertyChanged(nameof(ObjectOwners)); }
        }

        private Visibility _visibilityDialogWindow;
        public Visibility VisibilityDialogWindow
        {
            get => _visibilityDialogWindow;
            set { _visibilityDialogWindow = value; OnPropertyChanged(nameof(VisibilityDialogWindow)); }
        } 

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
            VisibilityDialogWindow = Visibility.Hidden;
        }
        public ICommand OpenKeyCommand => _openKeyCommand;
        private async void OpenKey()
        {
            _openFileDialog.ShowDialog();


            List<OperationRecorderDialogWindow> temp = new List<OperationRecorderDialogWindow>();
            if (await _model.GetServerSoftwareDeviceSettlementOperations(_openFileDialog.FileName, Password))
            {
                VisibilitiFieldDialogWindow = Visibility.Visible;

                foreach (var item in _model.GetListObjecyOwner())
                {
                    temp.Add(new OperationRecorderDialogWindow(item));
                }

                ObjectListDialogWindow = new List<OperationRecorderDialogWindow>(temp);

            }


        }

        public ICommand UpdateItemDataGridView => _updateItemDataGridView;
        public ICommand SaveObjectOwnerCommand => _saveObjectOwnerCommand;
        private void SaveObjectOwner()
        {
            Task t = Task.Run(async () => {
            
                if (await _model.SaveDataBaseItem(ObjectListDialogWindow))
                {
                    MessageBox.Show("Обєкти добавлені");
                    VisibilitiFieldDialogWindow = Visibility.Collapsed; 
                }

            });
            t.ContinueWith(t => { 
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
                _model.ClearListObjectOwner();
            });

        }
        public ICommand DeleteObjectCommand => _deleteObjectCommand;
        private void DeleteObject()
        {
            var item = ObjectList.ElementAt(SelectedItem);
            if (item != null)
            {
                Task t = Task.Run( async () => {
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
            new OperationRecorderDataView().ShowDialog();
        }
        public ICommand BindingObjectOwnerCommand => _bindingObjectOwnerCommandl;
        private void BindingObjectOwner()
        {
            Task t = Task.Run(async () => {
                ObjectOwners = await _model.GetAllObjectOwner();
            });
            t.ContinueWith(t => {
                VisibilityDialogWindow = Visibility.Visible;
            });
        }
        public ICommand SaveBindingObjectOwnerCommand => _saveBindingObjectOwnerCommand;
        private void SaveBindingObjectOwner()
        {
            Task t = Task.Run(async () => {
                if(await _model.SaveBinding(ObjectList.ElementAt(SelectedItem), ObjectOwners)){

                    MessageBox.Show("Првиязка успішна");
                    VisibilityDialogWindow = Visibility.Hidden;
                }
            }); 
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

        private List<string> _statusOperationRecorder;
        public List<string> StatusOperationRecorder
        {
            get { return _statusOperationRecorder; }
            set { _statusOperationRecorder = value; OnPropertyChanged(nameof(StatusOperationRecorder)); }
        }

        private int _selectedStatusOperationRecorder;
        public int SelectedStatusOperationRecorder
        {
            get { return _selectedStatusOperationRecorder; }
            set
            {
                _selectedStatusOperationRecorder = value; OnPropertyChanged(nameof(SelectedStatusOperationRecorder));
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
            _visibilityDialogWindow = Visibility.Hidden;
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
            if (StatusOperationRecorder.Count == 0)
            {
                StatusOperationRecorder.Add(TypeStatusOperationRecorder.Unknown.ToString());
                StatusOperationRecorder.Add(TypeStatusOperationRecorder.Closed.ToString());
                StatusOperationRecorder.Add(TypeStatusOperationRecorder.Open.ToString());
            }
            SelectedStatusOperationRecorder = 0;
        }


        private void SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            PaginatorData<OperationRecorder> result = new PaginatorData<OperationRecorder>();
            Task t = Task.Run(async () => {

                result = await _model.GetOperationsRecorderPageColumn(page, countCoulmn, Enum.Parse<TypeStatusOperationRecorder>(StatusOperationRecorder.ElementAt(SelectedStatusOperationRecorder)));
            });
            t.ContinueWith(t => {
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
            PaginatorData<OperationRecorder> result = new PaginatorData<OperationRecorder>();

            Task t = Task.Run(async () =>
            {
                result = await _model.SearchByName(_nameSearch, page, countColumn, Enum.Parse<TypeStatusOperationRecorder>(StatusOperationRecorder.ElementAt(SelectedStatusOperationRecorder)));

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
