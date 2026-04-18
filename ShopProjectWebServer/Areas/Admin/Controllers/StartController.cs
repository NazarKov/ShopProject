using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Helpers;
using ShopProjectWebServer.Models.UI;
using System.Threading.Tasks;

namespace ShopProjectWebServer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StartController : Controller
    {
        private static StartModel _model;
        private DataBaseService _controller;
        public StartController(DataBaseService controller)
        {
            _controller = controller;
        }
        [HttpGet]
        public IActionResult Index()
        { 
            if (_model == null)
            {
                SetFieldPage();
            }

            if (_controller.DataBaseAccess == null)
            {
                return View(_model);
            }

            if (_controller.DataBaseAccess != null && _controller.DataBaseAccess.UserTable == null)
            {
                return View(_model);
            }

            var count = _controller.DataBaseAccess.UserTable.GetAll().ToList().Count();
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
            _model = new StartModel();

            _model.TypeDataBase = new List<SelectListItem>();
            _model.TypeConnectDataBase = new List<SelectListItem>();
            _model.TypeAuthorizationDataBase = new List<SelectListItem>();
            _model.VisibilitiCreateDataBaseform = 0;
            _model.VisibilitiAutorizationDataBaseform = 0;
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

            Array arrayDataBaseAuthorizationType = Enum.GetValues(typeof(TypeAuthorizationDataBase));
            for (int i = 0; i < arrayDataBaseAuthorizationType.Length; i++)
            {
                _model.TypeAuthorizationDataBase.Add(new SelectListItem { Text = arrayDataBaseAuthorizationType.GetValue(i).ToString(), Value = i.ToString() });
            }

        }

        [HttpPost]
        public ActionResult RedirectToAuthorization()
        {
            return RedirectToAction("Index", "Start");
        }

        public async Task<IActionResult> LoginDataBase(StartModel model)
        {
            try
            {
                if (model.TypeAuthorizationDataBaseSelectItems == null)
                {
                    throw new Exception("Невибраний спосіб авторизації");
                }

                TypeAuthorizationDataBase typeAuthorization = Enum.Parse<TypeAuthorizationDataBase>(model.TypeAuthorizationDataBaseSelectItems);

                if(typeAuthorization == TypeAuthorizationDataBase.SQLAuthorization)
                {
                    if (model.Login == null)
                    {
                        throw new Exception("Не заповнене поле Login");
                    }

                    if (model.Password == "0")
                    {
                        throw new Exception("Не заповнене поле Password");
                    }
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

                bool isConnect = false;

                switch (typeAuthorization)
                {
                    case TypeAuthorizationDataBase.WindowsAuthorization:
                        {
                            isConnect = await _controller.CreateConnectionSql(Enum.Parse<TypeDataBase>(model.TypeDataBaseSelectItems), ConnectionString.CreateConnectionString(
                                string.Empty,
                                string.Empty,
                                Enum.Parse<TypeConnectDataBase>(model.TypeConnectDataBaseSelectItems),
                                string.Empty
                                ));
                            break;
                        }
                    case TypeAuthorizationDataBase.SQLAuthorization:
                        {
                            isConnect = await _controller.CreateConnectionSql(Enum.Parse<TypeDataBase>(model.TypeDataBaseSelectItems), ConnectionString.CreateConnectionString(
                                model.Login,
                                model.Password,
                                Enum.Parse<TypeConnectDataBase>(model.TypeConnectDataBaseSelectItems),
                                "master"
                                ));
                            break;
                        }
                }  
                if (isConnect)
                { 
                    model.TypeDataBase = _model.TypeDataBase;
                    model.TypeConnectDataBase = _model.TypeConnectDataBase;
                    model.TypeAuthorizationDataBase = _model.TypeAuthorizationDataBase; 
                    _model = model;
                    _model.VisibilitiCreateDataBaseform = 1;
                    _model.VisibilitiAutorizationDataBaseform = 1;
                    model.Messege = "Авторизація успішна"; 
                    return RedirectToAction("Index", "Start");
                }
                else
                {
                    throw new Exception("Невдалося авторизуватися, невірний логін чи пароль");
                }
            }
            catch (Exception ex)
            {
                throw; 
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDataBase(StartModel model)
        {

            try
            {

                if (model.NameDataBase == null)
                {
                    throw new Exception("Не заповнене поле назва бази даних");
                }

                if (_model.TypeDataBaseSelectItems == "0")
                {
                    throw new Exception("Невибраний тип бази даних");
                }
                if (_model.TypeDataBaseSelectItems == "0")
                {
                    if (_model.TypeConnectDataBaseSelectItems == "0")
                    {
                        throw new Exception("Невибраний тип підключення");
                    }
                }
                if (model.PasswordUser == null)
                {
                    throw new Exception("Ведіть пароль");
                }

                TypeAuthorizationDataBase typeAuthorization = Enum.Parse<TypeAuthorizationDataBase>(_model.TypeAuthorizationDataBaseSelectItems);
                ConnectionString connectionString = new ConnectionString();

                switch (typeAuthorization)
                {
                    case TypeAuthorizationDataBase.WindowsAuthorization:
                        {
                            connectionString =  ConnectionString.CreateConnectionString(
                                string.Empty,
                                string.Empty,
                                Enum.Parse<TypeConnectDataBase>(_model.TypeConnectDataBaseSelectItems),
                                string.Empty
                                );
                            break;
                        }
                    case TypeAuthorizationDataBase.SQLAuthorization:
                        {
                            connectionString =  ConnectionString.CreateConnectionString(
                                model.Login,
                                model.Password,
                                Enum.Parse<TypeConnectDataBase>(_model.TypeConnectDataBaseSelectItems),
                                "master"
                                );
                            break;
                        }
                } 
                if(!await _controller.Create(_model.TypeDataBaseSelectItems, model.NameDataBase, model.LoginUser, model.PasswordUser, connectionString)) 
                {
                    _model.MessegeErrorCreateDataBase = "База даних не створена";
                    _model = null; 
                }
            }
            catch (Exception ex)
            {
                SetFieldPage();
                _model.MessegeErrorCreateDataBase = ex.Message;

                throw;
            }
            return RedirectToAction("Index", "Start");
        }
    }
}
