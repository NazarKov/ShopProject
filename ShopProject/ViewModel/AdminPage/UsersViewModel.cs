using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.AdminPage;
using ShopProject.Views.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Threading.Tasks;
using ShopProjectDataBase.DataBase.Model;

namespace ShopProject.ViewModel.AdminPage
{
    internal class UsersViewModel : ViewModel<UsersViewModel>
    {
        private ICommand _openWindowCreateUserCommand;
        private ICommand _opendUserDateCommand;
        private ICommand _deleteSelectedUserCommand;
        private ICommand _closeDialogWindowCommand;
        private ICommand _bindingObjectOwnerCommandl;
        private ICommand _saveBindingObjectOwnerCommand;

        private ICommand _updateItemDataGridView;

        private static System.Threading.Timer timer;
        private static string? _nameSearch;


        private UsersModel _model;
        public UsersViewModel() 
        {
            _model = new UsersModel();
            _users = new List<UserEntity>();

            _openWindowCreateUserCommand = new DelegateCommand(CreateUser);
            _opendUserDateCommand = new DelegateCommand(OpenUserData);
            _deleteSelectedUserCommand = new DelegateCommand(DeleteUser);
            _closeDialogWindowCommand = new DelegateCommand(CloseDialogWindow);
            _bindingObjectOwnerCommandl = new DelegateCommand(BindingObjectOwner);
            _saveBindingObjectOwnerCommand = new DelegateCommand(SaveBindingObjectOwner);

            _objectListDialogWindow = new List<SoftwareDeviceSettlementOperationsHelper>();

            _updateItemDataGridView = new DelegateCommand(() => { SearchGoods(""); });

            timer = new System.Threading.Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);


            new Thread(new ThreadStart(SetFieldPage)).Start();
        }

        private List<UserEntity> _users;
        public List<UserEntity> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(nameof(Users)); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }
        private List<SoftwareDeviceSettlementOperationsHelper> _objectListDialogWindow;
        public List<SoftwareDeviceSettlementOperationsHelper> ObjectListDialogWindow
        {
            get { return _objectListDialogWindow; }
            set { _objectListDialogWindow = value; OnPropertyChanged("ObjectListDialogWindow"); }
        }
        private Visibility _visibilityDialogWindow;
        public Visibility VisibilityDialogWindow
        {
            get => _visibilityDialogWindow;
            set { _visibilityDialogWindow = value; OnPropertyChanged("VisibilityDialogWindow"); }
        }

        private void SetFieldPage()
        {
            _visibilityDialogWindow = Visibility.Collapsed;
            Users.Clear();
            Users = _model.GetUsers();
            Users.Reverse();
        }

        public ICommand OpenWindowCreateUserCommand => _openWindowCreateUserCommand;
        private void CreateUser()
        {
            new CreateUser().ShowDialog();
            SetFieldPage();
        }

        public ICommand OpenUserDateCommand => _opendUserDateCommand;
        private void OpenUserData()
        {
            Session.UserItem = Users.ElementAt(SelectedItem);
            new UserData().Show();
        }
        public ICommand DeleteSelecteUserCommand => _deleteSelectedUserCommand;
        private void DeleteUser()
        {
            //var user = _users.ElementAt(SelectedItem);
            //if(user != null)
            //{
            //    if(_model.DeleteUser(user))
            //    {
            //        MessageBox.Show("Користувача видалено");
            //        SetFieldPage();
            //    }
            //}
        }
        public ICommand CloseDialogWindowCommand => _closeDialogWindowCommand;
        public void CloseDialogWindow()
        {
              VisibilityDialogWindow = Visibility.Hidden;
        }
        public ICommand BindingObjectOwnerCommand => _bindingObjectOwnerCommandl;
        private void BindingObjectOwner()
        {
            var items = _model.GetAllObject();
            if (items != null)
            {

                ObjectListDialogWindow = items;
                VisibilityDialogWindow = Visibility.Visible;
            }
        }
        public ICommand SaveBindingObjectOwnerCommand => _saveBindingObjectOwnerCommand;
        private void SaveBindingObjectOwner()
        {
            if (_model.SaveBinding(Users.ElementAt(SelectedItem), ObjectListDialogWindow))
            {
                MessageBox.Show("Првиязка успішна");
                VisibilityDialogWindow = Visibility.Hidden;
            }

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
            //await Task.Run(() =>
            //{
            //    _nameSearch = parameter.ToString();

            //    var result = _model.SearchObject(parameter.ToString());
            //    result.Reverse();

            //    if (Users.Count != 0)
            //    {
            //        Users.Clear();
            //    }

            //    if (result.Count > 100)
            //    {
            //        Users = result.Take(100).ToList();//100 це кількість елементів на екрані 
            //    }
            //    else
            //    {
            //        Users = result;
            //    }
            //});

        }

        private bool CanRegister(object parameter) => true;
    }
}
