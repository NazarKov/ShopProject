using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Controllers.Attribute;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Models;

namespace ShopProjectWebServer.Controllers
{
    public class DataBaseController : Controller
    {
        private static DataBaseViewModel _model; 

        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new DataBaseViewModel();
            }
             
            _model.DataBase = DataBaseMainController.GetInfo();
            return View(_model);
        }
    }
}
