using DocumentFormat.OpenXml.Drawing.Charts;
using ShopProject.Helpers;
using ShopProject.Helpers.Navigation;
using ShopProject.Helpers.Template.Paginator;
using ShopProject.Model.Command;
using ShopProject.Model.GiftCertificatesPage;
using ShopProject.UIModel.GiftCertificatesPage;
using ShopProject.UIModel.StoragePage;
using ShopProject.View.GiftCertificatesPage;
using ShopProject.View.StoragePage.ProductPage;
using ShopProject.View.ToolsPage;
using ShopProject.ViewModel.TemplatePage;
using ShopProjectDataBase.Helper;
using ShopProjectDataBase.Migrations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.GiftCertificatesPage
{
    internal class GiftCertificatesViewModel :ViewModel<GiftCertificatesViewModel>
    {
        private GiftCertificatesModel _model;
        
        private ICommand _updateSizeGridCommand; 
        private ICommand _updateGiftCertificatesDataGridViewCommand;
        private ICommand _openWindowAddGiftCertificateCommad;

        private bool _isReadyUpdateDataGriedView;
        private static Timer _timer;
        private static string _itemSearch;

        public GiftCertificatesViewModel()
        {
            _updateSizeGridCommand = new DelegateCommand(UpdateSizes);
            _updateGiftCertificatesDataGridViewCommand = new DelegateCommand(SetFieldPage);
            _openWindowAddGiftCertificateCommad = new DelegateCommand(() => { new AddGiftCertificateView().Show(); });

            _model = new GiftCertificatesModel();
            _paginator = new TemplatePaginatorButtonViewModel();
            _statusGiftCertificate = new List<string>();
            _countShowList = new List<string>();
            _giftCertificates = new List<GiftCertificate>();
            _itemSearch = string.Empty;

            Paginator.Callback = UpdateDataGridView;

            _timer = new Timer(OnInputStopped, null, Timeout.Infinite, Timeout.Infinite);

            SetFieldPage();

            MediatorService.AddEvent(NavigationButton.ReloadGiftCertificates.ToString(), (object obg) => { SetFieldPage(); });
        }
        private TemplatePaginatorButtonViewModel _paginator;
        public TemplatePaginatorButtonViewModel Paginator
        {
            get { return _paginator; }
            set { _paginator = value; OnPropertyChanged(nameof(Paginator)); }
        }

        private List<GiftCertificate>? _giftCertificates;
        public List<GiftCertificate>? GiftCertificates
        {
            get { return _giftCertificates; }
            set { _giftCertificates = value; OnPropertyChanged(nameof(GiftCertificates)); }
        }

        private List<string> _statusGiftCertificate;
        public List<string> StatusGiftCertificate
        {
            get { return _statusGiftCertificate; }
            set { _statusGiftCertificate = value; OnPropertyChanged(nameof(StatusGiftCertificate)); }
        }
        private int _selectedStatusGiftCertificate;
        public int SelectedStatusGiftCertificate
        {
            get { return _selectedStatusGiftCertificate; }
            set
            {
                _selectedStatusGiftCertificate = value; OnPropertyChanged(nameof(SelectedStatusGiftCertificate));
                UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            }
        }

        private int _heigth;
        public int Heigth
        {
            get { return _heigth; }
            set { _heigth = value; OnPropertyChanged(nameof(Heigth)); }
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

        private void SetFieldPage()
        {
            SetComboBox();
            SetFielComboBoxTypeStatusCertificates(); 
            SetFieldDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)), 1, true);
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
        private void SetFielComboBoxTypeStatusCertificates()
        {
            SelectedStatusGiftCertificate = 0;
            if (StatusGiftCertificate.Count == 0)
            {
                StatusGiftCertificate.Add("Всі сертифікати");
                StatusGiftCertificate.Add("Доступні до видачі");
                StatusGiftCertificate.Add("Ахівовані"); 
                StatusGiftCertificate.Add("Видані");
            }
        }

        private void SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = true)
        {
            PaginatorData<GiftCertificate> result = new PaginatorData<GiftCertificate>();
            Task.Run(async () => {

                result = await _model.GetGiftCertificatePageColumn(page, countCoulmn,
                    Enum.Parse<TypeStatusGiftCertificate>(Enum.GetNames(typeof(TypeStatusGiftCertificate)).ToList().ElementAt(SelectedStatusGiftCertificate)));
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
                GiftCertificates = result.Data.ToList();
                _isReadyUpdateDataGriedView = true;
            });
        }

        private void UpdateDataGridView(int countCoulmn, int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (GiftCertificates != null && GiftCertificates.Count > 0)
                {
                    GiftCertificates.Clear();
                }
                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (_itemSearch == string.Empty && _itemSearch == "")
                {
                    if (page == 1)
                    {
                        SetFieldDataGridView(countColumn, page);
                    }
                    else
                    {
                        SetFieldDataGridView(countColumn, page, false);
                    }
                }
                else
                {
                    SearchByNameAndByBarCode(countCoulmn, page);
                }
            }
        }

        public ICommand SearchCommand { get => new DelegateParameterCommand(SearchProduct, CanRegister); }

        private void SearchProduct(object parameter)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _itemSearch = parameter.ToString();

            _timer.Change(2000, Timeout.Infinite);
        }

        private void OnInputStopped(object state)
        {
            UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void SearchByNameAndByBarCode(int countColumn, int page)
        {
            PaginatorData<GiftCertificate> result = new PaginatorData<GiftCertificate>();

            Task t = Task.Run(async () =>
            {
                if (_itemSearch.Count() == 13 && Regex.Matches(_itemSearch, "[1-9]").Any()) // 13 - довжина штрихкоду
                {
                    result.Data = new List<GiftCertificate>() { (await _model.SearchByBarCode(_itemSearch,
                            Enum.Parse<TypeStatusGiftCertificate>(Enum.GetNames(typeof(TypeStatusGiftCertificate)).ToList().ElementAt(SelectedStatusGiftCertificate)))) };
                }
                else
                {
                    result = await _model.SearchByName(_itemSearch, page, countColumn,
                        Enum.Parse<TypeStatusGiftCertificate>(Enum.GetNames(typeof(TypeStatusGiftCertificate)).ToList().ElementAt(SelectedStatusGiftCertificate)));
                }

            });
            t.ContinueWith(t =>
            {
                if (result.Data.Count() > 0 & result.Pages == 0)
                {
                    Paginator.CountButton = 1;
                }
                else
                {

                    Paginator.CountButton = result.Pages;
                }
                GiftCertificates = result.Data.ToList();
            });
        }
        public ICommand OpenWindowAddGiftCertificateCommand => _openWindowAddGiftCertificateCommad;


        public ICommand UpdateGiftCertificateCommand { get => new DelegateParameterCommand(UpdateGiftCertificate, CanRegister); }
        private void UpdateGiftCertificate(object parameter)
        {
            var _giftCertificates = new List<GiftCertificate>();
            if (_model != null)
                _model.ContertIListToList((IList)parameter, _giftCertificates);

            if (_giftCertificates.Count == 1)
            {
                Session.GiftCertificate = _giftCertificates[0];
                new UpdateGiftCertificateView().ShowDialog();
            } 
            UpdateDataGridView(int.Parse(CountShowList.ElementAt(SelectIndexCountShowList)));
        }
        public ICommand AddGiftCertificateArhiveCommand { get => new DelegateParameterCommand(AddGiftCertificateArhive, CanRegister); }
        private void AddGiftCertificateArhive(object parameter)
        {
            if (MessageBox.Show("перенести?", "informations", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var _giftCertificates = new List<GiftCertificate>();
                if (_model != null)
                {
                    _model.ContertIListToList((IList)parameter, _giftCertificates);
                    if (_giftCertificates.Count == 1)
                    {
                        Task.Run(async () => {
                            if (await _model.SetItemInArhive(_giftCertificates[0]))
                            {
                                SetFieldPage();
                                MessageBox.Show("Товар сертифікат в архів");
                            }
                        });
                    }
                }
            }
        }
        public ICommand OpenWindoiwCreateStikerCommand { get => new DelegateParameterCommand(ShowWindowCreateStikerCommand, CanRegister); }
        private void ShowWindowCreateStikerCommand(object parameter)
        {
            var _giftCertificates = new List<GiftCertificate>();
            if (_model != null)
                _model.ContertIListToList((IList)parameter, _giftCertificates);

            if (_giftCertificates.Count == 1)
            {
                Session.Product = null;
                Session.GiftCertificate = _giftCertificates[0];
                new CreateStickerView().Show();
            }
        }

        public ICommand UpdateSizeCommand => _updateSizeGridCommand;

        private void UpdateSizes()
        {
            Heigth = (int)Application.Current.MainWindow.ActualHeight - 380;
        }
        private bool CanRegister(object parameter) => true;

        public ICommand UpdateGiftCertificatesDataGridViewCommand => _updateGiftCertificatesDataGridViewCommand;
    }
}
