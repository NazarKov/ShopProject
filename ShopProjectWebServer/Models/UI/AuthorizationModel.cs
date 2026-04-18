using Microsoft.AspNetCore.Mvc; 

namespace ShopProjectWebServer.Models.UI
{
    public class AuthorizationModel
    {
        [BindProperty]
        public string Message  { get; set; } = string.Empty;

        [BindProperty]
        public string Login { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;
    }
}
