using ShopProject.Helpers.Template.Paginator;
using ShopProject.Helpers.Command;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShopProject.ViewModel.TemplatePage
{
    internal class TemplatePaginatorButtonViewModel :ViewModel<TemplatePaginatorButtonViewModel>
    {
        private ICommand _clickButtonCommand;
        private ICommand _clickNavigateButtonCommand;

        private SolidColorBrush selectdedBrush;
        private SolidColorBrush clearBrush;

        private List<PaginatorButton> _paginatorButtons;
        private PaginatorButton _selectLastButton;
        private int _indexButton;

        public TemplatePaginatorButtonViewModel() 
        {

            _clickButtonCommand = new DelegateParameterCommand(Click);
            _clickNavigateButtonCommand = new DelegateParameterCommand(ClickNavigateButton);
            _paginatorButtons = new List<PaginatorButton>(); 
            _paginatorButtonLast = new PaginatorButton();
            _selectLastButton = new PaginatorButton();
            selectdedBrush = new SolidColorBrush(Colors.CadetBlue);
            clearBrush = new SolidColorBrush(Colors.White); 

            _paginatorButtonFirst = new PaginatorButton();
            _paginatorButtonSecond = new PaginatorButton();
            _paginatorButtonThird = new PaginatorButton();
            _paginatorButtonQuarter = new PaginatorButton();
            _paginatorButtonFifth = new PaginatorButton();
            _paginatorButtonSixth = new PaginatorButton();  
        } 
        private int _countColumn;
        public int CountColumn
        {
            get { return _countColumn; }
            set { _countColumn = value; }
        }

        private int _countButton;
        public int CountButton
        {
            get { return _countButton; }
            set { _countButton = value; SetPagginatorButton(_countButton); }
        }

        private Action<int, int> _callback;
        public Action<int, int> Callback 
        {
            get { return _callback; }
            set { _callback = value; }
        }

        private PaginatorButton _paginatorButtonFirst;
        public PaginatorButton PaginatorButtonFirst
        {
            get { return _paginatorButtonFirst; }
            set { _paginatorButtonFirst = value; OnPropertyChanged(nameof(PaginatorButtonFirst)); }
        }
        private PaginatorButton _paginatorButtonSecond;
        public PaginatorButton PaginatorButtonSecond
        {
            get { return _paginatorButtonSecond; }
            set { _paginatorButtonSecond = value; OnPropertyChanged(nameof(PaginatorButtonSecond)); }
        }
        private PaginatorButton _paginatorButtonThird;
        public PaginatorButton PaginatorButtonThird
        {
            get { return _paginatorButtonThird; }
            set { _paginatorButtonThird = value; OnPropertyChanged(nameof(PaginatorButtonThird)); }
        }
        private PaginatorButton _paginatorButtonQuarter;
        public PaginatorButton PaginatorButtonQuarter
        {
            get { return _paginatorButtonQuarter; }
            set { _paginatorButtonQuarter = value; OnPropertyChanged(nameof(PaginatorButtonQuarter)); }
        }
        private PaginatorButton _paginatorButtonFifth;
        public PaginatorButton PaginatorButtonFifth
        {
            get { return _paginatorButtonFifth; }
            set { _paginatorButtonFifth = value; OnPropertyChanged(nameof(PaginatorButtonFifth)); }
        }
        private PaginatorButton _paginatorButtonSixth;
        public PaginatorButton PaginatorButtonSixth
        {
            get { return _paginatorButtonSixth; }
            set { _paginatorButtonSixth = value; OnPropertyChanged(nameof(PaginatorButtonSixth)); }
        }

        private PaginatorButton _paginatorButtonLast;
        public PaginatorButton PaginatorButtonLast
        {
            get { return _paginatorButtonLast; }
            set { _paginatorButtonLast = value; OnPropertyChanged(nameof(PaginatorButtonLast)); }
        }

        private Visibility _visibilitiButtonFirst;
        public Visibility VisibilitiButtonFirst
        {
            get { return _visibilitiButtonFirst; }
            set { _visibilitiButtonFirst = value; OnPropertyChanged(nameof(VisibilitiButtonFirst)); }
        }
        private Visibility _visibilitiButtonSecond;
        public Visibility VisibilitiButtonSecond
        {
            get { return _visibilitiButtonSecond; }
            set { _visibilitiButtonSecond = value; OnPropertyChanged(nameof(VisibilitiButtonSecond)); }
        }
        private Visibility _visibilitiButtonThird;
        public Visibility VisibilitiButtonThird
        {
            get { return _visibilitiButtonThird; }
            set { _visibilitiButtonThird = value; OnPropertyChanged(nameof(VisibilitiButtonThird)); }
        }
        private Visibility _visibilitiButtonQuarter;
        public Visibility VisibilitiButtonQuarter
        {
            get { return _visibilitiButtonQuarter; }
            set { _visibilitiButtonQuarter = value; OnPropertyChanged(nameof(VisibilitiButtonQuarter)); }
        }
        private Visibility _visibilitiButtonFifth;
        public Visibility VisibilitiButtonFifth
        {
            get { return _visibilitiButtonFifth; }
            set { _visibilitiButtonFifth = value; OnPropertyChanged(nameof(VisibilitiButtonFifth)); }
        }
        private Visibility _visibilitiButtonSixth;
        public Visibility VisibilitiButtonSixth
        {
            get { return _visibilitiButtonSixth; }
            set { _visibilitiButtonSixth = value; OnPropertyChanged(nameof(VisibilitiButtonSixth)); }
        }
        private Visibility _visibilitiButtonLast;
        public Visibility VisibilitiButtonLast
        {
            get { return _visibilitiButtonLast; }
            set { _visibilitiButtonLast = value; OnPropertyChanged(nameof(VisibilitiButtonLast)); }
        }

        private Visibility _visibilitySeparator1;
        public Visibility VisibilitySeparator1
        {
            get { return _visibilitySeparator1; }
            set { _visibilitySeparator1 = value; OnPropertyChanged(nameof(VisibilitySeparator1)); }
        }

        private Visibility _visibilitySeparator2;
        public Visibility VisibilitySeparator2
        {
            get { return _visibilitySeparator2; }
            set { _visibilitySeparator2 = value; OnPropertyChanged(nameof(VisibilitySeparator2)); }
        }


        private void SetPagginatorButton(int countButton)
        {
            _indexButton = 0;
            _paginatorButtons.Clear();
            if (countButton != 0)
            {
                for (int i = 1; i <= countButton; i++)
                {
                    _paginatorButtons.Add(new PaginatorButton() { Name = i.ToString(), Content = i.ToString(), Background = clearBrush });
                }
                _paginatorButtons[0].Background = selectdedBrush;
                _selectLastButton = _paginatorButtons[0];
                SetButton(_indexButton);
            }
            SetVisibilitiButonAndSeparator();
        }

        private void SetButton(int index)
        {
            var count = _paginatorButtons.Count;
            if (count == 1)
            {
                PaginatorButtonFirst = _paginatorButtons[0];
            }
            else if (count == 2)
            {
                PaginatorButtonFirst = _paginatorButtons[0];
                PaginatorButtonSecond = _paginatorButtons[index + 1];
            }
            else if (count == 3)
            {
                PaginatorButtonFirst = _paginatorButtons[0];
                PaginatorButtonSecond = _paginatorButtons[index + 1];
                PaginatorButtonThird = _paginatorButtons[index + 2];
            }
            else if (count == 4)
            {
                PaginatorButtonFirst = _paginatorButtons[0];
                PaginatorButtonSecond = _paginatorButtons[index + 1];
                PaginatorButtonThird = _paginatorButtons[index + 2];
                PaginatorButtonQuarter = _paginatorButtons[index + 3];
            }
            else if (count == 5)
            {
                PaginatorButtonFirst = _paginatorButtons[0];
                PaginatorButtonSecond = _paginatorButtons[index + 1];
                PaginatorButtonThird = _paginatorButtons[index + 2];
                PaginatorButtonQuarter = _paginatorButtons[index + 3];
                PaginatorButtonFifth = _paginatorButtons[index + 4];
            }
            else if (count == 6)
            {
                PaginatorButtonFirst = _paginatorButtons[0];
                PaginatorButtonSecond = _paginatorButtons[index + 1];
                PaginatorButtonThird = _paginatorButtons[index + 2];
                PaginatorButtonQuarter = _paginatorButtons[index + 3];
                PaginatorButtonFifth = _paginatorButtons[index + 4];
                PaginatorButtonSixth = _paginatorButtons[index + 5];
            }
            else
            {
                PaginatorButtonFirst = _paginatorButtons[0];
                PaginatorButtonSecond = _paginatorButtons[index + 1];
                PaginatorButtonThird = _paginatorButtons[index + 2];
                PaginatorButtonQuarter = _paginatorButtons[index + 3];
                PaginatorButtonFifth = _paginatorButtons[index + 4];
                PaginatorButtonSixth = _paginatorButtons[index + 5];
                PaginatorButtonLast = _paginatorButtons.Last();
            } 
        }

        private void SetVisibilitiButonAndSeparator()
        {
            VisibilitiButtonFirst = Visibility.Hidden;
            VisibilitiButtonSecond = Visibility.Hidden;
            VisibilitiButtonThird = Visibility.Hidden;
            VisibilitiButtonQuarter = Visibility.Hidden;
            VisibilitiButtonFifth = Visibility.Hidden;
            VisibilitiButtonSixth = Visibility.Hidden;
            VisibilitiButtonLast = Visibility.Hidden;

            VisibilitySeparator1 = Visibility.Hidden;
            VisibilitySeparator2 = Visibility.Hidden;

            var count = _paginatorButtons.Count;

            if (count != 0)
            {
                if (count == 1)
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                }
                else if (count == 2)
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                    VisibilitiButtonSecond = Visibility.Visible;
                }
                else if (count == 3)
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                    VisibilitiButtonSecond = Visibility.Visible;
                    VisibilitiButtonThird = Visibility.Visible;
                }
                else if (count == 4)
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                    VisibilitiButtonSecond = Visibility.Visible;
                    VisibilitiButtonThird = Visibility.Visible;
                    VisibilitiButtonQuarter = Visibility.Visible;
                }
                else if (count == 5)
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                    VisibilitiButtonSecond = Visibility.Visible;
                    VisibilitiButtonThird = Visibility.Visible;
                    VisibilitiButtonQuarter = Visibility.Visible;
                    VisibilitiButtonFifth = Visibility.Visible;
                }
                else if (count == 6)
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                    VisibilitiButtonSecond = Visibility.Visible;
                    VisibilitiButtonThird = Visibility.Visible;
                    VisibilitiButtonQuarter = Visibility.Visible;
                    VisibilitiButtonFifth = Visibility.Visible;
                    VisibilitiButtonSixth = Visibility.Visible;
                }
                else
                {
                    VisibilitiButtonFirst = Visibility.Visible;
                    VisibilitiButtonSecond = Visibility.Visible;
                    VisibilitiButtonThird = Visibility.Visible;
                    VisibilitiButtonQuarter = Visibility.Visible;
                    VisibilitiButtonFifth = Visibility.Visible;
                    VisibilitiButtonSixth = Visibility.Hidden;
                    VisibilitiButtonLast = Visibility.Visible;

                    VisibilitySeparator1 = Visibility.Hidden;
                    VisibilitySeparator2 = Visibility.Visible;
                } 
            }
        }


        public ICommand ClickButtonCommand => _clickButtonCommand;
        private void Click(object parameter)
        {  
            var name = parameter as string;
            for (int i = 0; i < _paginatorButtons.Count; i++)
            {
                if (_paginatorButtons[i].Name == name)
                { 
                    ClearBackground();
                    _paginatorButtons[i].Background = selectdedBrush;
                    if (i == 0)
                    {
                        _indexButton = 0; 
                    }
                    else if (i == _paginatorButtons.Count - 1)
                    {
                        if (i > 7)
                        {
                            _indexButton = _paginatorButtons.Count - 7;
                        }
                    }
                    else
                    { 
                        if (i < _paginatorButtons.Count - 1)
                        {
                            PaginatorButtonLast = _paginatorButtons.Last();
                        }

                        if (int.Parse(_selectLastButton.Name) < int.Parse(_paginatorButtons[i].Name))
                        {
                            if (i > 3)
                            {
                                if (_indexButton < _paginatorButtons.Count - 7)
                                {
                                    _indexButton++;
                                }
                            }
                        }

                        if (int.Parse(_selectLastButton.Name) > int.Parse(_paginatorButtons[i].Name))
                        {
                            if (i < _paginatorButtons.Count - 4)
                            {
                                if (_indexButton > 0)
                                {
                                    _indexButton--;
                                }
                            }
                        }
                    }
                    _selectLastButton = _paginatorButtons[i]; 
                }

            }
            SetButton(_indexButton);
            AddSeparator();
            Callback(CountColumn, int.Parse(name));
        }

        public ICommand ClickNavigateButtonCommand => _clickNavigateButtonCommand;
        private void ClickNavigateButton(object parameter)
        {
            var name = Enum.Parse(typeof(SelectButton), parameter.ToString()); 
            switch (name)
            {
                case SelectButton.buttonBeginning:
                    {
                        ClearBackground();
                        _indexButton = 0;
                        _paginatorButtons[0].Background = selectdedBrush;
                        _selectLastButton = _paginatorButtons[0];
                        Callback(CountColumn, 1);
                        break;
                    }
                case SelectButton.buttonNext:
                    { 
                        for (int i = 0; i < _paginatorButtons.Count;i++ )
                        { 

                            if (_paginatorButtons[i].Background is SolidColorBrush solidColorBrush)
                            {
                                if (solidColorBrush.Color == selectdedBrush.Color)
                                {
                                    if (i != _paginatorButtons.Count-1)
                                    {
                                        _paginatorButtons[i].Background = clearBrush;
                                        if (i >= _paginatorButtons.Count - 1)
                                        {
                                            _selectLastButton = _paginatorButtons[i];
                                            _paginatorButtons[i].Background = selectdedBrush;
                                            Callback(CountColumn, i);
                                            break;
                                        }
                                        _selectLastButton = _paginatorButtons[i+1];
                                        _paginatorButtons[i + 1].Background = selectdedBrush;
                                        Callback(CountColumn, i + 2);
                                        if (i > 2)
                                        {
                                            if (_indexButton < _paginatorButtons.Count - 7)
                                            {
                                                _indexButton++;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }  
                        }
                        break;
                    }
                case SelectButton.buttonBack:
                    {
                        for (int i = _paginatorButtons.Count-1; i > 0; i--)
                        { 

                            if (_paginatorButtons[i].Background is SolidColorBrush solidColorBrush)
                            {
                                if (solidColorBrush.Color == selectdedBrush.Color)
                                {
                                    _paginatorButtons[i].Background = clearBrush;
                                    if (i <= 0)
                                    {
                                        _selectLastButton = _paginatorButtons[i];
                                        _paginatorButtons[i].Background = selectdedBrush;
                                        Callback(CountColumn, i);
                                        break;
                                    }
                                    _selectLastButton = _paginatorButtons[i-1];
                                    _paginatorButtons[i - 1].Background = selectdedBrush;
                                    Callback(CountColumn, i);
                                    if (i < _paginatorButtons.Count-3)
                                    {
                                        if (_indexButton > 0)
                                        {
                                            _indexButton--; 
                                        }
                                    }
                                    break;
                                }
                            } 
                        }
                        break;
                    }
                case SelectButton.buttonEnd:
                    {
                        ClearBackground();
                        _indexButton = _paginatorButtons.Count - 7;
                        _paginatorButtons[_paginatorButtons.Count - 1].Background = selectdedBrush;
                        _selectLastButton = _paginatorButtons[_paginatorButtons.Count - 1];
                        Callback(CountColumn, _paginatorButtons.Count);

                        if (_paginatorButtons.Count < 7)
                        {
                            _indexButton = 0;
                        }
                        break;
                    }
            } 
            SetButton(_indexButton);  
            AddSeparator();
        }

        private void AddSeparator()
        {
            if (_paginatorButtons.Count > 7)
            {

            if (_indexButton > 0)
            {
                VisibilitiButtonSecond = Visibility.Hidden; 
                VisibilitySeparator1 = Visibility.Visible;
            }

            if (_indexButton < 1)
            {
                VisibilitiButtonSecond = Visibility.Visible;
                VisibilitySeparator1 = Visibility.Hidden;
            }

            if (_indexButton > _paginatorButtons.Count - 8)
            {
                VisibilitiButtonSixth = Visibility.Visible;
                VisibilitySeparator2 = Visibility.Hidden;
            }

            if (_indexButton < _paginatorButtons.Count - 7)
            {
                VisibilitiButtonSixth = Visibility.Hidden;
                VisibilitySeparator2 = Visibility.Visible;
            }

            }
        }

        private void ClearBackground()
        {
            foreach (var button in _paginatorButtons)
            {
                button.Background = clearBrush;
            }
            PaginatorButtonLast.Background = clearBrush;
             
        }

        
         
    }
}
