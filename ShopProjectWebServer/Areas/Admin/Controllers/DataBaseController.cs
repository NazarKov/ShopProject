using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Areas.Admin.Controllers.Attribute;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.Models.UI;

namespace ShopProjectWebServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DataBaseController : Controller
    {
        private static DataBaseModel _model;
        private IDataBaseService _dataBaseService;

        public DataBaseController(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new DataBaseModel();
            }

            _model.SettingDataBaseConnection = _dataBaseService.GetSetting();
            return View(_model);
        } 
    }
}
