using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm; 
using ShopProject.Infrastructure.CompositionRoot.Interface; 
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.OperationRecorder;
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Domain.OperationRecorder.Interface; 
using ShopProject.Services.Modules.Mapping.OperationRecorder;
using ShopProject.View.AdminPage.PointOfSale.OperationRecorder;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading;
using System.Threading.Tasks;
using System.Windows; 
using System.Windows.Input;

namespace ShopProject.ViewModel.AdminPage.PointOfSale.OperationRecorder
{
    internal class OperationRecordersDataViewModel : ViewModel<OperationRecordersDataViewModel>, IViewModelLoadResourse
    {
        private ICommand _searchItemCommand;
        private ICommand _updateGridViewCommad;
        private ICommand _openCreateOperationRecorderWindowCommand;
        private ICommand _openCreateOperationRecorderFromKeyWindowCommand;

        private bool _isReadyUpdateDataGriedView;
        private bool _reloadField;

        private IOperationRecorderService _operationRecorderService;
        public OperationRecordersDataViewModel(IOperationRecorderService operationRecorderService)
        {  
            _operationRecorderService = operationRecorderService;

            _operationRecorders = new List<OperationRecorderModel>();
            _paginator = new PaginatorViewModel();
            _statusOperationRecorder = new List<string>();
            _countShowList = new List<string>();
            _searchItem = string.Empty;


            _updateGridViewCommad = CreateCommandAsync(async () => { _reloadField = false; SearchItem = string.Empty; SelectedStatusOperationRecorder = 0; SelectIndexCountShowList = 0; await SetFieldPage(); });
            _searchItemCommand = CreateCommandAsync(DebounceSearch);
            _openCreateOperationRecorderWindowCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateOperationRecorederView, CreateOperationRecorederViewModel>().Show(); });
            _openCreateOperationRecorderFromKeyWindowCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateOperationRecorderFromKeyView,CreateOperationRecorderFromKeyViewModel>().Show();});
            MediatorService.AddEventAsync(NavigationButton.ReloadOperationRecroder.ToString(), async () => { await SafeExecuteAsync(SetFieldPage); });

            _shadowVisibility = Visibility.Collapsed;
        }
        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage);
        }
        private List<OperationRecorderModel> _operationRecorders;
        public List<OperationRecorderModel> OperationRecorders
        {
            get { return _operationRecorders; }
            set { _operationRecorders = value; OnPropertyChanged(nameof(OperationRecorders)); }
        }

        private PaginatorViewModel _paginator;
        public PaginatorViewModel Paginator
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
                Task.Run(async () => { await UpdateDataGridView(); });
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
                Task.Run(async () => { await UpdateDataGridView(); });
            }
        }

        private string _searchItem;
        public string SearchItem
        {
            get { return _searchItem; }
            set { _searchItem = value; OnPropertyChanged(nameof(SearchItem)); if (_reloadField) { SearchCommand.Execute(null); } }
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


        public async Task SetFieldPage()
        { 
            SetComboBox();
            SetFielComboBoxTypeStatusProduct();
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

        private void SetFielComboBoxTypeStatusProduct()
        {
            if (StatusOperationRecorder.Count == 0)
            {
                StatusOperationRecorder = OperationRecorderStatusModel.GetTaxObjectStatusForStorage();
            }
            SelectedStatusOperationRecorder = 0;
        }


        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            var result = await _operationRecorderService.GetPageColumn(page, countCoulmn, Enum.GetValues<TypeStatusOperationRecorder>().ElementAt(SelectedStatusOperationRecorder));
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
                    throw new Exception("Невдалося завантажити");
                }
                OperationRecorders = new List<OperationRecorderModel>(paginator.Data.ToOperationRecorderModel());
                _isReadyUpdateDataGriedView = true;
            }
            else if (result.IsError)
            {
                OperationRecorders = new List<OperationRecorderModel>();
                Paginator.CountButton = 0;
            }
        }

        private async Task UpdateDataGridView(int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (OperationRecorders != null && OperationRecorders.Count > 0)
                {
                    OperationRecorders.Clear();
                }

                int countColumn = int.Parse(CountShowList.ElementAt(SelectIndexCountShowList));
                if (string.IsNullOrEmpty(SearchItem))
                {
                    await SetFieldDataGridView(countColumn, page, false);
                }
                else
                {
                    SearchByNameAndByBarCode(countColumn, page);
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
            var result = await _operationRecorderService.SearchByName(SearchItem, page, countColumn, Enum.GetValues<TypeStatusOperationRecorder>().ElementAt(SelectedStatusOperationRecorder));

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
                OperationRecorders = new List<OperationRecorderModel>(paginator.Data.ToOperationRecorderModel());
            }
            else if (result.IsError)
            {
                OperationRecorders = new List<OperationRecorderModel>();
                Paginator.CountButton = 0;
            }
        }
        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateOperationRecorderWindowCommand => _openCreateOperationRecorderWindowCommand;
        public ICommand OpenCreateOperationRecorderFromKeyWindowCommand => _openCreateOperationRecorderFromKeyWindowCommand;
    }
}
