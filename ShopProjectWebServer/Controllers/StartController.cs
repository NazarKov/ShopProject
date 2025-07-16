using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.Models;

namespace ShopProjectWebServer.Controllers
{
    public class StartController : Controller
    {
        private static StartViewModel _model; 

        [HttpGet]
        public IActionResult Index()
        {
            if (_model == null)
            {
                SetFieldPage();
            }
              
            if (DataBaseMainController.DataBaseAccess == null)
            {
                return View(_model);
            }

            var count = DataBaseMainController.DataBaseAccess.UserTable.GetAll().ToList().Count();
            if (count > 0)
            {
                return RedirectToAction("Index", "Authorization");
            }
            else
            {
                return View(_model);
            }
        }

        private void SetFieldPage()
        {
            _model = new StartViewModel();

            _model.TypeDataBase = new List<SelectListItem>();
            _model.TypeConnectDataBase = new List<SelectListItem>();

            Array arrayDataBaseType = Enum.GetValues(typeof(TypeDataBase));
            for (int i = 0; i < arrayDataBaseType.Length; i++)
            {
                _model.TypeDataBase.Add(new SelectListItem { Text = arrayDataBaseType.GetValue(i).ToString(), Value = i.ToString() });
            }

            Array arrayDataBaseConnectionType = Enum.GetValues(typeof(TypeConnectDataBase));
            for (int i = 0; i < arrayDataBaseConnectionType.Length; i++)
            {
                _model.TypeConnectDataBase.Add(new SelectListItem { Text = arrayDataBaseConnectionType.GetValue(i).ToString(), Value = i.ToString() });
            }
        }

        [HttpPost]
        public ActionResult RedirectToAuthorization()
        {
            return RedirectToAction("Index", "Start");
        }

        [HttpPost]
        public IActionResult CreateDataBase(StartViewModel model)
        {

            try
            {

                if (model.NameDataBase == null)
                {
                    throw new Exception("Не заповнене поле назва бази даних");
                }

                if (model.TypeDataBaseSelectItems == "0")
                {
                    throw new Exception("Невибраний тип бази даних");
                }
                if (model.TypeDataBaseSelectItems == "0")
                {
                    if (model.TypeConnectDataBaseSelectItems == "0")
                    {
                        throw new Exception("Невибраний тип підключення");
                    }
                }
                if (model.PasswordDataBase == null)
                {
                    throw new Exception("Ведіть пароль");
                }
                DataBaseMainController.Create(model.NameDataBase, model.PasswordDataBase, model.TypeDataBaseSelectItems, model.TypeConnectDataBaseSelectItems);
                _model.MessegeCreateDataBase = "База даних створена";
                _model.TypeDataBaseSelectItems = model.TypeDataBaseSelectItems;
                _model.TypeConnectDataBaseSelectItems = model.TypeConnectDataBaseSelectItems;
                _model.NameDataBase = model.NameDataBase;
                _model.PasswordDataBase = model.PasswordDataBase;
            }
            catch (Exception ex)
            {
                SetFieldPage();
                _model.MessegeErrorCreateDataBase = ex.Message;
            }
            return RedirectToAction("Index", "Start");
        }
    }
}
