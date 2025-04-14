using ShopProject.Model.Command;
using ShopProject.Model.AdminPage;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using ShopProject.Views.AdminPage;
using ShopProject.Helpers.DataGridViewHelperModel;
using System.Threading.Tasks;
using System.Timers;
using ShopProjectDataBase.DataBase.Entities;

namespace ShopProject.ViewModel.AdminPage
{
    internal class ObjectOwnerShipViewModel : ViewModel<ObjectOwnerShipViewModel>
    {
        private ObjectOwnerShipModel _model;
        private OpenFileDialog _openFileDialog;

        private static System.Threading.Timer timer;
        private static string? _nameSearch;

        private ICommand _openDialogWindowCommand;
        private ICommand _closeDialogWindowCommand;
        private ICommand _openKeyCommand;
        private ICommand _saveObjectOwnerCommand;
        private ICommand _deleteObjectCommand;
        private ICommand _openWindowDataObjectCommand;
        private ICommand _updateItemDataGridView;

        public ObjectOwnerShipViewModel()
        {
            _model = new ObjectOwnerShipModel();
            _objectList = new List<ObjectOwnerEntity>();
            _visibilitiDialogWindow = string.Empty;
            _openFileDialog = new OpenFileDialog();
            _objectListDialogWindow = new List<ObjectOwnerHelpers>();

            _openDialogWindowCommand = new DelegateCommand(OpenDialogWindow);
            _closeDialogWindowCommand = new DelegateCommand(CloseDialogWindow);
            _openKeyCommand = new DelegateCommand(OpenKey);
            _saveObjectOwnerCommand = new DelegateCommand(SaveObjectOwner);
            _deleteObjectCommand = new DelegateCommand(DeleteObject);
            _openWindowDataObjectCommand = new DelegateCommand(OpenWindowObjectData);
            _updateItemDataGridView = new DelegateCommand(() => { SearchGoods(""); });



            timer = new System.Threading.Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);

            new Thread(new ThreadStart(SetFilewPage)).Start();
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }
        private List<ObjectOwnerEntity> _objectList;
        public List<ObjectOwnerEntity> ObjectList
        {
            get { return _objectList; }
            set { _objectList = value; OnPropertyChanged(nameof(ObjectList)); }
        }
        private List<ObjectOwnerHelpers> _objectListDialogWindow;
        public List<ObjectOwnerHelpers> ObjectListDialogWindow
        {
            get { return _objectListDialogWindow; }
            set { _objectListDialogWindow = value; OnPropertyChanged("ObjectListDialogWindow"); }
        }

        private string _visibilitiDialogWindow;
        public string VisibilitiDialogWindow
        {
            get { return _visibilitiDialogWindow; }
            set { _visibilitiDialogWindow = value; OnPropertyChanged("VisibilitiDialogWindow"); }
        }
        private string _visibilitiFieldDialogWindow;
        public string VisibilitiFieldDialogWindow
        {
            get { return _visibilitiFieldDialogWindow; }
            set { _visibilitiFieldDialogWindow = value; OnPropertyChanged(nameof(VisibilitiFieldDialogWindow)); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        public ICommand UpdateItemDataGridView => _updateItemDataGridView;
        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchGoods, CanRegister); }

        private void SearchGoods(object parameter)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            _nameSearch = parameter.ToString();

            timer.Change(2000, Timeout.Infinite);// 2000 затримка в дві секунди для продовження ведення тексту
        }
        private void OnInputStopped(object state)
        {
            UpdateDataGrid(_nameSearch);
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        private async void UpdateDataGrid(string parameter)
        {
            await Task.Run(() =>
            {
                _nameSearch = parameter.ToString();

                var result = _model.SearchObject(parameter.ToString());
                result.Reverse();

                if (ObjectList.Count != 0)
                {
                    ObjectList.Clear();
                }

                if (result.Count > 100)
                {
                    ObjectList = result.Take(100).ToList();//100 це кількість елементів на екрані 
                }
                else
                {
                    ObjectList = result;
                }
            });

        }



        private void SetFilewPage()
        {

            VisibilitiDialogWindow = "Hidden";
            VisibilitiFieldDialogWindow = "Collapsed";
            SearchGoods("");
        }

        public ICommand OpenDialogWindowCommand => _openDialogWindowCommand;
        private void OpenDialogWindow()
        {
            if (ObjectListDialogWindow.Count != 0)
            {

                VisibilitiFieldDialogWindow = "Visible";
            }
            VisibilitiDialogWindow = "Visible";
        }
        public ICommand CloseDialogWindowCommand => _closeDialogWindowCommand;
        public void CloseDialogWindow()
        {
            VisibilitiDialogWindow = "Hidden";
            VisibilitiFieldDialogWindow = "Collapsed";
        }
        public ICommand OpenKeyCommand => _openKeyCommand;
        private async void OpenKey()
        {
            _openFileDialog.ShowDialog();


            List<ObjectOwnerHelpers> temp = new List<ObjectOwnerHelpers>();
            if (await _model.GetServerObjectOwner(_openFileDialog.FileName, Password))
            {
                VisibilitiFieldDialogWindow = "Visible";

                foreach (var item in _model.GetListObjecyOwner())
                {
                    temp.Add(new ObjectOwnerHelpers(item));
                }

                ObjectListDialogWindow = new List<ObjectOwnerHelpers>(temp);

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

            VisibilitiDialogWindow = "Hidden";
            VisibilitiFieldDialogWindow = "Collapsed";
            SearchGoods("");
        }
        public ICommand DeleteObjectCommand => _deleteObjectCommand;
        private void DeleteObject()
        {
            var item = ObjectList.ElementAt(SelectedItem);
            if (item != null)
            {
                if (_model.deleteItemDataBase(item))
                {
                    MessageBox.Show("Обєкт видалено");
                    SearchGoods("");
                }
                else
                {
                    MessageBox.Show("Обєкт не вдалося видалити");
                }
            }
        }
        public ICommand OpenWindowDataObjectCommand => _openWindowDataObjectCommand;
        private void OpenWindowObjectData()
        {
            new ObjectOwnerShipData().ShowDialog();
        }


        private bool CanRegister(object parameter) => true;
    }
}
