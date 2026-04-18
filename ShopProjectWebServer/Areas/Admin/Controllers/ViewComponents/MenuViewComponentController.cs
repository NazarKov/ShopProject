using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models.Domain.User;
using ShopProjectWebServer.Models.ViewComponents;

namespace ShopProjectWebServer.Areas.Admin.Controllers.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "Menu")]
    public class MenuViewComponentController : ViewComponent
    {
        public MenuComponentViewModel _model;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_model == null)
            {
                _model = new MenuComponentViewModel();
            }
            _model.Login = HttpContext.Session.GetObject<User>(nameof(User)).Login;
            return View("Index",_model);
        }    
    }
}
