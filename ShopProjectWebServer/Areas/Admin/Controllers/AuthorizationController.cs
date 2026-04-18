using Microsoft.AspNetCore.Mvc; 
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models.Exceptions;
using ShopProjectWebServer.Models.UI;
using ShopProjectWebServer.Services.Modules.Domain.User;
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;
using ShopProjectWebServer.Services.Modules.Mapping;


namespace ShopProjectWebServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorizationController : Controller
    {
        public static AuthorizationModel _model { get; set; } = new AuthorizationModel();
        private IUserService _userService;

        public AuthorizationController(IUserService service)
        {
            _userService = service; 
        }
        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new AuthorizationModel(); 
            }
            return View(_model);
        }

        public IActionResult Authorization(AuthorizationModel autorizanionViewMode)
        {
            try
            {
                _model.Login = autorizanionViewMode.Login; 
                var user = _userService.Authorization(autorizanionViewMode.Login, autorizanionViewMode.Password, Environment.MachineName); 
                if (user != null)
                {
                    HttpContext.Session.SetObject(nameof(ShopProjectWebServer.Models.Domain.User.User), user);
                    return RedirectToAction("Index", "Profile"); 
                }  
                return RedirectToAction("Index", "Authorization");
            }
            catch(AuthorizationException ae)
            {
                _model.Message = ae.Message;
                return RedirectToAction("Index", "Authorization");
            }
            catch(EmptyFieldException ef)
            {
                _model.Message = ef.Message;
                return RedirectToAction("Index", "Authorization");
            }
            catch (Exception ex)
            {
                _model.Message = ex.Message;
                throw; 
            }
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult UpdatePassword()
        {
            return View();
        }

        public IActionResult RedirectToResetPassword()
        {
            return RedirectToAction("ResetPassword", "Authorization");
        }

        public IActionResult RedirectToUpdatePassword()
        {
            return RedirectToAction("UpdatePassword", "Authorization");
        }

        public IActionResult RedirectToAuthorization()
        {
            return RedirectToAction("Index", "Authorization");
        }
    }
}
