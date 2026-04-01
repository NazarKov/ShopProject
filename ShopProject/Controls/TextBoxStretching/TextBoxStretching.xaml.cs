using ShopProject.Core.Mvvm.Command;
using ShopProject.ViewModel.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopProject.Controls
{
    /// <summary>
    /// Interaction logic for TextBoxStretching.xaml
    /// </summary>
    public partial class TextBoxStretching : UserControl
    {
        public TextBoxStretchingViewModel ViewModel { get; }
        public TextBoxStretching()
        {
            ViewModel = new TextBoxStretchingViewModel(Guid.NewGuid());
            InitializeComponent();

            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                   

                    case nameof(ViewModel.ShadowVisibility):
                        SetValue(ExternalShadowVisibilityProperty, ViewModel.ShadowVisibility);
                        break;
                }
            };

        }

        public static readonly DependencyProperty TextProperty =
         DependencyProperty.Register(
             nameof(Text),
             typeof(string),
             typeof(TextBoxStretching),
             new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty ExternalShadowVisibilityProperty =
            DependencyProperty.Register(
            nameof(ExternalShadowVisibility),
            typeof(Visibility),
            typeof(TextBoxStretching),
            new FrameworkPropertyMetadata(
                Visibility.Visible,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnShadowVisibilityChanged));

        private static void OnShadowVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.ShadowVisibility = (Visibility)e.NewValue;
        }

        public Visibility ExternalShadowVisibility
        {
            get => (Visibility)GetValue(ExternalShadowVisibilityProperty);
            set => SetValue(ExternalShadowVisibilityProperty, value);
        }

        public static readonly DependencyProperty TextBlockVisibilityProperty =
        DependencyProperty.Register(
        nameof(TextBlockVisibility),
        typeof(Visibility),
        typeof(TextBoxStretching),
        new FrameworkPropertyMetadata(
            Visibility.Visible,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnTextBlockVisibilityChanged));

        private static void OnTextBlockVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.TextBlockVisibility = (Visibility)e.NewValue;
            control.ViewModel.HeightGriwRowTextBlock = 0;
        }

        public Visibility TextBlockVisibility
        {
            get => (Visibility)GetValue(TextBlockVisibilityProperty);
            set => SetValue(TextBlockVisibilityProperty, value);
        }
        public static readonly DependencyProperty ExternalLostFocusCommandProperty =
         DependencyProperty.Register(
             nameof(ExternalLostFocusCommand),
             typeof(ICommand),
             typeof(TextBoxStretching),
             new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ICommand ExternalLostFocusCommand
        {
            get => (ICommand)GetValue(ExternalLostFocusCommandProperty);
            set => SetValue(ExternalLostFocusCommandProperty, value);
        }
        private void ResizableTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ExternalLostFocusCommand == null)
            {
                ExternalLostFocusCommand = ViewModel.LostFocusCommand;
            }
        }


        public static readonly DependencyProperty HeightTextBoxProperty =
            DependencyProperty.Register(
                nameof(HeightTextBox),
                typeof(double),
                typeof(TextBoxStretching),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HeightTextBox_Changed));

        public double HeightTextBox
        {
            get => (double)GetValue(HeightTextBoxProperty);
            set => SetValue(HeightTextBoxProperty, value);
        }
        private static void HeightTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.Height = (double)e.NewValue;
        }

        public static readonly DependencyProperty WidthTextBoxProperty =
           DependencyProperty.Register(
               nameof(WidthTextBox),
               typeof(double),
               typeof(TextBoxStretching),
               new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, WidthTextBox_Changed));

        public double WidthTextBox
        {
            get => (double)GetValue(WidthTextBoxProperty);
            set => SetValue(WidthTextBoxProperty, value);
        }
        private static void WidthTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.Width = (double)e.NewValue;
        }


        public static readonly DependencyProperty MinHeightTextBoxProperty =
            DependencyProperty.Register(
                nameof(MinHeightTextBox),
                typeof(double),
                typeof(TextBoxStretching),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, MinHeightTextBox_Changed));

        public double MinHeightTextBox
        {
            get => (double)GetValue(MinHeightTextBoxProperty);
            set => SetValue(MinHeightTextBoxProperty, value);
        }
        private static void MinHeightTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.MinHeight = (double)e.NewValue;
        }

        public static readonly DependencyProperty MinWidthTextBoxProperty =
           DependencyProperty.Register(
               nameof(MinWidthTextBox),
               typeof(double),
               typeof(TextBoxStretching),
               new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, MinWidthTextBox_Changed));

        public double MinWidthTextBox
        {
            get => (double)GetValue(MinWidthTextBoxProperty);
            set => SetValue(MinWidthTextBoxProperty, value);
        }
        private static void MinWidthTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.MinWidth = (double)e.NewValue;
        }


        public static readonly DependencyProperty MaxHeightTextBoxProperty =
           DependencyProperty.Register(
               nameof(MaxHeightTextBox),
               typeof(double),
               typeof(TextBoxStretching),
               new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, MaxHeightTextBox_Changed));

        public double MaxHeightTextBox
        {
            get => (double)GetValue(MaxHeightTextBoxProperty);
            set => SetValue(MaxHeightTextBoxProperty, value);
        }
        private static void MaxHeightTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.MaxHeight = (double)e.NewValue;
        }

        public static readonly DependencyProperty MaxWidthTextBoxProperty =
           DependencyProperty.Register(
               nameof(MaxWidthTextBox),
               typeof(double),
               typeof(TextBoxStretching),
               new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, MaxWidthTextBox_Changed));

        public double MaxWidthTextBox
        {
            get => (double)GetValue(MaxWidthTextBoxProperty);
            set => SetValue(MaxWidthTextBoxProperty, value);
        }
        private static void MaxWidthTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.MaxWidth = (double)e.NewValue;
        }


        public string TextBlockText
        {
            get => (string)GetValue(TextBlockTextProperty);
            set => SetValue(TextBlockTextProperty, value);
        }

        public static readonly DependencyProperty TextBlockTextProperty =
         DependencyProperty.Register(
             nameof(TextBlockText),
             typeof(string),
             typeof(TextBoxStretching),
             new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextBlockTextChanged));

        private static void OnTextBlockTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.TextBlock.Text = (string)e.NewValue;
        }


        public static readonly DependencyProperty LostFocusWidthTextBoxProperty =
            DependencyProperty.Register(
            nameof(LostFocusWidthTextBox),
            typeof(double),
            typeof(TextBoxStretching),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, LostFoucsWidthTextBox_Changed));

        public double LostFocusWidthTextBox
        {
            get => (double)GetValue(LostFocusWidthTextBoxProperty);
            set => SetValue(LostFocusWidthTextBoxProperty, value);
        }
        private static void LostFoucsWidthTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.LostFocusWidth = (double)e.NewValue;
        }

        public static readonly DependencyProperty LostFocusHeightTextBoxProperty =
            DependencyProperty.Register(
            nameof(LostFocusHeightTextBox),
            typeof(double),
            typeof(TextBoxStretching),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, LostFoucsHeightTextBox_Changed));

        public double LostFocusHeightTextBox
        {
            get => (double)GetValue(LostFocusHeightTextBoxProperty);
            set => SetValue(LostFocusHeightTextBoxProperty, value);
        }
        private static void LostFoucsHeightTextBox_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TextBoxStretching)d;
            control.ViewModel.LostFocusHeight = (double)e.NewValue;
        } 
    }
}
