﻿using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ShopProject.Views.SettingPage;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingViewModel : ViewModel<SettingViewModel>
    {
        private ICommand _dataBaseSettingOpenCommand;
        private ICommand _generalSettingOpenCommand;



        public SettingViewModel()
        {
            _dataBaseSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingDataBase(); });
            _generalSettingOpenCommand = new DelegateCommand(() => { PageSetting = new SettingGeneral(); });
        }

        private Page _pageSetting;
        public Page PageSetting
        {
            get { return _pageSetting; }
            set { _pageSetting = value; OnPropertyChanged("PageSetting"); }
        }


        public ICommand DataBaseSettingOpenCommand => _dataBaseSettingOpenCommand;
        public ICommand GeneralSettingOpenCommand => _generalSettingOpenCommand;
       
    }
}
