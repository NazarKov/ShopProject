using ShopProject.Core.Mvvm.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShopProject.View.AdminPage.Storage.Product
{
    /// <summary>
    /// Interaction logic for UpdateProductRangeView.xaml
    /// </summary>
    public partial class UpdateProductRangeView : Window
    {
        public UpdateProductRangeView()
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
