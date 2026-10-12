using ShopProject.Services.Infrastructure.ControlWindow.Interface;
using System.Linq;
using System.Windows;

namespace ShopProject.Services.Infrastructure.ControlWindow
{
    internal class ControlWindowService : IControlWindowService
    {
        public void ActivateMainWindow()
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow == null)
                return;

            if (mainWindow.WindowState == WindowState.Minimized)
                mainWindow.WindowState = WindowState.Normal;

            mainWindow.Show();
            mainWindow.Activate();
            mainWindow.Focus();
        }

        public void CloseSecondaryWindows()
        {
            foreach (Window window in Application.Current.Windows
                                              .Cast<Window>()
                                              .ToList())
            {
                if (window != Application.Current.MainWindow)
                {
                    window.Close();
                }
            }
        }
    }
}
