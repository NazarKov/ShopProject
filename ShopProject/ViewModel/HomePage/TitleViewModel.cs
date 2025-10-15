using ShopProject.Model.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.ViewModel.HomePage
{
    internal class TitleViewModel : ViewModel<TitleViewModel>
    {
        private TitleModel _model;
        public TitleViewModel() 
        {
            _model = new TitleModel();
        }
    }
}
