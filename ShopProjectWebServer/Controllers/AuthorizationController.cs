using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models;


namespace ShopProjectWebServer.Controllers
{
    public class AuthorizationController : Controller
    {
        public static AuthorizationViewModel _model { get; set; } 

        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new AuthorizationViewModel(); 
            }
            return View(_model);
        }

        public IActionResult Authorization(AuthorizationViewModel autorizanionViewMode)
        {
            try
            {
                var users = DataBaseMainController.DataBaseAccess.UserTable.GetAll();
                if (users != null)
                {
                    var user = users.Where(item => item.Login == autorizanionViewMode.Login).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Password == autorizanionViewMode.Password)
                        {
                            HttpContext.Session.SetObject(nameof(UserEntity), user);
                        }
                        else
                        {
                            throw new Exception("Неввірний пароль");
                        }
                    }
                    else
                    {
                        throw new Exception("Користувача не знайдено");
                    }
                }
                _model = null;
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception ex)
            {
                _model.Messege = ex.Message;
                return RedirectToAction("Index", "Authorization");
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
            return RedirectToAction("index", "Authorization");
        }
    }
}
