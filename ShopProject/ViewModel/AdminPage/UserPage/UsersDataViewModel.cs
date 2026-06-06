using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm; 
using ShopProject.Infrastructure.CompositionRoot.Interface; 
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation; 
using ShopProject.Model.UI.User;
using ShopProject.Services.Infrastructure.Mediator; 
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Mapping.User;
using ShopProject.View.AdminPage.UserPage; 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.UserPage
{
    internal class UsersDataViewModel : ViewModel<UsersDataViewModel>, IViewModelLoadResourse
    {
        private ICommand _openWindowCreateUserCommand;   

        private ICommand _updateGridViewCommad; 
        private ICommand _searchItemCommand;

        private bool _isReadyUpdateDataGriedView;
        private bool _reloadField;

        private IUserService _userService;
        public UsersDataViewModel(IUserService userService)
        { 
            _userService = userService;

            _users = new List<UserModel>(); 
            _statusUsers = new List<string>();
            _countShowList = new List<string>();
            _searchItem = string.Empty; 
            _reloadField = false;
            _openWindowCreateUserCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateUserView, CreateUserViewModel>().ShowDialog(); _updateGridViewCommad?.Execute(false); });       

            _searchItemCommand = CreateCommandAsync(DebounceSearch);
            _updateGridViewCommad = CreateCommandAsync(async () => { _reloadField = false; SearchItem = string.Empty; SelectedStatusUser = 0; SelectIndexCountShowList = 0; await SetFieldPage(); });

            _paginator = new PaginatorViewModel();

            Paginator.Callback = async (int i) => { await UpdateDataGridView(i); };

            //_objectListDialogWindow = new List<OperationRecorderDialogWindow>();  
            _shadowVisibility = Visibility.Collapsed;

            MediatorService.AddEventAsync(NavigationButton.ReloadUser.ToString(), async () => { await SafeExecuteAsync(SetFieldPage); });
        }
        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage);
        }

        private List<UserModel> _users;
        public List<UserModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(nameof(Users)); }
        }

        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; OnPropertyChanged(nameof(SearchItem)); if (_reloadField) { SearchCommand.Execute(null); } }
        }

        private int _selectedItem;
        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }
        //private List<OperationRecorderDialogWindow> _objectListDialogWindow;
        //public List<OperationRecorderDialogWindow> ObjectListDialogWindow
        //{
        //    get { return _objectListDialogWindow; }
        //    set { _objectListDialogWindow = value; OnPropertyChanged(nameof(ObjectListDialogWindow)); }
        //}
        //private Visibility _visibilityDialogWindow;
        //public Visibility VisibilityDialogWindow
        //{
        //    get => _visibilityDialogWindow;
        //    set { _visibilityDialogWindow = value; OnPropertyChanged(nameof(VisibilityDialogWindow)); }
        //}

        private PaginatorViewModel _paginator;
        public PaginatorViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
        }

        private Visibility _shadowVisibility;
        public Visibility ShadowVisibility
        {
            get { return _shadowVisibility; }
            set { _shadowVisibility = value; OnPropertyChanged(nameof(ShadowVisibility)); }
        }
        private ICommand? _lostfocusCommand;
        public ICommand? LostFocusCommand
        {
            get { return _lostfocusCommand; }
            set { _lostfocusCommand = value; OnPropertyChanged(nameof(LostFocusCommand)); }
        } 

        public ICommand OpenWindowCreateUserCommand => _openWindowCreateUserCommand;

        //public ICommand OpenUserDateCommand => _opendUserDataCommand;
        //private void OpenUserData()
        //{
        //    //Session.UserItem = Users.ElementAt(SelectedItem);
        //    new UserData().Show();
        //}

        public ICommand UpdateUserCommand { get => CreateCommandParameter<object>(UpdateUser); }
        private void UpdateUser(object parameter)
        {
            var user = parameter as IList; 
            if (user != null)
            {
                if (user[0] != null)
                {
                    _userService.SetUpdateUserInSession(((UserModel)user[0]).ToUser());
                    App.Container.GetNewViewWithViewModel<UpdateUserView,UpdateUserViewModel>().ShowDialog();
                }
            }
        } 

        //public ICommand CloseDialogWindowCommand => _closeDialogWindowCommand;
        //public void CloseDialogWindow()
        //{
        //   // VisibilityDialogWindow = Visibility.Hidden;
        //}
        //public ICommand BindingObjectOwnerCommand => _bindingObjectOwnerCommandl;
        //private void BindingObjectOwner()
        //{

        //    //Task.Run(async () =>
        //    //{
        //    //    var items = await _model.GetAllObject();
        //    //    if (items != null)
        //    //    {
        //    //        ObjectListDialogWindow = items;
        //    //        VisibilityDialogWindow = Visibility.Visible;
        //    //    }
        //    //});
        //}
        //public ICommand SaveBindingObjectOwnerCommand => _saveBindingObjectOwnerCommand;
        //private void SaveBindingObjectOwner()
        //{
        //    //Task.Run(async () =>
        //    //{
        //    //    if (await _model.SaveBinding(Users.ElementAt(SelectedItem), ObjectListDialogWindow))
        //    //    {
        //    //        MessageBox.Show("Првиязка успішна");
        //    //        VisibilityDialogWindow = Visibility.Hidden;
        //    //    }
        //    //});
        //} 

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
                Task.Run(async () => { await UpdateDataGridView(); });
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
                Task.Run(async () => { await UpdateDataGridView(); });
            }
        } 

        public async Task SetFieldPage()
        {
            SetComboBox(); 
            SetFielComboBoxTypeStatusUser();
            await SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
            _reloadField = true;
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

        private void SetFielComboBoxTypeStatusUser()
        {
            SelectedStatusUser = 0;
            if (StatusUsers.Count == 0)
            {
                StatusUsers = UserStatusModel.GetUserStatusForStorage();
            }
        }


        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            var result = await _userService.GetPageColumn(page, countCoulmn, Enum.GetValues<TypeStatusUser>().ElementAt(SelectedStatusUser));
            if (result != null)
            {
                var paginator = result.Data;
                if (reloadbutton)
                {
                    if (paginator.Pages == 0)
                    {
                        Paginator.CountButton = 1;
                    }
                    else
                    {
                        Paginator.ReloadButton = true;
                        Paginator.CountButton = paginator.Pages;
                    }
                }
                if (result.Data == null)
                {
                    throw new Exception("Невдалося завантажити одиниці");
                }
                Users = new List<UserModel>(paginator.Data.ToUserModel());
                _isReadyUpdateDataGriedView = true;
            }
            else if (result.IsError)
            {
                Users = new List<UserModel>();
                Paginator.CountButton = 0;
            }
        }

        private async Task UpdateDataGridView(int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (Users != null && Users.Count > 0)
                {
                    Users.Clear();
                }

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (string.IsNullOrEmpty(SearchItem))
                {
                    await SetFieldDataGridView(countColumn, page, false);
                }
                else
                {
                   await SearchByNameAndByBarCode(countColumn, page);
                }
            }
        }

        public ICommand SearchCommand => _searchItemCommand;


        private CancellationTokenSource? _searchCts;
        private async Task DebounceSearch()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();

            try
            {
                await Task.Delay(1000, _searchCts.Token);
                await SafeExecuteAsync(async () =>
                {
                    await UpdateDataGridView();
                });
            }
            catch (TaskCanceledException) { }
        }

        private async Task SearchByNameAndByBarCode(int countColumn, int page)
        { 
            var result = await _userService.SearchByName(SearchItem, page, countColumn, Enum.GetValues<TypeStatusUser>().ElementAt(SelectedStatusUser));

            if (result.IsSuccess)
            {
                var paginator = result.Data;

                if (paginator.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {
                    Paginator.CountButton = paginator.Pages;
                }
                Users = new List<UserModel>(paginator.Data.ToUserModel());
            }
            else if (result.IsError)
            {
                Users = new List<UserModel>();
                Paginator.CountButton = 0;
            }
        }
        public ICommand UpdateFieldPageCommand => _updateGridViewCommad; 
    }
}
