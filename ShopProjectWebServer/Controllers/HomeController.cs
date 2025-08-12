using Microsoft.AspNetCore.Mvc;
using ShopProjectSQLDataBase.Entities;
using ShopProjectWebServer.Controllers.Attribute;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models;
using System.Diagnostics;

namespace ShopProjectWebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetObject<UserEntity>(nameof(UserEntity));
            if (user == null)
            {
                return RedirectToAction("Index", "Authorization");
            }

            return View();
        }
        [LayoutInjecter("_AdminLayout")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}