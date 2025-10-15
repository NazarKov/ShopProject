using ShopProject.Model.AdminPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.ViewModel.AdminPage.OperationRecorderPage
{
    internal class OperationRecorderDataViewModel : ViewModel<OperationRecorderDataViewModel>
    {
        private OperationsRecorderModel _model;
        private OperationRecorderDataViewModel() 
        {
            _model = new OperationsRecorderModel();
        }
    }
}
