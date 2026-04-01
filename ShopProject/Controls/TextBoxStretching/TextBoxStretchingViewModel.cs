using Microsoft.EntityFrameworkCore.Metadata;
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ShopProject.ViewModel.Controls
{
    public class TextBoxStretchingViewModel : ViewModel<TextBoxStretchingViewModel>
    {
        public Guid ID;
        public TextBoxStretchingViewModel(Guid id) 
        {
            ID = id;
            _height = 0;
            _width = 0;
            _minheight = 0;
            _minWidth = 0;
            _lostFocusheight = 0;
            _lostFocusWidth = 0;
            _maxheight = 60;
            _maxWidth = 250;

            _heightGriwRowTextBlock = 21;

            _resizableTextBoxZIndex = 0;
            _resizableGridZIndex = 0;
            _shadowVisibility = Visibility.Collapsed;
            _textBlockVisibility = Visibility.Visible;
        }

        private int _resizableGridZIndex;
        public int ResizableGridZIndex
        {
            get => _resizableGridZIndex;
            set { _resizableGridZIndex = value; OnPropertyChanged(nameof(ResizableGridZIndex)); }
        }

        private int _resizableTextBoxZIndex;
        public int ResizableTextBoxZIndex
        {
            get => _resizableTextBoxZIndex;
            set { _resizableTextBoxZIndex = value; OnPropertyChanged(nameof(ResizableTextBoxZIndex)); }
        }

        private Visibility _shadowVisibility;
        public Visibility ShadowVisibility
        {
            get { return _shadowVisibility; }
            set { _shadowVisibility = value; OnPropertyChanged(nameof(ShadowVisibility)); }
        }

        private Visibility _textBlockVisibility;
        public Visibility TextBlockVisibility
        {
            get { return _textBlockVisibility; }
            set { _textBlockVisibility = value; OnPropertyChanged(nameof(TextBlockVisibility)); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged(nameof(Height)); }
        }

        private double _heightGriwRowTextBlock;
        public double HeightGriwRowTextBlock
        {
            get { return _heightGriwRowTextBlock; }
            set { _heightGriwRowTextBlock = value; OnPropertyChanged(nameof(HeightGriwRowTextBlock)); }
        }

        private double _minheight;
        public double MinHeight
        {
            get { return _minheight; }
            set { _minheight = value; OnPropertyChanged(nameof(MinHeight)); }
        }

        private double _maxheight;
        public double MaxHeight
        {
            get { return _maxheight; }
            set { _maxheight = value; OnPropertyChanged(nameof(MaxHeight)); }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged(nameof(Width)); }
        }
        private double _minWidth;
        public double MinWidth
        {
            get { return _minWidth; }
            set { _minWidth = value; OnPropertyChanged(nameof(MinWidth)); }
        }

        private double _maxWidth;
        public double MaxWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; OnPropertyChanged(nameof(MaxWidth)); }
        }

        private double _lostFocusheight;
        public double LostFocusHeight
        {
            get { return _lostFocusheight; }
            set { _lostFocusheight = value; OnPropertyChanged(nameof(LostFocusHeight)); }
        }

        private double _lostFocusWidth;
        public double LostFocusWidth
        {
            get { return _lostFocusWidth; }
            set { _lostFocusWidth = value; OnPropertyChanged(nameof(LostFocusWidth)); }
        }
        public ICommand FocusCommand { get => CreateCommandParameter<object>(Focus); }

        public void Focus(object obj)
        {
            ResizableTextBoxZIndex = 8;
            ResizableGridZIndex = 7;
            ShadowVisibility = Visibility.Visible;
        }
        private ICommand? _lostFocusCommand;
        public ICommand LostFocusCommand => _lostFocusCommand ??= CreateCommandParameter<object>(LostFocus); 

        public void LostFocus(object obj)
        {
            ResizableTextBoxZIndex = 0;
            ResizableGridZIndex = 0;
            Keyboard.ClearFocus();
            Width = LostFocusWidth;
            Height = LostFocusHeight;
            ShadowVisibility = Visibility.Collapsed;
        } 

        public ICommand ResizeTextBoxCommand { get => CreateCommandParameter<object>(Resize); }

        public void Resize(object sender)
        {
            var e = sender as DragDeltaEventArgs;
            if (e != null)
            {
                Width = Math.Max(MinWidth, Math.Min(MaxWidth, Width + e.HorizontalChange));
                Height = Math.Max(MinHeight, Math.Min(MaxHeight, Height + e.VerticalChange));
            }
        } 
        private bool CanRegister(object parameter) => true;
    }
}
