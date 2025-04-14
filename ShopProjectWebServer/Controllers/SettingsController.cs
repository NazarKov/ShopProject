using Microsoft.AspNetCore.Mvc;
using ShopProjectWebServer.Controllers.Attribute;

namespace ShopProjectWebServer.Controllers
{
    public class SettingsController : Controller
    {
        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
