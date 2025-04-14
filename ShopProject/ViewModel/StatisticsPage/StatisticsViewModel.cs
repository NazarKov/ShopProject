using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.StatisticsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.StatisticsPage
{
    internal class StatisticsViewModel : ViewModel<StatisticsViewModel>
    {
        private StatisticsModel _model;

        private ICommand _searchOperationsCommand;

        public StatisticsViewModel()
        {
            _model = new StatisticsModel();

            //_users = new List<UserEntiti>();
            //_operationsRecorders = new List<OperationsRecorderEntiti>();
            //_operations = new List<OperationEntiti>();

            //_searchOperationsCommand = new DelegateCommand(SearchOperation);

            //setFielPage();
        }

        //private List<UserEntiti> _users;
        //public List<UserEntiti> Users
        //{
        //    get { return _users; }
        //    set { _users = value; OnPropertyChanged(nameof(Users)); }
        //}

        //private UserEntiti _selectedUser;
        //public UserEntiti SelectedUser
        //{
        //    get { return _selectedUser; }
        //    set { _selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        //}
        //private OperationsRecorderEntiti _selectOperationsRecorder;
        //public OperationsRecorderEntiti SelectOperationRecorder
        //{
        //    get { return _selectOperationsRecorder; }
        //    set { _selectOperationsRecorder = value; OnPropertyChanged(nameof(SelectOperationRecorder)); }
        //}

        //private List<OperationsRecorderEntiti> _operationsRecorders;
        //public List<OperationsRecorderEntiti> OperationsRecorders
        //{
        //    get { return _operationsRecorders; }
        //    set { _operationsRecorders = value; OnPropertyChanged(nameof(OperationsRecorders)); }
        //}

        //private DateTime _startedDate;
        //public DateTime StartedDate
        //{
        //    get { return _startedDate; }
        //    set { _startedDate = value; OnPropertyChanged(nameof(StartedDate)); }
        //}

        //private DateTime _finishedDate;
        //public DateTime FinishedDate
        //{
        //    get { return _finishedDate; }
        //    set { _finishedDate = value; OnPropertyChanged(nameof(FinishedDate)); }
        //}

        //private List<OperationEntiti> _operations;
        //public List<OperationEntiti> Operations
        //{
        //    get { return _operations; }
        //    set { _operations = value; OnPropertyChanged(nameof(Operations)); }
        //}

        //private void setFielPage()
        //{
        //    Users = _model.GetUserAll();
        //    OperationsRecorders = _model.GetOperationsRecorderAll();
        //    StartedDate = DateTime.Now;
        //    FinishedDate = DateTime.Now;
        //    Operations = _model.GetOperaionAll();
        //    SelectedUser = Session.User;
        //    SelectOperationRecorder = OperationsRecorders[0];

        //}

        //public ICommand SearchOperationsCommand => _searchOperationsCommand;
        //private void SearchOperation()
        //{
        //    var items = _model.Search(StartedDate,FinishedDate,SelectedUser, SelectOperationRecorder);

        //    if(items!=null)
        //    {
        //        Operations = new List<OperationEntiti>();
        //        Operations = items;
        //    }

        //}


    }
}
