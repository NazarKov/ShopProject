using ShopProject.Helpers;
using ShopProject.Helpers.Command;
using ShopProject.Model.AdminPage;
using ShopProject.Views.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows; 
using System.Threading.Tasks; 
using ShopProject.View.AdminPage.UserPage;
using ShopProject.ViewModel.TemplatePage; 
using ShopProject.Helpers.Template.Paginator;
using System.Collections; 
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using ShopProject.UIModel.UserPage;
using ShopProject.Helpers.Navigation;
using ShopProject.UIModel.OperationRecorderPage;

namespace ShopProject.ViewModel.AdminPage
{
    internal class UsersViewModel : ViewModel<UsersViewModel>
    {
        private ICommand _openWindowCreateUserCommand;
        private ICommand _opendUserDataCommand;
        private ICommand _deleteUserCommand;
        private ICommand _closeDialogWindowCommand;
        private ICommand _bindingObjectOwnerCommandl;
        private ICommand _saveBindingObjectOwnerCommand;
        private ICommand _updateSizeGridCommand;

        private ICommand _updateItemDataGridView;

        private static Timer? _timer; 
        private bool _isReadyUpdateDataGriedView;
        private static string? _nameSearch;


        private UsersModel _model;
        public UsersViewModel() 
        {
            _model = new UsersModel();
            _users = new List<User>();
            _paginator = new TemplatePaginatorButtonViewModel(); 
            _statusUsers = new List<string>();
            _countShowList = new List<string>();
            _nameSearch = string.Empty;

            _openWindowCreateUserCommand = new DelegateCommand(CreateUser);
            _opendUserDataCommand = new DelegateCommand(OpenUserData);
            _deleteUserCommand = new DelegateCommand(DeleteUser);
            _closeDialogWindowCommand = new DelegateCommand(CloseDialogWindow);
            _bindingObjectOwnerCommandl = new DelegateCommand(BindingObjectOwner);
            _saveBindingObjectOwnerCommand = new DelegateCommand(SaveBindingObjectOwner);
            _updateItemDataGridView = new DelegateCommand(() => { SetFieldPage(); });
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);

            _objectListDialogWindow = new List<OperationRecorderDialogWindow>();
            _timer = new System.Threading.Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite); 
            
            Paginator.Callback = UpdateDataGridView;

            SetFieldPage(); 
        }

        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(nameof(Users)); }
        }
        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }
        private List<OperationRecorderDialogWindow> _objectListDialogWindow;
        public List<OperationRecorderDialogWindow> ObjectListDialogWindow
        {
            get { return _objectListDialogWindow; }
            set { _objectListDialogWindow = value; OnPropertyChanged(nameof(ObjectListDialogWindow)); }
        }
        private Visibility _visibilityDialogWindow;
        public Visibility VisibilityDialogWindow
        {
            get => _visibilityDialogWindow;
            set { _visibilityDialogWindow = value; OnPropertyChanged(nameof(VisibilityDialogWindow)); }
        }
        public ICommand OpenWindowCreateUserCommand => _openWindowCreateUserCommand;
        private void CreateUser()
        {
            new CreateUserView().ShowDialog();
            SetFieldPage();
        }

        public ICommand OpenUserDateCommand => _opendUserDataCommand;
        private void OpenUserData()
        {
            Session.UserItem = Users.ElementAt(SelectedItem);
            new UserData().Show();
        }

        public ICommand UpdateUserCommand { get => new DelegateParameterCommand(UpdateUser, CanRegister); }
        private void UpdateUser(object parameter)
        {
            //var user = parameter as IList;

            //if (user != null) 
            //{
            //    if (user[0] != null)
            //    {
            //        Session.UserEntity = (UserEntity)user[0];
            //        new UpdateUserView().ShowDialog();
            //    }
            //}
        }

        public ICommand DeleteSelecteUserCommand => _deleteUserCommand;
        private void DeleteUser()
        {
            var user = _users.ElementAt(SelectedItem);
            if (user != null)
            {
                Task t = Task.Run(async () => {
                    if (await _model.DeleteUser(user))
                    {
                        MessageBox.Show("Користувача видалено");
                        SetFieldPage();
                    }
                });
            }
        }
        public ICommand CloseDialogWindowCommand => _closeDialogWindowCommand;
        public void CloseDialogWindow()
        {
              VisibilityDialogWindow = Visibility.Hidden;
        }
        public ICommand BindingObjectOwnerCommand => _bindingObjectOwnerCommandl;
        private void BindingObjectOwner()
        {

            Task.Run(async () =>
            {
                var items = await _model.GetAllObject();
                if (items != null)
                {
                    ObjectListDialogWindow = items;
                    VisibilityDialogWindow = Visibility.Visible;
                }
            });
        }
        public ICommand SaveBindingObjectOwnerCommand => _saveBindingObjectOwnerCommand;
        private void SaveBindingObjectOwner()
        {
            Task.Run(async () => { 
                if (await _model.SaveBinding(Users.ElementAt(SelectedItem), ObjectListDialogWindow))
                {
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

        private List<string> _statusUsers;
        public List<string> StatusUsers
        {
            get { return _statusUsers; }
            set { _statusUsers = value; OnPropertyChanged(nameof(StatusUsers)); }
        }

        private int _selectedStatusUser;
        public int SelectedStatusUser
        {
            get { return _selectedStatusUser; }
            set
            {
                _selectedStatusUser = value; OnPropertyChanged(nameof(SelectedStatusUser));
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
            SetComboBox();
            SetFieldDialogWindow();
            SetFielComboBoxTypeStatusProduct();
            SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
        }

        private void SetFieldDialogWindow()
        {
            _visibilityDialogWindow = Visibility.Collapsed;
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
            SelectedStatusUser = 0;
            if (StatusUsers.Count == 0)
            {
                StatusUsers.Add(TypeStatusUser.Unknown.ToString());
                StatusUsers.Add(TypeStatusUser.AvailableElectronicKey.ToString());
                StatusUsers.Add(TypeStatusUser.NotAvailableElectronicKey.ToString()); 
            }
        }


        private void SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            PaginatorData<User> result = new PaginatorData<User>();
            Task t = Task.Run(async () => {

                result = await _model.GetUsersPageColumn(page, countCoulmn, Enum.Parse<TypeStatusUser>(StatusUsers.ElementAt(SelectedStatusUser)));
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
                Paginator.CountColumn = countCoulmn;
                if (result.Data != null)
                {
                    Users = result.Data.ToList();
                }
                _isReadyUpdateDataGriedView = true;
            });
        }

        private void UpdateDataGridView(int countCoulmn, int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (Users != null && Users.Count > 0)
                {
                    Users.Clear();
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

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchUser, CanRegister); }

        private void SearchUser(object parameter)
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
            PaginatorData<User> result = new PaginatorData<User>();

            Task t = Task.Run(async () =>
            {
                if(_nameSearch !=null && _nameSearch != string.Empty)
                {
                    result = await _model.SearchByName(_nameSearch, page, countColumn, Enum.Parse<TypeStatusUser>(StatusUsers.ElementAt(SelectedStatusUser)));
                }

            });
            t.ContinueWith(t =>
            {
                if (!(Paginator.CountButton == result.Pages))
                {
                    Paginator.CountButton = result.Pages;
                }
                Paginator.CountColumn = countColumn;
                if (result.Data != null)
                {
                    Users = result.Data.ToList();
                }
            });
        }
        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 300;
        }

        public ICommand UpdateUserDataGridView => _updateItemDataGridView;
        private bool CanRegister(object parameter) => true;
    }
}
