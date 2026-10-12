using ShopProject.Extensions.FactoryExtensions;
using ShopProject.Infrastructure.CompositionRoot; 
using ShopProject.Services.Modules.Resourse.Interface;
using ShopProject.View.Common.Main;
using ShopProject.ViewModel.Common.Main;
using System.Threading;
using System.Windows;

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
        private static Mutex? _mutex;
        private EventWaitHandle? _showEvent;

        protected override void OnStartup(StartupEventArgs e)
        {
            const string id = "ShopAutomation_{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}";

            _mutex = new Mutex(true, @"Local\" + id, out bool isNew);
            _showEvent = new EventWaitHandle(false, EventResetMode.AutoReset, @"Local\" + id + "_show");

            if (!isNew)
            {
                _showEvent.Set();   
                Shutdown();
                return;
            }

            base.OnStartup(e);

            var thread = new Thread(() =>
            {
                while (_showEvent.WaitOne())
                {
                    Dispatcher.Invoke(() =>
                    {
                        var w = MainWindow;
                        if (w == null) return;
                        w.Show();
                        if (w.WindowState == WindowState.Minimized)
                            w.WindowState = WindowState.Normal;
                        w.Activate();
                        w.Topmost = true;   
                        w.Topmost = false;
                        w.Focus();
                    });
                }
            })
            { IsBackground = true };
            thread.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
            base.OnExit(e);
        }
    }
}
