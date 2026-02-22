using ShopProject.Helpers;
using ShopProject.Helpers.Command;
using ShopProject.Helpers.Navigation; 
using ShopProject.Model.SalePage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.SettingPage;
using ShopProject.UIModel.UserPage;
using ShopProject.Views.SalePage; 
using ShopProjectDataBase.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq; 
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FiscalServerApi.ExceptionServer;

namespace ShopProject.ViewModel.SalePage
{
    internal class WorkShiftMenuViewModel : ViewModel<WorkShiftMenuViewModel>
    {
        private WorkShiftMenuModel _model;

        private ICommand _openShiftCommand;
        private ICommand _closeShiftCommand;

        private ICommand _updateSizeCommand;
        private ICommand _openNewCheckCommand; 

        private ICommand _officialDepositMoneyCommand;
        private ICommand _officialReceivedMoneyCommand;

        private ICommand _okFirstDialogsWindowCommand;
        private ICommand _cancelFirstDialogsWindowCommand;

        private ICommand _okSecondDialogsWindowCommand;
        private ICommand _cancelSecondDialogsWindowCommand;

        private ICommand _exitWorkShiftMenuCommand;
        private ICommand _printLastCheckCommand;
        private ICommand _publishCertificateCommand;

        private User _user;
        private OperationRecorder _operationsRecorder;

        public WorkShiftMenuViewModel ()
        {
            _model = new WorkShiftMenuModel ();

            _openShiftCommand = new DelegateCommandAsync(OpenShift);
            _closeShiftCommand = new DelegateCommandAsync(CloseShift);
            _updateSizeCommand = new DelegateCommand(UpdateSizes);
            _openNewCheckCommand = new DelegateCommand(OpenCheck); 


            _officialDepositMoneyCommand = new DelegateCommand(OfficialDepositMoney);
            _officialReceivedMoneyCommand = new DelegateCommand(OfficialIssuanceModey);

            _okFirstDialogsWindowCommand = new DelegateCommandAsync(OkFirstDialogWindow);
            _cancelFirstDialogsWindowCommand = new DelegateCommand(CancelFirstDialogWindow);


            _okSecondDialogsWindowCommand = new DelegateCommandAsync(OkSeconDialogWindow);
            _cancelSecondDialogsWindowCommand = new DelegateCommand(CancelSecondDialogWindow);
            _exitWorkShiftMenuCommand = new DelegateCommand(ExitWorkShiftMenu);
            _printLastCheckCommand = new DelegateCommandAsync(PrintLastCheck);
            _publishCertificateCommand = new DelegateCommand(PublishCertificate);

            _statusShift = string.Empty;
            _statusColor = string.Empty;
            _tabs = new ObservableCollection<TabItem>();
            _user = new User();
            _operationsRecorder = new OperationRecorder(); 
            _fnNumber = string.Empty;
            _economicUnit = string.Empty;
            _seller = string.Empty;
            _statusOnline = string.Empty;
            _isEnableCloseShiftButton = true;
            _isEnableOpenShiftButton = true;
            _visibilitiOpenShift = Visibility.Visible;

            VisibleFirstDialogWindow = Visibility.Hidden;
            VisibleSecondDialogWindow = Visibility.Hidden;
            VisibilitiExitButton = Visibility.Visible;
            Cash = 0;
            SetFieldPage();
        }

        private string _statusShift;
        public string StatusShift
        {
            get { return _statusShift; }
            set { _statusShift = value; OnPropertyChanged(nameof(StatusShift)); }
        }

        private string _statusColor;
        public string StatusColor
        {
            get { return _statusColor; }
            set { _statusColor = value; OnPropertyChanged(StatusColor); }
        }

        private string _fnNumber;
        public string FNumber
        {
            get { return _fnNumber; }
            set { _fnNumber = value;OnPropertyChanged(nameof(FNumber)); }
        }

        private string _economicUnit;
        public string EconomicUnit
        {
            get { return _economicUnit; }
            set { _economicUnit = value; OnPropertyChanged(nameof(EconomicUnit)); }
        }

        private string _seller;
        public string Seller
        {
            get { return _seller; }
            set { _seller = value; OnPropertyChanged(nameof(Seller)); }
        }

        private string _statusOnline;
        public string StatusOnline
        {
            get { return _statusOnline; }
            set { _statusOnline = value; OnPropertyChanged(nameof(StatusOnline)); }
        }

        private int _widght;
        public int Widght
        {
            get { return _widght; }
            set { _widght = value; OnPropertyChanged(nameof(Widght)); }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged(nameof(Height)); }
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
            set { _selectedTabItem = value;OnPropertyChanged(nameof(SelectedTabItem)); }
        }

        private Visibility _visibleFirstDialogWindow;
        public Visibility VisibleFirstDialogWindow
        {
            get { return _visibleFirstDialogWindow; }
            set { _visibleFirstDialogWindow = value; OnPropertyChanged(nameof(VisibleFirstDialogWindow)); }
        }

        private Visibility _visibleSecondDialogWindow;
        public Visibility VisibleSecondDialogWindow
        {
            get { return _visibleSecondDialogWindow; }
            set { _visibleSecondDialogWindow = value; OnPropertyChanged(nameof(VisibleSecondDialogWindow)); }
        }
        private Visibility _visibilitiExitButton;
        public Visibility VisibilitiExitButton
        {
            get { return _visibilitiExitButton; }
            set { _visibilitiExitButton = value; OnPropertyChanged(nameof(VisibilitiExitButton));}
        }

        private decimal _cash;
        public decimal Cash
        {
            get { return _cash; }
            set { _cash = value; OnPropertyChanged(nameof(Cash)); }
        }

        private Visibility _testMode;
        public Visibility TestMode
        {
            get { return _testMode; }
            set { _testMode = value; OnPropertyChanged(nameof(TestMode)); }
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
        private Visibility _visibilitiOpenShift;
        public Visibility VisibilitiOpenShift
        {
            get { return _visibilitiOpenShift; }
            set { _visibilitiOpenShift = value;OnPropertyChanged(nameof(VisibilitiOpenShift)); }
        }

        public ICommand UpdateSizeCommand => _updateSizeCommand;

        private void UpdateSizes()
        {
            Widght = (int)Application.Current.MainWindow.ActualWidth;
            Height = (int)Application.Current.MainWindow.ActualHeight;
        }


        private void SetFieldPage()
        {
            var user = Session.User;
            if (user != null) 
            {
                _user = user;
            }
            SetTabsField();
            SetHeaderLabelField();
        }
        private void SetTabsField()
        {
            var tabs = Session.Tabs;
            if (tabs.Count == 0)
            {
                Tabs.Add(new TabItem()
                {
                    Header = " ",
                    Content = new Frame() { Content = new SaleGoodsMenu() }
                });
                SelectedTabItem = 0;
            }
            else
            {
                Tabs = new ObservableCollection<TabItem>();
                for (int i = 0; i < tabs.Count; i++)
                {
                    Tabs.Add(tabs[i]);
                }

                SelectedTabItem = tabs.Where(item => item.IsSelected == true).FirstOrDefault().TabIndex;

                OnPropertyChanged(nameof(Tabs));
            }
        }


        private void SetHeaderLabelField()
        { 

            var workingShiftStatus = Session.WorkingShiftStatus;

            if (workingShiftStatus != null)
            {
                if(workingShiftStatus.WorkingShift != null && workingShiftStatus.WorkingShift.ID != 0)
                {
                    Task.Run(async () =>
                    {
                        Session.WorkingShiftStatus.WorkingShift = await _model.GetWorkingShift(workingShiftStatus.WorkingShift.ID.ToString());
                    });
                }

                if(workingShiftStatus.OperationRecorder != null && workingShiftStatus.OperationRecorder.FiscalNumber!=string.Empty)
                {
                    FNumber = workingShiftStatus.OperationRecorder.FiscalNumber;
                }
                else
                {
                    FNumber = workingShiftStatus.WorkingShift.FiscalNumberRRO;
                } 
                StatusShift = workingShiftStatus.StatusShift;
                StatusOnline = workingShiftStatus.StatusOnline;
                _operationsRecorder = workingShiftStatus.OperationRecorder;
                if (workingShiftStatus.ObjectOwner != null)
                {
                    EconomicUnit = workingShiftStatus.ObjectOwner.NameObject;
                } 
            } 

            if (StatusShift == "Зміна відкрита")
            {
                VisibilitiOpenShift = Visibility.Collapsed;
                VisibilitiExitButton = Visibility.Collapsed;  
                StatusColor = "Green";
            }
            else
            {
                VisibilitiOpenShift = Visibility.Visible; 
                VisibilitiExitButton = Visibility.Visible;
                StatusColor = "Red";
            } 

            if (_user.FullName != null && _user.FullName != string.Empty && _user.FullName != "")
            {

                Seller = _user.FullName;
            }
            else
            {
                Seller = _user.Login;
            }  

            if ((OperationRecorderSetting.Deserialize(AppSettingsManager.GetParameterFiles("OperationRecorder").ToString())).IsTestMode)
            {
                _testMode = Visibility.Visible;
            }
            else
            {
                _testMode = Visibility.Hidden;
            }
        }


        public ICommand OpenShiftCommand => _openShiftCommand;
        private async Task OpenShift()
        { 
            try
            {
                IsEnableOpenShiftButton = false;
                Session.LoadSaleMenuDataFromFile(); 
                WorkingShift workingShiftEntity = new WorkingShift(); 
                _model.AddKey(_user.SignatureKey); 
                workingShiftEntity = new WorkingShift()
                {
                    TypeRRO = 0,
                    FiscalNumberRRO = _operationsRecorder.FiscalNumber,
                    TypeShiftCrateAt = TypeWorkingShift.OpenShift,
                    UserOpenShift = _user,
                    DataPacketIdentifier = decimal.Parse(_operationsRecorder.FiscalNumber),
                    FactoryNumberRRO = "v1",
                    MACCreateAt = await _model.GetMAC(_operationsRecorder.ID),
                    CreateAt = DateTimeOffset.Now,

                }; 
                Session.WorkingShiftStatus.WorkingShift = workingShiftEntity; 
                if (await _model.OpenShift(workingShiftEntity))
                {

                    MessageBox.Show("Змінна відкрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    ChangeHeaderLable("Зміна відкрита", "Green", true); 
                    IsEnableOpenShiftButton = true;
                    VisibilitiOpenShift = Visibility.Collapsed;
                    VisibilitiExitButton = Visibility.Collapsed;
                    SaveWorkingShiftStatus();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeHeaderLable(string status, string color, bool isOnline = false)
        {
            StatusShift = status;
            StatusColor = color;
            if (isOnline)
            {
                StatusOnline = "з " + DateTime.Now.ToString("g"); 
            }
            else
            {
                StatusOnline = string.Empty;  
            }
            OnPropertyChanged(nameof(StatusColor)); 
        }

        private void SaveWorkingShiftStatus()
        {
            if (Session.WorkingShiftStatus != null)
            {
                Session.WorkingShiftStatus.StatusShift = StatusShift;
                Session.WorkingShiftStatus.StatusOnline = StatusOnline;
                Session.WorkingShiftStatus.OperationRecorder = _operationsRecorder;

                AppSettingsManager.SetParameterFile("WorkingShiftStatus", Session.WorkingShiftStatus.Serialize());
            }
        }

        public ICommand CloseShiftCommand => _closeShiftCommand;
        private async Task CloseShift()
        {
            try
            {
                IsEnableCloseShiftButton = false;
                Session.LoadSaleMenuDataFromFile();
                _model.AddKey(_user.SignatureKey);
                var shift = Session.WorkingShiftStatus.WorkingShift;
                var info = await _model.GetOperationInfo(shift.ID);

                shift.TotalCheckForShift = info.TotalCheck;
                shift.TotalReturnCheckForShift = info.TotalReturnCheck;
                shift.UserCloseShift = _user;
                shift.AmountOfOfficialFundsIssuedCash = info.AmountOfOfficialFundsIssued;
                shift.AmountOfFundsIssued = info.AmountOfFundsIssued;
                shift.AmountOfOfficialFundsReceivedCash = info.AmountOfOfficialFundsReceived;
                shift.AmountOfFundsReceived = info.AmountOfFundsReceived;
                shift.AmountOfOfficialFundsIssuedCard = 0;
                shift.AmountOfOfficialFundsReceivedCard = 0;
                shift.EndAt = DateTimeOffset.Now;
                shift.MACEndAt = await _model.GetMAC(_operationsRecorder.ID);
                shift.TypeShiftEndAt = TypeWorkingShift.CloseShift;

                if (await _model.CloseShift(shift))
                {
                    MessageBox.Show("Змінна закрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    ChangeHeaderLable("Зміна закрита", "Red");
                    IsEnableCloseShiftButton = true;
                    VisibilitiOpenShift = Visibility.Visible;
                    VisibilitiExitButton = Visibility.Visible;
                    SaveWorkingShiftStatus();
                    //_model.Print(operation);

                } 
            }
            catch (ExceptionCheckShiftIsNotOpen e)
            {
                MessageBox.Show(e.Message);
                ChangeHeaderLable("Зміна закрита", "Red");
                IsEnableCloseShiftButton = true;
                VisibilitiOpenShift = Visibility.Visible;
                VisibilitiExitButton = Visibility.Visible;
                SaveWorkingShiftStatus();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ICommand OpenNewCheck => _openNewCheckCommand;

        private void OpenCheck()
        {
            if (((Frame)Tabs.ElementAt(0).Content).Content.GetType() != typeof(SaleGoodsMenu))
            {
                Tabs.Clear();
                Tabs.Add(new TabItem()
                {
                    Header = " ",
                    Content = new Frame() { Content = new SaleGoodsMenu() }
                });
                SelectedTabItem = 0;
            }
            else
            {
                int maxCount = 15;
                TabItem newTabItem = new TabItem();
                int count = Tabs.Count;

                if (count <= maxCount)
                {

                    newTabItem.Header = "Новий чек №" + count;
                    newTabItem.TabIndex = count;
                    newTabItem.Content = new Frame() { Content = new SaleGoodsMenu() };

                    Tabs.Add(newTabItem);

                    Session.Tabs = Tabs;
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

        }

        public ICommand OfficialDepositMoneyCommand => _officialDepositMoneyCommand;
        private void OfficialDepositMoney()
        {
            VisibleFirstDialogWindow = Visibility.Visible;
        }

        public ICommand OfficialIssuanceModeyCommand => _officialReceivedMoneyCommand;
        private void OfficialIssuanceModey()
        {
            VisibleSecondDialogWindow = Visibility.Visible;
        }

        public ICommand OkFirstDialogWindowCommand => _okFirstDialogsWindowCommand;
        private async Task OkFirstDialogWindow()
        {
            Session.LoadSaleMenuDataFromFile();
            var shift = Session.WorkingShiftStatus.WorkingShift;
            Operation operation = new Operation();
            _model.AddKey(_user.SignatureKey);
            operation = new Operation
            {
                TypeOperation = TypeOperation.DepositMoney,
                MAC = await _model.GetMAC(_operationsRecorder.ID),
                CreatedAt = DateTime.Now,
                NumberPayment = await _model.GetLocalNumber(),
                TypePayment = TypePayment.Cash,
                TotalPayment = Cash,
                GoodsTax = 0.ToString(),
            };

            VisibleFirstDialogWindow = Visibility.Hidden;
            if (await _model.DepositAndWithdrawalMoney(shift, operation))
            {
                MessageBox.Show("Сумма внесених коштів:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
                Cash = 0;
            }
            else
            {
                MessageBox.Show("Невдалося внести кошти:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
                Cash = 0;
            }
        }

        public ICommand CancelFirstDialogWindowCommand => _cancelFirstDialogsWindowCommand;
        private void CancelFirstDialogWindow()
        {
            Cash = 0;
            VisibleFirstDialogWindow = Visibility.Hidden;
        }


        public ICommand OkSecondDialogWindowCommand => _okSecondDialogsWindowCommand;
        private async Task OkSeconDialogWindow()
        {
            Session.LoadSaleMenuDataFromFile();
            var shift = Session.WorkingShiftStatus.WorkingShift;
            Operation operation = new Operation();
            _model.AddKey(_user.SignatureKey);
            operation = new Operation
            {
                TypeOperation = TypeOperation.WithdrawalMoney,
                MAC = await _model.GetMAC(_operationsRecorder.ID),
                CreatedAt = DateTime.Now,
                NumberPayment = await _model.GetLocalNumber(),
                TypePayment = TypePayment.Cash,
                GoodsTax = 0.ToString(),
                TotalPayment = Cash,
            };

            VisibleFirstDialogWindow = Visibility.Hidden;
            if (await _model.DepositAndWithdrawalMoney(shift, operation))
            {
                MessageBox.Show("Сумма виданих коштів:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
                Cash = 0;
            }
            else
            {
                MessageBox.Show("Невдалося видати кошти:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Error);
                Cash = 0;
            }
        }

        public ICommand CancelSecondDialogWindowCommand => _cancelSecondDialogsWindowCommand;
        private void CancelSecondDialogWindow()
        {
            Cash = 0;
            VisibleSecondDialogWindow = Visibility.Hidden;
        } 

        public ICommand ExitWorkShiftMenuCommand => _exitWorkShiftMenuCommand;
        private void ExitWorkShiftMenu()
        {
            MediatorService.ExecuteEvent(NavigationButton.RedirectToOperationsRecorderView.ToString());
        }

        public ICommand PrintLastCheckCommand => _printLastCheckCommand;
        private async Task PrintLastCheck()
        {
            await _model.PrintLastCheck();
        }

        public ICommand PublishCertificateCommand => _publishCertificateCommand;
        private void PublishCertificate() {
        
        }
    }
}
