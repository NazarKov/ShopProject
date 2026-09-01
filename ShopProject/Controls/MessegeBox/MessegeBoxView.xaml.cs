using ShopProject.Controls.MessegeBox.Enum; 
using System.Windows; 

namespace ShopProject.Controls.MessegeBox
{
    /// <summary>
    /// Interaction logic for MessegeBoxView.xaml
    /// </summary>
    public partial class MessegeBoxView : Window
    {
        public bool Result { get; private set; }

        public MessegeBoxView() { }

        private MessegeBoxView(
            string title,
            string message,
            MessageBoxType type,Style successbutton,Style cancelButton)
        {
            InitializeComponent();

            DataContext = new MessegeBoxViewModel(
                title,
                message,
                type);

            ConfigureType(type, successbutton, cancelButton);
        }

        private void ConfigureType(MessageBoxType type,Style successbutton, Style cancelButton)
        {
            switch (type)
            {
                case MessageBoxType.Success: 
                    OkButton.Style = successbutton;

                    break;

                case MessageBoxType.Warning:
                    OkButton.Style = successbutton;

                    break;

                case MessageBoxType.Error:
                    OkButton.Style = successbutton;

                    break;

                case MessageBoxType.Question:

                    CancelButton.Visibility = Visibility.Visible;

                    OkButton.Content = "Так";
                    CancelButton.Content = "Ні";
                    
                    OkButton.Style = successbutton;
                    CancelButton.Style = cancelButton;

                    break;

                default:
                    OkButton.Style = successbutton;

                    break;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            DialogResult = false;
        }

        public static bool Show(
            string message,
            Style successButton,
            Style cancelButton = null,
            string title = "Повідомлення", 
            MessageBoxType type = MessageBoxType.Information,
            Window? owner = null)
        {
            var window = new MessegeBoxView(
                title,
                message,
                type, successButton, cancelButton);

            if (owner != null)
                window.Owner = owner;

            window.ShowDialog();

            return window.Result;
        }
    }
}
