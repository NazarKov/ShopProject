using ShopProject.DataBase.Model;
using ShopProject.Model;
using ShopProject.Model.Command;
using ShopProject.Model.ToolsPage;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.ToolsPage
{
    internal class UpdateGoodsRangeViewModel : ViewModel<UpdateGoodsRangeViewModel>
    {
        private UpdateGoodsRangeModel _model;
        private ICommand _upateGoodsDataBaseCommand;

        public UpdateGoodsRangeViewModel()
        {
            _model = new UpdateGoodsRangeModel();
            _upateGoodsDataBaseCommand = new DelegateCommand(UpdateProduct);
            
            _goodsList = new List<Goods>();
            _goodsUnits = new List<string>();
            _goodsCodeUKTZED = new List<string>();

            GoodsUnits = new List<string>();
            GoodsUnits = _model.GetUnitList();
            GoodsCodeUKTZED = _model.GetCodeUKTZEDList();

            SetFieldDataGrid();
        }

        private void SetFieldDataGrid()
        {
            if(StaticResourse.goodsList !=null)
                GoodsList = StaticResourse.goodsList;
        }

        private List<string> _goodsUnits;
        public List<string> GoodsUnits
        {
            get { return _goodsUnits; }
            set { _goodsUnits = value; OnPropertyChanged("GoodsUnits"); }
        }

        private List<string> _goodsCodeUKTZED;
        public List<string> GoodsCodeUKTZED
        {
            get { return _goodsCodeUKTZED; }
            set { _goodsCodeUKTZED = value; OnPropertyChanged("GoodsUnits"); }
        }

        private List<Goods> _goodsList;
        public List<Goods> GoodsList
        {
            get { return _goodsList; }
            set { _goodsList = value; OnPropertyChanged("GoodsList"); }
        }

        public ICommand ExitWindowCommand { get => new DelegateParameterCommand(WindowClose, CanRegister); }
        private void WindowClose(object parameter)
        {
            Window? window = parameter as Window;
            window?.Close();
        }
        private bool CanRegister(object parameter) => true;

        public ICommand UpateGoodsDataBaseCommand => _upateGoodsDataBaseCommand;
        private void UpdateProduct()
        {
            if (_model.UpdateGoods(GoodsList))
            {
                MessageBox.Show("Товари редаговано","Інформація",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

    }
}
