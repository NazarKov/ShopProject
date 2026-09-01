using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Controls.Paginator;
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Infrastructure.CompositionRoot.Interface; 
using ShopProject.Model.Enum;
using ShopProject.Model.Navigation;
using ShopProject.Model.UI.TaxObject; 
using ShopProject.Services.Infrastructure.Mediator;
using ShopProject.Services.Modules.Control.Interface;
using ShopProject.Services.Modules.Domain.PoinOfSale.TaxObject.Interface;
using ShopProject.Services.Modules.Mapping.TaxObject;
using ShopProject.View.AdminPage.PointOfSale.TaxObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input; 

namespace ShopProject.ViewModel.AdminPage.PointOfSale.TaxObject
{
    internal class TaxObjectsDataViewModel : ViewModel<TaxObjectsDataViewModel> , IViewModelLoadResourse
    { 
         
        private ICommand _searchItemCommand;
        private ICommand _updateGridViewCommad;
        private ICommand _openCreateTaxObjectWindowCommand;
        private ICommand _openCreateTaxObjectForKeyWindwoCommmad; 


        private bool _isReadyUpdateDataGriedView;
        private bool _reloadField;

        private readonly IMessageBoxControlService _messageBoxControlService;
        private ITaxObjectService _taxObjectService;

        public TaxObjectsDataViewModel(ITaxObjectService taxObjectService, IMessageBoxControlService messageBoxControlService)
        {
            _taxObjectService = taxObjectService;
            _messageBoxControlService = messageBoxControlService;
            _paginator = new  PaginatorViewModel();
            _taxObjects = new List<TaxObjectModel>();
            _statusTaxObject = new List<string>(); 
            _countShowList = new List<string>();
            _searchItem = string.Empty; 
 
            _updateGridViewCommad = CreateCommandAsync(async () => { _reloadField = false; SearchItem = string.Empty; SelectedStatusTaxObject = 0; SelectIndexCountShowList = 0; await SetFieldPage(); });
            _searchItemCommand = CreateCommandAsync(DebounceSearch);
            _openCreateTaxObjectWindowCommand = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateTaxObjectView, CreateTaxObjectViewModel>().Show(); });
            _openCreateTaxObjectForKeyWindwoCommmad = CreateCommand(() => { App.Container.GetNewViewWithViewModel<CreateTaxObjectFromKeyView, CreateTaxObjectFromKeyViewModel>().Show(); });

            MediatorService.AddEventAsync(NavigationButton.ReloadTaxObject.ToString(), async () => { await SafeExecuteAsync(SetFieldPage); });

            _shadowVisibility = Visibility.Collapsed; 
        }

        public async Task LoadResourse()
        {
            await SafeExecuteAsync(SetFieldPage);
        } 

        private List<TaxObjectModel> _taxObjects;
        public List<TaxObjectModel> TaxObjects
        {
            get { return _taxObjects; }
            set { _taxObjects = value; OnPropertyChanged(nameof(TaxObjects)); }
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

        private List<string> _statusTaxObject;
        public List<string> StatusTaxObject
        {
            get { return _statusTaxObject; }
            set { _statusTaxObject = value; OnPropertyChanged(nameof(StatusTaxObject)); }
        }

        private int _selectedStatusTaxObject;
        public int SelectedStatusTaxObject
        {
            get { return _selectedStatusTaxObject; }
            set
            {
                _selectedStatusTaxObject = value; OnPropertyChanged(nameof(SelectedStatusTaxObject));
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
            if (StatusTaxObject.Count == 0)
            {
                StatusTaxObject = TaxObjectStatusModel.GetTaxObjectStatusForStorage();
            }
            SelectedStatusTaxObject = 0;
        }


        private async Task SetFieldDataGridView(int countCoulmn, int page = 1, bool reloadbutton = false)
        {
            var result = await _taxObjectService.GetPageColumn(page, countCoulmn,Enum.GetValues<TypeStatusTaxObject>().ElementAt(SelectedStatusTaxObject));
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
                TaxObjects  = new List<TaxObjectModel>(paginator.Data.ToTaxObjectModel());
                _isReadyUpdateDataGriedView = true;
            }
            else if (result.IsError)
            {
                TaxObjects = new List<TaxObjectModel>();
                Paginator.CountButton = 0;
            }
        }

        private async Task UpdateDataGridView(int page = 1)
        {
            if (_isReadyUpdateDataGriedView)
            {
                if (TaxObjects != null && TaxObjects.Count > 0)
                {
                    TaxObjects.Clear();
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
            var result = await _taxObjectService.SearchByName(SearchItem, page, countColumn, Enum.GetValues<TypeStatusTaxObject>().ElementAt(SelectedStatusTaxObject));

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
                TaxObjects = new List<TaxObjectModel>(paginator.Data.ToTaxObjectModel());
            }
            else if (result.IsError)
            {
                TaxObjects = new List<TaxObjectModel>();
                Paginator.CountButton = 0;
            }
        }
        public ICommand OpenBindingOperationRecorderToTaxObjectWindwoCommand { get => CreateCommandParameterAsync<object>(OpenBindingOperationRecorderToTaxObjectWindow); }
        private async Task OpenBindingOperationRecorderToTaxObjectWindow(object parameter)
        {
            var taxObject = new TaxObjectModel();
            if (parameter != null)
            { 
                var items = (IList)parameter;
                if(items.Count == 1)
                {
                    taxObject = (TaxObjectModel)items[0];
                    if (taxObject != null)
                    {
                        _taxObjectService.SetBindingTaxObjectTOSession(taxObject.ToTaxObject());
                        App.Container.GetNewViewWithViewModel<BindingOperationRecorderToTaxObjectView, BindingOperationRecorderToTaxObjectViewModel>().Show();
                    }
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "PointOfSaleSnadow");
                }
            } 
        }

        public ICommand OpenBindingUserToTaxObjectWindwoCommand { get => CreateCommandParameterAsync<object>(OpenBindingUserToTaxObjectWindow); }
        private async Task OpenBindingUserToTaxObjectWindow(object parameter)
        {
            var taxObject = new TaxObjectModel();
            if (parameter != null)
            { 
                var items = (IList)parameter;
                if(items.Count == 1)
                {
                    taxObject = (TaxObjectModel)items[0];
                    if (taxObject != null)
                    {
                        _taxObjectService.SetBindingTaxObjectTOSession(taxObject.ToTaxObject());
                        App.Container.GetNewViewWithViewModel<BindingUserToTaxObjectView, BindingUserToTaxObjectViewModel>().Show();
                    }
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "PointOfSaleSnadow");
                } 
            } 
        }

        public ICommand SetTaxObjectStatusDisableCommand { get => CreateCommandParameterAsync<object>(SetTaxObjectStatusDisable); }
        private async Task SetTaxObjectStatusDisable(object parameter)
        {
            var taxobject = parameter as IList;
            if (taxobject != null)
            {
                if (taxobject.Count == 1)
                {
                    if (await _messageBoxControlService.Show("Вимкнути?.", "Informations", MessageBoxType.Question, "PointOfSaleSnadow"))
                    {
                        var result = await _taxObjectService.UpdateParameter("Status", TypeStatusTaxObject.Closed, ((TaxObjectModel)taxobject[0]).ToTaxObject());
                        if (result.IsSuccess)
                        {
                            await SetFieldPage();
                            await _messageBoxControlService.Show("Обєкт вимкнено.", "Informations", MessageBoxType.Success, "PointOfSaleSnadow");
                        }
                        else
                        {
                            await _messageBoxControlService.Show("Невдалося виконати операцію.", "Error", MessageBoxType.Error, "PointOfSaleSnadow");
                        }
                    }
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "PointOfSaleSnadow");
                }
            }
        }

        public ICommand SetTaxObjectStatusEnableCommand { get => new DelegateParameterCommandAsync<object>(SetTaxObjectStatusEnable); }
        private async Task SetTaxObjectStatusEnable(object parameter)
        {
            var taxobject = parameter as IList;
            if (taxobject != null)
            {
                if (taxobject.Count == 1)
                {
                    if (await _messageBoxControlService.Show("Вимкнути?.", "Informations", MessageBoxType.Question, "PointOfSaleSnadow"))
                    {
                        var result = await _taxObjectService.UpdateParameter("Status", TypeStatusTaxObject.Open, ((TaxObjectModel)taxobject[0]).ToTaxObject());
                        if (result.IsSuccess)
                        {
                            await SetFieldPage();
                            await _messageBoxControlService.Show("Обєкт вимкнено.", "Informations", MessageBoxType.Success, "PointOfSaleSnadow");
                        }
                        else
                        {
                            await _messageBoxControlService.Show("Невдалося виконати операцію.", "Error", MessageBoxType.Error, "PointOfSaleSnadow");
                        }
                    }
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "PointOfSaleSnadow");
                }
            }
        }

        public ICommand UpdateTaxObjectCommand { get => new DelegateParameterCommandAsync<object>(UpdateTaxObject); }
        private async Task UpdateTaxObject(object parameter)
        {
            var taxobjects = parameter as IList;
            if (taxobjects != null)
            {
                if (taxobjects.Count == 1)
                {
                    var item = ((TaxObjectModel)taxobjects[0]);
                    if (item!=null && !item.LoadTaxServer)
                    {
                        _taxObjectService.SetBindingTaxObjectTOSession(item.ToTaxObject());
                        App.Container.GetNewViewWithViewModel<UpdateTaxObjectView, UpdateTaxObjectViewModel>().Show();
                    }
                    else
                    {
                        await _messageBoxControlService.Show("Цей обєкт завантажено з податкової його не можливо редагувата.", "Warninng", MessageBoxType.Warning, "PointOfSaleSnadow");
                    } 
                }
                else
                {
                    await _messageBoxControlService.Show("Ви не обрали елемент.", "Warninng", MessageBoxType.Warning, "PointOfSaleSnadow");
                }
            }
        }

        public ICommand UpdateFieldPageCommand => _updateGridViewCommad;
        public ICommand OpenCreateTaxObjectWindow => _openCreateTaxObjectWindowCommand;
        public ICommand OpenCreateTaxObjectForKeyWindwoCommmad => _openCreateTaxObjectForKeyWindwoCommmad;
    }
}
