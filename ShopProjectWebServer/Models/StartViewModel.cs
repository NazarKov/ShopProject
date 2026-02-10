using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopProjectWebServer.Models
{
    public class StartViewModel
    {

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Messege { get; set; }

        [BindProperty]
        public string MessegeCreateDataBase { get; set; }

        [BindProperty]
        public string MessegeErrorCreateDataBase { get; set; }

        [BindProperty]
        public string NameDataBase { get; set; }

        [BindProperty]
        public string LoginUser { get; set; }
        [BindProperty]
        public string PasswordUser { get; set; }

        [BindProperty]
        public List<SelectListItem> TypeDataBase { get; set; }

        [BindProperty]
        public string TypeDataBaseSelectItems { get; set; }

        [BindProperty]
        public List<SelectListItem> TypeConnectDataBase { get; set; }

        [BindProperty]
        public string TypeConnectDataBaseSelectItems { get; set; }

        [BindProperty]
        public List<SelectListItem> TypeAuthorizationDataBase { get; set; }

        [BindProperty]
        public string TypeAuthorizationDataBaseSelectItems { get; set; }

        [BindProperty]
        public int VisibilitiCreateDataBaseform {  get; set; }
        [BindProperty]
        public int VisibilitiAutorizationDataBaseform { get; set; }
    }
}
