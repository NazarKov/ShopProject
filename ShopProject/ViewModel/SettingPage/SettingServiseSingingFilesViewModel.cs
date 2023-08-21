using ShopProject.Helpers.MiniServiceSigningFile;
using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.ViewModel.SettingPage
{
    internal class SettingServiseSingingFilesViewModel : ViewModel<SettingServiseSingingFilesViewModel>
    {
        const string pathservise = "..\\..\\..\\..\\..\\ShopProject\\MiniServiceSigningFiles\\bin\\Debug\\MiniServiceSigningFiles.exe";//Debug
        private ICommand startServise;
        private ICommand stopServise;

        private ICommand initializingSingingFiles;
        private ICommand isInitializing;

        private ICommand singFile;


        public SettingServiseSingingFilesViewModel() 
        {
            MainContoller mainContoller = new MainContoller();
            mainContoller.StartServise();
            startServise = new DelegateCommand(() => { mainContoller.StartServise(); mainContoller.ConnectService(); });
            stopServise = new DelegateCommand(() => { mainContoller.SendingCommand(TypeCommand.Disconnect); });
            initializingSingingFiles = new DelegateCommand(() => { mainContoller.SendingCommand(TypeCommand.Initialize);});
            isInitializing = new DelegateCommand(() => { mainContoller.SendingCommand(TypeCommand.IsInitialize); });
            singFile = new DelegateCommand(() => { mainContoller.SendingCommand(TypeCommand.SingFile);  });
        }


        public ICommand StartServise => startServise;
        public ICommand StopServise => stopServise;
        public ICommand Initializing => initializingSingingFiles;
        public ICommand IsInitializing => isInitializing;
        public ICommand SingFile => singFile;


    }
}
