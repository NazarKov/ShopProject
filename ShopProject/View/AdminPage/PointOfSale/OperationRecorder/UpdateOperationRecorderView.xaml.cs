using ShopProject.Core.Mvvm.Interface; 
using System.Windows; 

namespace ShopProject.View.AdminPage.PointOfSale.OperationRecorder
{
    /// <summary>
    /// Interaction logic for UpdateOperationRecorderView.xaml
    /// </summary>
    public partial class UpdateOperationRecorderView : Window
    {
        public UpdateOperationRecorderView()
        {
            InitializeComponent();
            Loaded += LoadExitButton;
        }
        private void LoadExitButton(object sender, RoutedEventArgs e)
        {
            if (DataContext is IСontrolView vm)
            {
                vm.CloseView += () =>
                {
                    this.Close();
                };
            }
        }
    }
}
