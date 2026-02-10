using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopProjectWebServer.Models
{
    public class AuthorizationViewModel
    {
        [BindProperty]
        public string Messege  { get; set; }

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }
    }
}
