using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopProjectWebServer.Models.UI
{
    public class StartModel
    {

        [BindProperty]
        public string Login { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public string Messege { get; set; } = string.Empty;

        [BindProperty]
        public string MessegeCreateDataBase { get; set; } = string.Empty;

        [BindProperty]
        public string MessegeErrorCreateDataBase { get; set; } = string.Empty;

        [BindProperty]
        public string NameDataBase { get; set; } = string.Empty;

        [BindProperty]
        public string LoginUser { get; set; } = string.Empty;
        [BindProperty]
        public string PasswordUser { get; set; } = string.Empty;

        [BindProperty]
        public List<SelectListItem> TypeDataBase { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string TypeDataBaseSelectItems { get; set; } = string.Empty;

        [BindProperty]
        public List<SelectListItem> TypeConnectDataBase { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string TypeConnectDataBaseSelectItems { get; set; } = string.Empty;

        [BindProperty]
        public List<SelectListItem> TypeAuthorizationDataBase { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string TypeAuthorizationDataBaseSelectItems { get; set; } = string.Empty;

        [BindProperty]
        public int VisibilitiCreateDataBaseform {  get; set; }
        [BindProperty]
        public int VisibilitiAutorizationDataBaseform { get; set; }
    }
}
