using Microsoft.AspNetCore.Mvc;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.Areas.Admin.Controllers.Attribute;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.Helpers;
using ShopProjectWebServer.Models.Domain.User;
using ShopProjectWebServer.Models.Exceptions;
using ShopProjectWebServer.Models.UI;
using ShopProjectWebServer.Services.Modules.Domain.User.Interface;

namespace ShopProjectWebServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private static ProfileModel _model { get; set; } = new ProfileModel();
        private IUserService  _userService;
        public ProfileController(IUserService service)
        {
            _userService = service;
        }

        [LayoutInjecter("_AdminLayout")]
        public IActionResult Index()
        {
            if (_model == null)
            {
                _model = new ProfileModel();
            } 
            var user = HttpContext.Session.GetObject<User>(nameof(User));
            if (user == null)
            {
                return RedirectToAction("Index", "Authorization", new { area = "Admin" });
            }  
             
            _model.Login = user.Login;
            _model.Password = "*******";
            _model.Email = user.Email;
            _model.Role = user.UserRole.NameRole; 
            return View(_model);
        }
        [HttpPost]
        [LayoutInjecter("_AdminLayout")]
        public IActionResult ChangePassword()
        { 
            return View(_model);
        }

        [HttpPost]
        [LayoutInjecter("_AdminLayout")]
        public IActionResult ChangeUserPassword(ProfileModel model)
        {
            try
            {
                var user = HttpContext.Session.GetObject<User>(nameof(User));
                if (user == null)
                {
                    return RedirectToAction("Index", "Authorization");
                }
                var item = _userService.GetUserById(user.Token, user.ID.ToString());
                 
                if(model.OldPassword == null || model.OldPassword == string.Empty)
                {
                    throw new EmptyFieldException("Заповніть поле старий пароль");
                }
                if (model.Password == null || model.Password == string.Empty)
                {
                    throw new EmptyFieldException("Заповніть поле пароль");
                }

                if (item.Password == model.OldPassword)
                { 
                    _userService.UpdateParameterUser(item.Token,item.ID.ToString(),nameof(item.Password),model.Password);
                }
                else
                {
                    throw new ChangePasswordException("Невірний пароль");
                } 

                item.UserRole = user.UserRole;

                _model.Error = "Пароль зміненно";
                HttpContext.Session.SetObject(nameof(User), item);
                return View("ChangePassword", _model);
            }
            catch(ChangePasswordException ce)
            {
                _model.Error = ce.Message;
                return View("ChangePassword", _model);
            }
            catch(EmptyFieldException ef)
            {
                _model.Error = ef.Message;
                return View("ChangePassword", _model);
            }
            catch (Exception)
            { 
                throw;
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
        public IActionResult ChangeData(ProfileModel model)
        {
            try
            {
                var user = HttpContext.Session.GetObject<User>(nameof(User));
                if (user == null)
                {
                    return RedirectToAction("Index", "Authorization");
                }

                var item = _userService.GetUserById(user.Token,user.ID.ToString());

                if (item != null)
                {
                    if (model.Login != string.Empty)
                    {
                        _userService.UpdateParameterUser(user.Token,item.ID.ToString(), nameof(item.Login), model.Login);
                    }

                    if (model.Email != string.Empty)
                    {
                        _userService.UpdateParameterUser(user.Token, item.ID.ToString(), nameof(item.Email), model.Email); 
                    }

                    item.UserRole = user.UserRole;
                }
                _model.Error = "Дані зміненно";

                HttpContext.Session.SetObject(nameof(User), item);
                return View(_model);
            }
            catch (Exception)
            { 
                throw;
            }
        }



    }
}
