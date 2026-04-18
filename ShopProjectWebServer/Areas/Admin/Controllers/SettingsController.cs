using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Areas.Admin.Controllers.Attribute;

namespace ShopProjectWebServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            return View();
        } 
    }
}
