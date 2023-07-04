using ShopProject.Model.Command;
using ShopProject.Model.SettingPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingDeviceSettlementOperationsViewModel : ViewModel<SettingDeviceSettlementOperationsViewModel>
    {
        private SettingDeviceSettlementOperationsModel _model;

        private ICommand _chekConnectionServerCommand;

        public SettingDeviceSettlementOperationsViewModel()
        {
            _model = new SettingDeviceSettlementOperationsModel();
            _chekConnectionServerCommand = new DelegateCommand(() => { ChekConnectionServer(); });

        }

        public ICommand ChekConnectionServerCommand=> _chekConnectionServerCommand;
        private void ChekConnectionServer()
        {
            _model.ChekConnection();
        }

    }
}
