using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.DataBase.Model;
using ShopProjectWebServer.Controllers.Attribute;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models;
using System.Data.Entity.Infrastructure;

namespace ShopProjectWebServer.Controllers
{
    public class ProfileController : Controller
    {
        private static ProfileViewModel _model { get; set; } 


        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new ProfileViewModel();
            } 
            var user = HttpContext.Session.GetObject<UserEntity>(nameof(UserEntity));
            if (user == null)
            {
                return RedirectToAction("Index", "Authorization");
            }

            var item = DataBaseMainController.DataBaseAccess.UserTable.GetAll().Where(U => U.ID == user.ID).First();

            _model.Login = item.Login;
            _model.Password = item.Password;
            _model.Email = item.Email;
            _model.Role = item.UserRole.NameRole;
            _model.Rule = "Empty";
            return View(_model);
        }
        [HttpPost]
        [LayoutInjecter("_AdminLayout")]
        public IActionResult ChangePassword()
        {
            _model.Error = string.Empty;
            return View(_model);
        }

        [HttpGet]
        [LayoutInjecter("_AdminLayout")]
        public IActionResult ChangePassword(ProfileViewModel model)
        {
            try
            {
                var user = HttpContext.Session.GetObject<UserEntity>(nameof(UserEntity));
                if (user == null)
                {
                    return RedirectToAction("Index", "Authorization");
                }
                var item = DataBaseMainController.DataBaseAccess.UserTable.GetAll().Where(U => U.ID == user.ID).First();
                if (item.Password == model.OldPassword)
                {
                    DataBaseMainController.DataBaseAccess.UserTable.UpdateParameter(item.ID,nameof(item.Password),model.Password);
                }
                else
                {
                    throw new Exception("Невірний пароль");
                }


                _model.Error = "Пароль зміненно";
                HttpContext.Session.SetObject(nameof(UserEntity), item);
                return View(_model);
            }
            catch (Exception ex)
            {
                _model.Error = ex.Message;
                return View(_model);
            }
        }

        [HttpPost]
        [LayoutInjecter("_AdminLayout")]
        public IActionResult ChangeData()
        {
            _model.Error = string.Empty;
            return View(_model);
        }

        [HttpGet]
        [LayoutInjecter("_AdminLayout")]
        public IActionResult ChangeData(ProfileViewModel model)
        {
            try
            {
                var user = HttpContext.Session.GetObject<UserEntity>(nameof(UserEntity));
                if (user == null)
                {
                    return RedirectToAction("Index", "Authorization");
                }

                var item = DataBaseMainController.DataBaseAccess.UserTable.GetAll().Where(U => U.Login == user.Login).First();

                if (item != null)
                {
                    if (model.Login != string.Empty)
                    {
                        DataBaseMainController.DataBaseAccess.UserTable.UpdateParameter(item.ID, nameof(item.Login), model.Login);
                    }

                    if (model.Email != string.Empty)
                    {
                        DataBaseMainController.DataBaseAccess.UserTable.UpdateParameter(item.ID, nameof(item.Email), model.Email);
                    }
                }
                _model.Error = "Дані зміненно";

                HttpContext.Session.SetObject(nameof(UserEntity), item);
                return View(_model);
            }
            catch (Exception ex)
            {
                _model.Error = ex.Message;
                return View(_model);
            }
        }



    }
}
