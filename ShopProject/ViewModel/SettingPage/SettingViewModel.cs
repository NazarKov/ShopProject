using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingViewModel : ViewModel<SettingViewModel>
    {
        private ICommand dbSettingOpen;

        public SettingViewModel()
        {
            dbSettingOpen = new DelegateCommand(DbSettingPageOpen);
            _visibilitiDbPage = "Visible";
        }

        private string _visibilitiDbPage;
        public string VisibilitiDbPage
        {
            set { 
                _visibilitiDbPage = value;
                OnPropertyChanged("VisibilitiDbPage");
            }
            get { return _visibilitiDbPage; }
        }

        public ICommand DbSettingOpen => dbSettingOpen;

        private void DbSettingPageOpen()
        {
            if(_visibilitiDbPage =="Hidden")
            {
                VisibilitiDbPage = "Visible";
            }
            else
            {
                VisibilitiDbPage = "Hidden";
            }
        }
    }
}
