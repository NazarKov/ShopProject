using ShopProject.Core.Mvvm;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.Operation;
using ShopProject.Model.UI.WorkingShift;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface;
using ShopProject.Services.Modules.Domain.PoinOfSale.SaleMenu.Interface;
using ShopProject.Services.Modules.Domain.User.Interface;
using ShopProject.Services.Modules.Mapping.Operation;
using ShopProject.Services.Modules.Mapping.WorkingShift;
using ShopProject.View.UserPage.PointOfSale.SaleMenu;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage.PointOfSale.SaleMenu
{
    internal class WorkShiftMenuViewModel : ViewModel<WorkShiftMenuViewModel>, IViewModelLoadResourse
    { 
        private ICommand _openShiftCommand;
        private ICommand _openOpenShiftDialogWindowCommand;
        private ICommand _closeOpenShiftDialogWindowCommand;
        private ICommand _closeOpenShiftSuccessDialogWindowCommand;

        private ICommand _openNewCheckCommand;
        private ICommand _closeFisclaCheckSuccessDialogWindowCommand; 
        private ICommand _openOfficialDepositMoneyDialogWindowCommad;
        private ICommand _closeOfficialDepositMoneyDialogWindowCommad;
        private ICommand _officialDepositMoneyCommand;
        private ICommand _closeSuccessOfficialDepositMoneyDialogWindowCommad;

        private ICommand _openOfficialWithdrawalMoneyDialogWindowCommad;
        private ICommand _closeOfficialWithdrawalMoneyDialogWindowCommad;
        private ICommand _officialWithdrawalMoneyCommand;
        private ICommand _closeSuccessOfficialWithdrawalMoneyDialogWindowCommad;
        
        private ICommand _closeShiftCommand;
        private ICommand _openCloseShiftDialogWindowCommand;
        private ICommand _closeCloseShiftDialogWindowCommand;
        private ICommand _closeCloseShiftSuccessDialogWindowCommand;

        private ICommand _exitWorkShiftMenuCommand;
        private ICommand _printLastCheckCommand;
        private ICommand _publishCertificateCommand;
         
        private IWorkingShiftService _workingShiftService; 
         
        public WorkShiftMenuViewModel(IUserService userServise, IWorkingShiftService workingShiftService, IOperationRecorderService operationRecorderServise)
        {
            _userName = userServise.GetUserFromSession().FullName;
            _workingShiftService = workingShiftService;

            _openShiftCommand = CreateCommandAsync(OpenShift);
            _openOpenShiftDialogWindowCommand = CreateCommandAsync(async () => {  Cash = 0; VisibilitiOpenShiftDialogWindow = Visibility.Visible;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetVisible"); VisibilitiShadowPage = Visibility.Visible; });
            _closeOpenShiftDialogWindowCommand = CreateCommandAsync(async () => { VisibilitiOpenShiftDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed; });
            _closeOpenShiftSuccessDialogWindowCommand = CreateCommandAsync(async () => { VisibilitiOpenShiftSuccessDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed; });

            _openOfficialDepositMoneyDialogWindowCommad = CreateCommandAsync(async () => { Cash = 0; VisibilitiOfficialDepositMoneyDialogWindow = Visibility.Visible;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetVisible"); VisibilitiShadowPage = Visibility.Visible; });
            _closeOfficialDepositMoneyDialogWindowCommad = CreateCommandAsync(async () => { VisibilitiOfficialDepositMoneyDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed; });
            _officialDepositMoneyCommand = CreateCommandAsync(OfficialDepositMoney);
            _closeSuccessOfficialDepositMoneyDialogWindowCommad = CreateCommandAsync(async () => { VisibilitiSuccessOfficialDepositMoneyDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed; });

            _openOfficialWithdrawalMoneyDialogWindowCommad = CreateCommandAsync(async () => {
                Cash = 0; VisibilitiOfficialWithdrawalMoneyDialogWindow = Visibility.Visible;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetVisible"); VisibilitiShadowPage = Visibility.Visible;
            });
            _closeOfficialWithdrawalMoneyDialogWindowCommad = CreateCommandAsync(async () => {
                VisibilitiOfficialWithdrawalMoneyDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed;
            });
            _officialWithdrawalMoneyCommand = CreateCommandAsync(OfficialWithdrawalMoney);
            _closeSuccessOfficialWithdrawalMoneyDialogWindowCommad = CreateCommandAsync(async () => {
                VisibilitiSuccessOfficialWithdrawalMoneyDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed;
            });

            _closeShiftCommand = CreateCommandAsync(CloseShift);
            _openCloseShiftDialogWindowCommand = CreateCommandAsync(async () => {
                Cash = 0; VisibilitiCloseShiftDialogWindow = Visibility.Visible;
                OperationsInfo = (await _workingShiftService.GetOperationInfo(_workingShiftStatus.WorkingShift.ID)).ToOperationInfoModel();
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetVisible"); VisibilitiShadowPage = Visibility.Visible;
            });
            _closeCloseShiftDialogWindowCommand = CreateCommandAsync(async () => {
                VisibilitiCloseShiftDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed;
            });
            _closeCloseShiftSuccessDialogWindowCommand = CreateCommandAsync(async () => {
                VisibilitiCloseShiftSuccessDialogWindow = Visibility.Collapsed;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); VisibilitiShadowPage = Visibility.Collapsed;
            }); 
            _openNewCheckCommand = CreateCommand(OpenCheck);
            _closeFisclaCheckSuccessDialogWindowCommand = CreateCommandAsync(async () => { await MediatorService.ExecuteEventAsync("VisibilitiShadowSetHidden"); 
                VisibilitiFiscalCheckSuccessdialogWindow = Visibility.Collapsed; 
                VisibilitiShadowPage = Visibility.Collapsed;
                Operation = new OperationModel();
            });
            MediatorService.AddEventAsync("FiscalCheckSuccess", async () => {
                VisibilitiFiscalCheckSuccessdialogWindow = Visibility.Visible;
                VisibilitiShadowPage = Visibility.Visible;
                await MediatorService.ExecuteEventAsync("VisibilitiShadowSetVisible");
                Operation = _workingShiftService.GetOperationSession().ToOperationModel();
            });


            _exitWorkShiftMenuCommand = CreateCommand(ExitWorkShiftMenu);

            _printLastCheckCommand = CreateCommandAsync(PrintLastCheck);
            _publishCertificateCommand = CreateCommand(PublishCertificate);
             
            _tabs = new ObservableCollection<TabItem>(); 
            _isEnableCloseShiftButton = true;
            _isEnableOpenShiftButton = true;
            _visibilitiOpenShiftDialogWindow = Visibility.Collapsed;
            _visibilitiOpenShiftSuccessDialogWindow = Visibility.Collapsed;
            _visibilitiShadowPage = Visibility.Collapsed;
            _visibilitiOpenShift = Visibility.Visible;
            _visibilitiCloseShift = Visibility.Visible;
            _visibilitiExitButton = Visibility.Visible;
            _workingShiftStatus = new WorkingShiftDataModel();

            _visibilitiOfficialDepositMoneyDialogWindow = Visibility.Collapsed;
            _visibilitiSuccessOfficialDepositMoneyDialogWindow = Visibility.Collapsed;
            _visibilitiOfficialWithdrawalMoneyDialogWindow = Visibility.Collapsed;
            _visibilitiSuccessOfficialWithdrawalMoneyDialogWindow = Visibility.Collapsed;

            _visibilitiCloseShiftDialogWindow = Visibility.Collapsed;
            _visibilitiCloseShiftSuccessDialogWindow = Visibility.Collapsed;
            _visibilitiFiscalCheckSuccessdialogWindow = Visibility.Collapsed;
            _operation = new OperationModel();
            _operationsInfo = new OperationsInfoModel();
            Cash = 0;
        }
        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage);
        }
        private WorkingShiftDataModel _workingShiftStatus;
        public WorkingShiftDataModel WorkingShiftStatus
        {
            get { return _workingShiftStatus; } 
            set { _workingShiftStatus = value;OnPropertyChanged(nameof(WorkingShiftStatus)); } 
        }
        private OperationModel _operation;
        public OperationModel Operation
        {
            get { return _operation; }
            set { _operation = value;OnPropertyChanged(nameof(Operation));}
        }

        private OperationsInfoModel _operationsInfo;
        public OperationsInfoModel OperationsInfo
        {
            get { return _operationsInfo; }
            set { _operationsInfo = value;OnPropertyChanged(nameof(OperationsInfo)); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        private ObservableCollection<TabItem> _tabs;
        public ObservableCollection<TabItem> Tabs
        {
            get { return _tabs; }
            set { _tabs = value; OnPropertyChanged(nameof(Tabs)); }
        } 

        private int _selectedTabItem;
        public int SelectedTabItem
        {
            get { return _selectedTabItem; }
            set { _selectedTabItem = value; OnPropertyChanged(nameof(SelectedTabItem)); }
        } 
        private decimal _cash;
        public decimal Cash
        {
            get { return _cash; }
            set { _cash = value; OnPropertyChanged(nameof(Cash)); }
        } 
        private bool _isEnableOpenShiftButton;
        public bool IsEnableOpenShiftButton
        {
            get { return _isEnableOpenShiftButton; }
            set { _isEnableOpenShiftButton = value; OnPropertyChanged(nameof(IsEnableOpenShiftButton)); }
        }
        private bool _isEnableCloseShiftButton;
        public bool IsEnableCloseShiftButton
        {
            get { return _isEnableCloseShiftButton; }
            set { _isEnableCloseShiftButton = value; OnPropertyChanged(nameof(IsEnableCloseShiftButton)); }
        }

        private Visibility _visibilitiShadowPage;
        public Visibility VisibilitiShadowPage
        {
            get { return _visibilitiShadowPage; }
            set { _visibilitiShadowPage = value; OnPropertyChanged(nameof(VisibilitiShadowPage)); }
        }

        private Visibility _visibilitiOpenShift;
        public Visibility VisibilitiOpenShift
        {
            get { return _visibilitiOpenShift; }
            set { _visibilitiOpenShift = value; OnPropertyChanged(nameof(VisibilitiOpenShift)); }
        }
        private Visibility _visibilitiCloseShift;
        public Visibility VisibilitiCloseShift
        {
            get { return _visibilitiCloseShift; }
            set { _visibilitiCloseShift = value; OnPropertyChanged(nameof(VisibilitiCloseShift)); }
        } 
        private Visibility _visibilitiExitButton;
        public Visibility VisibilitiExitButton
        {
            get { return _visibilitiExitButton; }
            set { _visibilitiExitButton = value; OnPropertyChanged(nameof(VisibilitiExitButton)); }
        } 
        private Visibility _visibilitiOpenShiftDialogWindow;
        public Visibility VisibilitiOpenShiftDialogWindow
        {
            get { return _visibilitiOpenShiftDialogWindow; }
            set { _visibilitiOpenShiftDialogWindow = value; OnPropertyChanged(nameof(VisibilitiOpenShiftDialogWindow)); }
        }

        private Visibility _visibilitiOpenShiftSuccessDialogWindow;
        public Visibility VisibilitiOpenShiftSuccessDialogWindow
        {
            get { return _visibilitiOpenShiftSuccessDialogWindow; }
            set { _visibilitiOpenShiftSuccessDialogWindow = value; OnPropertyChanged(nameof(VisibilitiOpenShiftSuccessDialogWindow)); }
        } 

        private Visibility _visibilitiOfficialDepositMoneyDialogWindow;
        public Visibility VisibilitiOfficialDepositMoneyDialogWindow
        {
            get { return _visibilitiOfficialDepositMoneyDialogWindow; }
            set { _visibilitiOfficialDepositMoneyDialogWindow = value; OnPropertyChanged(nameof(VisibilitiOfficialDepositMoneyDialogWindow)); }
        }

        private Visibility _visibilitiSuccessOfficialDepositMoneyDialogWindow;
        public Visibility VisibilitiSuccessOfficialDepositMoneyDialogWindow
        {
            get { return _visibilitiSuccessOfficialDepositMoneyDialogWindow; }
            set { _visibilitiSuccessOfficialDepositMoneyDialogWindow = value; OnPropertyChanged(nameof(VisibilitiSuccessOfficialDepositMoneyDialogWindow)); }
        }


        private Visibility _visibilitiOfficialWithdrawalMoneyDialogWindow;
        public Visibility VisibilitiOfficialWithdrawalMoneyDialogWindow
        {
            get { return _visibilitiOfficialWithdrawalMoneyDialogWindow; }
            set { _visibilitiOfficialWithdrawalMoneyDialogWindow = value; OnPropertyChanged(nameof(VisibilitiOfficialWithdrawalMoneyDialogWindow)); }
        }

        private Visibility _visibilitiSuccessOfficialWithdrawalMoneyDialogWindow;
        public Visibility VisibilitiSuccessOfficialWithdrawalMoneyDialogWindow
        {
            get { return _visibilitiSuccessOfficialWithdrawalMoneyDialogWindow; }
            set { _visibilitiSuccessOfficialWithdrawalMoneyDialogWindow = value; OnPropertyChanged(nameof(VisibilitiSuccessOfficialWithdrawalMoneyDialogWindow)); }
        }

        private Visibility _visibilitiCloseShiftDialogWindow;
        public Visibility VisibilitiCloseShiftDialogWindow
        {
            get { return _visibilitiCloseShiftDialogWindow; }
            set { _visibilitiCloseShiftDialogWindow = value; OnPropertyChanged(nameof(VisibilitiCloseShiftDialogWindow)); }
        }

        private Visibility _visibilitiCloseShiftSuccessDialogWindow;
        public Visibility VisibilitiCloseShiftSuccessDialogWindow
        {
            get { return _visibilitiCloseShiftSuccessDialogWindow; }
            set { _visibilitiCloseShiftSuccessDialogWindow = value; OnPropertyChanged(nameof(VisibilitiCloseShiftSuccessDialogWindow)); }
        }

        private Visibility _visibilitiFiscalCheckSuccessdialogWindow;
        public Visibility VisibilitiFiscalCheckSuccessdialogWindow
        {
            get { return _visibilitiFiscalCheckSuccessdialogWindow; }
            set { _visibilitiFiscalCheckSuccessdialogWindow = value;OnPropertyChanged(nameof(VisibilitiFiscalCheckSuccessdialogWindow)); }
        }

        private async Task SetFieldPage()
        { 
            SetTabsField();
            await SetHeaderLabelField();
            if(WorkingShiftStatus.Status == TypeStatusShift.Open)
            {
                VisibilitiOpenShift = Visibility.Collapsed;
                VisibilitiExitButton = Visibility.Collapsed;
            }
            else if(WorkingShiftStatus.Status == TypeStatusShift.Close)
            {
                VisibilitiCloseShift = Visibility.Collapsed;
            }

        }
        private void SetTabsField()
        {
            if (Tabs.Count == 0)
            {
                Tabs.Add(new TabItem()
                {
                    Header = "Чек № 1",
                    Content = new Frame() { Content = App.Container.GetViewWithViewModel<SaleMenuView, SaleMenuViewModel>() }
                });
                SelectedTabItem = 0;
            }
        }


        private async Task SetHeaderLabelField()
        {  
            var workingShiftDataModel = _workingShiftService.GetWorkingShiftStatusFromSession().ToWorkingShiftData();
            workingShiftDataModel.IsTestMode = _workingShiftService.IsTestMode();
            WorkingShiftStatus = new WorkingShiftDataModel(workingShiftDataModel);
            OnPropertyChanged(nameof(WorkingShiftStatus));
        }

        private async Task ChangeHeaderLable(bool isOnline = false)
        {
            await SetHeaderLabelField();
            if (isOnline)
            {
                WorkingShiftStatus.OpenShiftTime = DateTime.Now;
            }
            else
            {
                WorkingShiftStatus.OpenShiftTime = null;
            }
            OnPropertyChanged(nameof(WorkingShiftStatus));
        }

        public ICommand OpenOpenShiftDialogWindowCommand => _openOpenShiftDialogWindowCommand;
        public ICommand CloseOpenShiftDialogWindowCommand => _closeOpenShiftDialogWindowCommand;
        public ICommand CloseOpenShiftSuccessDialogWindowCommand => _closeOpenShiftSuccessDialogWindowCommand;
        public ICommand OpenShiftCommand => _openShiftCommand;
        private async Task OpenShift()
        {
            IsEnableOpenShiftButton = false;
            var result = await _workingShiftService.OpenShift();

            if (result.IsSuccess)
            {  
                await ChangeHeaderLable(true);

                if (Cash != 0)
                {
                    await _workingShiftService.DepositAndWithdrawalMoney(Cash, TypeOperation.DepositMoney);
                }

                VisibilitiOpenShift = Visibility.Collapsed;
                VisibilitiExitButton = Visibility.Collapsed;
                VisibilitiCloseShift = Visibility.Visible;
                VisibilitiOpenShiftDialogWindow = Visibility.Collapsed;
                VisibilitiOpenShiftSuccessDialogWindow = Visibility.Visible;

               
            }
            else
            {
                MessageBox.Show(result.ErrorMessage); 
            }
            IsEnableOpenShiftButton = true;
        }

        public ICommand OpenCloseShiftDialogWindowCommand => _openCloseShiftDialogWindowCommand;
        public ICommand CloseCloseShiftDialogWindowCommand => _closeCloseShiftDialogWindowCommand;
        public ICommand CloseCloseShiftSuccessDialogWindowCommand => _closeCloseShiftSuccessDialogWindowCommand;  
        public ICommand CloseShiftCommand => _closeShiftCommand;
        private async Task CloseShift()
        {
            IsEnableCloseShiftButton = false;
            var result = await _workingShiftService.CloseShift();

            if (result.IsSuccess)
            { 
                VisibilitiOpenShift = Visibility.Visible;
                VisibilitiExitButton = Visibility.Visible;
                VisibilitiCloseShiftDialogWindow = Visibility.Collapsed;
                VisibilitiCloseShiftSuccessDialogWindow = Visibility.Visible;
                await ChangeHeaderLable();  
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            } 
            IsEnableCloseShiftButton = true; 
        }

        public ICommand OpenNewCheck => _openNewCheckCommand; 
        private void OpenCheck()
        {
            int maxCount = 15;
            TabItem newTabItem = new TabItem();
            int count = Tabs.Count+1;

            if (count <= maxCount)
            {

                newTabItem.Header = "Чек №" + count;
                newTabItem.TabIndex = count;
                newTabItem.Content = new Frame() { Content = App.Container.GetNewViewWithViewModel<SaleMenuView, SaleMenuViewModel>() };

                Tabs.Add(newTabItem); 
                OnPropertyChanged(nameof(Tabs));

            }
            if (Tabs.IndexOf(Tabs.Where(item => item.IsSelected == true).FirstOrDefault()) == maxCount)
            {
                SelectedTabItem = Tabs.IndexOf(Tabs.ElementAt(0));
            }
            else
            {
                var tab = Tabs.Where(item => item.IsSelected == true).FirstOrDefault();
                SelectedTabItem = Tabs.IndexOf(tab) + 1;
            }
        }
        public ICommand OpenOfficialDepositMoneyDialogWindowCommad => _openOfficialDepositMoneyDialogWindowCommad;
        public ICommand CloseOfficialDepositMoneyDialogWindowCommad => _closeOfficialDepositMoneyDialogWindowCommad;
        public ICommand CloseSuccessOfficialDepositMoneyDialogWindowCommad => _closeSuccessOfficialDepositMoneyDialogWindowCommad;
        public ICommand OfficialDepositMoneyCommand => _officialDepositMoneyCommand;
        private async Task OfficialDepositMoney()
        {
            var result = await _workingShiftService.DepositAndWithdrawalMoney(Cash, TypeOperation.DepositMoney);

            if (result.IsSuccess)
            {
                VisibilitiOfficialDepositMoneyDialogWindow = Visibility.Collapsed;
                VisibilitiSuccessOfficialDepositMoneyDialogWindow = Visibility.Visible; 
            }
            else
            {
                MessageBox.Show("Невдалося внести кошти:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information); 
            } 
        }

        public ICommand OpenOfficialWithdrawalMoneyDialogWindowCommad => _openOfficialWithdrawalMoneyDialogWindowCommad;
        public ICommand CloseOfficialWithdrawalMoneyDialogWindowCommad => _closeOfficialWithdrawalMoneyDialogWindowCommad;
        public ICommand CloseSuccessOfficialWithdrawalMoneyDialogWindowCommad => _closeSuccessOfficialWithdrawalMoneyDialogWindowCommad;
        public ICommand OfficialWithdrawalMoneyCommand => _officialWithdrawalMoneyCommand;

        private async Task OfficialWithdrawalMoney()
        {
            var result = await _workingShiftService.DepositAndWithdrawalMoney(Cash, TypeOperation.WithdrawalMoney);

            if (result.IsSuccess)
            {
                VisibilitiOfficialWithdrawalMoneyDialogWindow = Visibility.Collapsed;
                VisibilitiSuccessOfficialWithdrawalMoneyDialogWindow = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Невдалося видати кошти:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public ICommand CloseFisclaCheckSuccessDialogWindowCommand => _closeFisclaCheckSuccessDialogWindowCommand;

        public ICommand ExitWorkShiftMenuCommand => _exitWorkShiftMenuCommand;
        private void ExitWorkShiftMenu()
        {
            MediatorService.ExecuteNavigation(NavigationButton.RedirectToAssignedPointsOfSalePage);
        }

        public ICommand PrintLastCheckCommand => _printLastCheckCommand;
        private async Task PrintLastCheck()
        {
           // await _workingShiftService.PrintLastCheck();
        }

        public ICommand PublishCertificateCommand => _publishCertificateCommand;
        private void PublishCertificate()
        {

        }
    }
}
