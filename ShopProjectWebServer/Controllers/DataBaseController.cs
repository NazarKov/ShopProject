using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProjectWebServer.Controllers.Attribute;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.Models;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Controllers
{
    public class DataBaseController : Controller
    {
        private static DataBaseViewModel _model;
        private DataBaseMainController _controller;

        public DataBaseController(DataBaseMainController controller)
        {
            _controller = controller;
        }
        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new DataBaseViewModel();
            }
             
            _model.DataBase = _controller.GetInfo();
            return View(_model);
        } 
    }
}
