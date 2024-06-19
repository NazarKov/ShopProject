using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ShopProject.Model.SettingPage;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingServiseSingingFilesViewModel : ViewModel<SettingServiseSingingFilesViewModel>
    {
        private SettingServiseSingingFilesModel _model;

        private ICommand _startServiseCommand;
        private ICommand _stopServiseCommand;

        private ICommand _connectServiseCommand;
        private ICommand _disconnectServiseCommand;

        private ICommand _initializingServiseCommnad;
        private ICommand _isInitializingServiseCommand;

        private ICommand _singFileCommand;
        private ICommand _autoTestServiseCommand;


        public SettingServiseSingingFilesViewModel() 
        {
            _model = new SettingServiseSingingFilesModel();


            _startServiseCommand = new DelegateCommand(() => { _model.StartServise(); });
            _stopServiseCommand = new DelegateCommand(() => { });

            _connectServiseCommand = new DelegateCommand(() => { _model.ConnectServise(); });
            _disconnectServiseCommand = new DelegateCommand(() => { Disconnect(); });
            
            _initializingServiseCommnad = new DelegateCommand(() => { Init(); });
            _isInitializingServiseCommand = new DelegateCommand(() => { IsInit(); });

            _singFileCommand = new DelegateCommand(() => { SingFile();});
            _autoTestServiseCommand = new DelegateCommand(() => { });

            _status = string.Empty;
            _message = string.Empty;
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }
        private string _message;
        public string Message
        {
            get { return _message; } 
            set { _message = value; OnPropertyChanged("Message"); }
        }

        public ICommand StartServiseCommand => _startServiseCommand;
        public ICommand StopServiseCommand => _stopServiseCommand;

        public ICommand ConnectServiseCommnad => _connectServiseCommand;
        public ICommand DisconectServiseCommand => _disconnectServiseCommand;
        private void Disconnect()
        {
            //var item = _model.SendCommand(TypeCommand.DisconnectUser);
            //if (item != null)
            //{
            //    Status = item.Status;
            //    Message = item.Description;
            //}
        }

        public ICommand InitializingSeriveCommand => _initializingServiseCommnad;
        private void Init()
        {
            //var item = _model.SendCommand(TypeCommand.Initialize);
            //if(item != null)
            //{
            //    Status = item.Status;
            //    Message = item.Description;
            //}
        }
        public ICommand IsInitializingServiseCommand => _isInitializingServiseCommand;
        private void IsInit()
        {
            //var item = _model.SendCommand(TypeCommand.IsInitialize);
            //if (item != null)
            //{
            //    Status = item.Status;
            //    Message = item.Description;
            //}
        }

       
        public ICommand SingFileCommand => _singFileCommand;
        private void SingFile()
        {
            //var item = _model.SendCommand(TypeCommand.SingFile);
            //if (item != null)
            //{
            //    Status = item.Status;
            //    Message = item.Description;
            //}
        }
        public ICommand AutoTestServieCommand => _autoTestServiseCommand;


    }
}
