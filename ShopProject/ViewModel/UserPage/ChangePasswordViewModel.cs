using ShopProject.Helpers;
using ShopProject.Model.Command;
using ShopProject.Model.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.UserPage
{
    internal class ChangePasswordViewModel : ViewModel<ChangePasswordViewModel>
    {
        private ChangePasswordModel _model;

        private ICommand _sendMessageEmailCommand;
        private ICommand _confirmCodeCommand;
        private ICommand _changePasswordCommand;
        private ICommand _openPageAuthorizationCommand;

        public ChangePasswordViewModel()
        {
            _model = new ChangePasswordModel();
            
            _email = string.Empty;
            _code = string.Empty;
            _password = string.Empty;


            _sendMessageEmailCommand = new DelegateCommand(SendMessageEmail);
            _confirmCodeCommand = new DelegateCommand(ConfirmCode);
            _changePasswordCommand = new DelegateCommand(ChangePassword);
            _openPageAuthorizationCommand = new DelegateCommand(OpenPageAuthorization); 

            _isEnableFieldCode = false;
            _isEnableFieldChagePasssword = false;
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); } 
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code));}
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password));}
        }

        private bool _isEnableFieldCode;
        public bool IsEnableFieldCode
        {
            get { return _isEnableFieldCode; }
            set { _isEnableFieldCode = value; OnPropertyChanged(nameof(IsEnableFieldCode));}
        }
        private bool _isEnableFieldChagePasssword;
        public bool IsEnableFieldChagePasssword
        {
            get { return _isEnableFieldChagePasssword; }
            set { _isEnableFieldChagePasssword = value; OnPropertyChanged(nameof(IsEnableFieldChagePasssword)); }
        }


        public ICommand SendMessageEmailCommand => _sendMessageEmailCommand;
        private void SendMessageEmail()
        {
            if (Email != string.Empty)
            {
                //_model.SendMessageEmail(Email);
                IsEnableFieldCode = true;
                MessageBox.Show("Код надіслано перевірте свою електрону адресу");
            }
        }

        public ICommand ConfirmCodeCommand => _confirmCodeCommand;
        private void ConfirmCode()
        {
            if(Code!=string.Empty)
            {
                //_model.ConfirmCode(Code);
                IsEnableFieldChagePasssword = true;
                IsEnableFieldCode = false;

                MessageBox.Show("Код підтверджено змініть пароль");
            }
        }
        public ICommand ChangePasswordCommand => _changePasswordCommand;
        private void ChangePassword()
        {
            if(Password !=string.Empty)
            {
                //_model.ChangePassword(Email, Password);
            }
        }

        public ICommand OpenPageAuthorizationCommand => _openPageAuthorizationCommand;
        private void OpenPageAuthorization()
        {
            Mediator.Notify("OpenAuthorization", "");
        }
    }
}
