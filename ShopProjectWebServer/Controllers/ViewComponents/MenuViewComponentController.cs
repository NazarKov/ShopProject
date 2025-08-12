using Microsoft.AspNetCore.Mvc;
using ShopProjectSQLDataBase.Entities;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models.ViewComponents;

namespace ShopProjectWebServer.Controllers.ViewComponents
{
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
            _model.Login = HttpContext.Session.GetObject<UserEntity>(nameof(UserEntity)).Login;
            return View("Index",_model);
        }    
    }
}
