using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ShopProject.Core.Mvvm.CompositionRoot;
using ShopProject.Extensions.FactoryExtensions;
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.View.Common.Main;
using ShopProject.ViewModel.Common.Home;

namespace ShopProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppCompositionRoot Container { get; private set; } = new AppCompositionRoot();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Container.ServiceProvider.AddApplicationService(); 
            Container.ServiceProvider.Get<IResourseService>().IsInitSystemFolders();

            Container.FactoryView.AddApplicationView();
            Container.ServiceProvider.AddApplicationViewModel();


            MainView wnd = Container.GetViewWithViewModel<MainView, MainViewModel>();
            wnd.Show();
        }
    }
}
