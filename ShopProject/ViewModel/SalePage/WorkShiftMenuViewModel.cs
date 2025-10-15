using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Model.Command;
using ShopProject.Model.SalePage;
using ShopProject.UIModel.OperationRecorderPage;
using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.UserPage;
using ShopProject.Views.SalePage;
using ShopProjectDataBase.Entities;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopProject.ViewModel.SalePage
{
    internal class WorkShiftMenuViewModel : ViewModel<WorkShiftMenuViewModel>
    {
        private WorkShiftMenuModel _model;

        private ICommand _openShiftCommand;
        private ICommand _closeShiftCommand;

        private ICommand _updateSizeCommand;
        private ICommand _openNewCheckCommand;
        private ICommand _openReturnGoodsMenuCommand;

        private ICommand _officialDepositMoneyCommand;
        private ICommand _officialReceivedMoneyCommand;

        private ICommand _okFirstDialogsWindowCommand;
        private ICommand _cancelFirstDialogsWindowCommand;

        private ICommand _okSecondDialogsWindowCommand;
        private ICommand _cancelSecondDialogsWindowCommand;

        private ICommand _exitWorkShiftMenuCommand;
        private ICommand _printLastCheckCommand;

        private User _user;
        private OperationRecorder _operationsRecorder;

        public WorkShiftMenuViewModel ()
        {
            _model = new WorkShiftMenuModel ();

            _openShiftCommand = new DelegateCommand(OpenShift);
            _closeShiftCommand = new DelegateCommand(CloseShift);
            _updateSizeCommand = new DelegateCommand(UpdateSizes);
            _openNewCheckCommand = new DelegateCommand(openNewCheck);
            _openReturnGoodsMenuCommand = new DelegateCommand(OpenReturnGoodsMenu);


            _officialDepositMoneyCommand = new DelegateCommand(OfficialDepositMoney);
            _officialReceivedMoneyCommand = new DelegateCommand(OfficialIssuanceModey);

            _okFirstDialogsWindowCommand = new DelegateCommand(OkFirstDialogWindow);
            _cancelFirstDialogsWindowCommand = new DelegateCommand(CancelFirstDialogWindow);


            _okSecondDialogsWindowCommand = new DelegateCommand(OkSeconDialogWindow);
            _cancelSecondDialogsWindowCommand = new DelegateCommand(CancelSecondDialogWindow);
            _exitWorkShiftMenuCommand = new DelegateCommand(ExitWorkShiftMenu);
            _printLastCheckCommand = new DelegateCommand(PrintLastCheck);

            _statusShift = string.Empty;
            _statusColor = string.Empty;
            _tabs = new ObservableCollection<TabItem>();

            _user = Session.User;
            _operationsRecorder = new OperationRecorder();
            _operationsRecorder = Session.FocusDevices;
            _fnNumber = string.Empty;
            _economicUnit = string.Empty;
            _seller = string.Empty;
            _statusOnline = string.Empty;

            VisibleFirstDialogWindow = Visibility.Hidden;
            VisibleSecondDialogWindow = Visibility.Hidden;
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

        public ICommand UpdateSizeCommand => _updateSizeCommand;

        private void UpdateSizes()
        {
            Widght = (int)Application.Current.MainWindow.ActualWidth;
            Height = (int)Application.Current.MainWindow.ActualHeight;
        }


        private void SetFieldPage()
        { 
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

            StatusShift = AppSettingsManager.GetParameterFiles("StatusWorkShift").ToString();
            StatusOnline = AppSettingsManager.GetParameterFiles("StatusWorkShiftTime").ToString();
           
            if (StatusShift == "Зміна відкрита")
            {
                StatusColor = "Green";
            }
            else
            {
                StatusColor = "Red";
            }

            FNumber = _operationsRecorder.FiscalNumber;
             
            if (_operationsRecorder.ObjectOwner != null)
            {
                EconomicUnit = _operationsRecorder.ObjectOwner.NameObject;
            }

            if (_user.FullName != null && _user.FullName != string.Empty && _user.FullName != "")
            {

                Seller = _user.FullName;
            }
            else
            {
                Seller = _user.Login;
            } 

            if ((bool)AppSettingsManager.GetParameterFiles("TestMode"))
            {
                _testMode = Visibility.Visible;
            }
            else
            {
                _testMode = Visibility.Hidden;
            }
        }


        public ICommand OpenShiftCommand => _openShiftCommand;
        private void OpenShift()
        {
            WorkingShift workingShiftEntity = new WorkingShift();

            Task t = Task.Run(async () => {

                _model.AddKey(_user.SignatureKey);
                workingShiftEntity = new WorkingShift()
                {
                    TypeRRO = 0,
                    FiscalNumberRRO = _operationsRecorder.FiscalNumber,
                    TypeShiftCrateAt =  TypeWorkingShift.OpenShift,
                    UserOpenShift = _user,
                    DataPacketIdentifier = decimal.Parse(_operationsRecorder.FiscalNumber), 
                    FactoryNumberRRO = "v1",
                    MACCreateAt = await _model.GetMAC(_operationsRecorder.ID),
                    CreateAt = DateTimeOffset.Now,
                    
                }; 
                Session.WorkingShift = workingShiftEntity;
            });
            t.ContinueWith(t =>
            {
                if (_model.OpenShift(workingShiftEntity))
                {
                    StatusShift = "Зміна відкрита";
                    StatusColor = "Green";
                    AppSettingsManager.SetParameterFile("StatusWorkShift", StatusShift);
                    StatusOnline = "з " + DateTime.Now.ToString("g");
                    AppSettingsManager.SetParameterFile("StatusWorkShiftTime", StatusOnline);
                    AppSettingsManager.SetParameterFile("FocusDevise", _operationsRecorder.ID.ToString());


                    MessageBox.Show("Змінна відкрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }

        public ICommand CloseShiftCommand => _closeShiftCommand;
        private void CloseShift()
        {

            var shift = Session.WorkingShift;



            Task t = Task.Run(async () =>
            {
                _model.AddKey(_user.SignatureKey);
                shift.TotalCheckForShift = 0;
                shift.TotalReturnCheckForShift = 0;
                shift.UserCloseShift = _user;
                shift.AmountOfOfficialFundsIssuedCash = 0;
                shift.AmountOfFundsIssued = 0;
                shift.AmountOfOfficialFundsReceivedCash = 0;
                shift.AmountOfFundsReceived = 0;
                shift.AmountOfOfficialFundsIssuedCard = 0;
                shift.AmountOfOfficialFundsReceivedCard = 0;
                shift.EndAt = DateTimeOffset.Now;
                shift.MACEndAt = await _model.GetMAC(_operationsRecorder.ID);
                shift.TypeShiftEndAt =  TypeWorkingShift.CloseShift;
            });
            t.ContinueWith(t => {
                if (_model.CloseShift(shift))
                {
                    StatusShift = "Зміна закрита";
                    StatusColor = "Red";
                    AppSettingsManager.SetParameterFile("StatusWorkShift", StatusShift);
                    StatusOnline = string.Empty;
                    AppSettingsManager.SetParameterFile("StatusWorkShiftTime", StatusOnline);
                    //_model.Print(operation);


                    AppSettingsManager.SetParameterFile("FocusDevise", string.Empty);

                    MessageBox.Show("Змінна закрита", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                } 
            });

        }

        public ICommand OpenNewCheck => _openNewCheckCommand;

        private void openNewCheck()
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
                    OnPropertyChanged("Tabs");

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
        private void OkFirstDialogWindow()
        {
            new Thread(new ThreadStart(() =>
            {

                //int number = int.Parse(_model.GetLocalNumber());
                OperationEntity operation = new OperationEntity
                {
                   // VersionDataPaket = 1,
                   // TypeOperation = 2,
                   // DataPacketIdentifier = 1,
                   // TypeRRO = 0,
                   // FiscalNumberRRO = Session.FocusDevices.FiscalNumber,
                   // TaxNumber = Session.User.TIN,
                   // FactoryNumberRRO = "v1",
                    //MAC = _model.GetMac(),
                    CreatedAt = DateTime.Now,
                   // NumberPayment = number.ToString(),
                   // FormOfPayment = 0,
                    TotalPayment = Cash,
                };

                VisibleFirstDialogWindow = Visibility.Hidden;
                if (_model.OfficialDepositMoney(operation))
                {
                    MessageBox.Show("Сумма внесених коштів:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
                    Cash = 0;
                }
                else
                {
                    MessageBox.Show("Невдалося внести кошти:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
                    Cash = 0;
                }

            })).Start();
        }

        public ICommand CancelFirstDialogWindowCommand => _cancelFirstDialogsWindowCommand;
        private void CancelFirstDialogWindow()
        {
            Cash = 0;
            VisibleFirstDialogWindow = Visibility.Hidden;
        }


        public ICommand OkSecondDialogWindowCommand => _okSecondDialogsWindowCommand;
        private void OkSeconDialogWindow()
        {
            new Thread(new ThreadStart(() =>
            {

               // int number = int.Parse(_model.GetLocalNumber());
                OperationEntity operation = new OperationEntity
                {
                    //VersionDataPaket = 1,
                    //TypeOperation = 2.01m,
                    //DataPacketIdentifier = 1,
                    //TypeRRO = 0,
                    //FiscalNumberRRO = Session.FocusDevices.FiscalNumber,
                    //TaxNumber = Session.User.TIN,
                    //FactoryNumberRRO = "v1",
                   // MAC = _model.GetMac(),
                    CreatedAt = DateTime.Now,
                   // NumberPayment = number.ToString(),
                    //FormOfPayment = 0,
                    TotalPayment = Cash,
                };
                VisibleSecondDialogWindow = Visibility.Hidden;
                if (_model.OfficialDepositMoney(operation))
                {
                    MessageBox.Show("Сумма виданих коштів:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Information);
                    Cash = 0;
                }
                else
                {
                    MessageBox.Show("Невдалося видати кошти:" + Cash, "inform", MessageBoxButton.OK, MessageBoxImage.Error);
                    Cash = 0;
                }

            })).Start();

        }

        public ICommand CancelSecondDialogWindowCommand => _cancelSecondDialogsWindowCommand;
        private void CancelSecondDialogWindow()
        {
            Cash = 0;
            VisibleSecondDialogWindow = Visibility.Hidden;
        }

        public ICommand OpenReturnGoodsMenuCommand => _openReturnGoodsMenuCommand;
        private void OpenReturnGoodsMenu()
        {
            if (Tabs.Count != 0)
            {
                Tabs.Clear();
                Tabs.Add(new TabItem()
                {
                    Header = " ",
                    Content = new Frame() { Content = new ReturnGoodsMenu() }
                });
                SelectedTabItem = 0;

                OnPropertyChanged("Tabs");
            }
        }

        public ICommand ExitWorkShiftMenuCommand => _exitWorkShiftMenuCommand;
        private void ExitWorkShiftMenu()
        {
            MediatorService.ExecuteEvent(NavigationButton.RedirectToOperationsRecorderView.ToString()); 
            Session.FocusDevices = null;
        }

        public ICommand PrintLastCheckCommand => _printLastCheckCommand;
        private void PrintLastCheck()
        {

        }

    }
}
