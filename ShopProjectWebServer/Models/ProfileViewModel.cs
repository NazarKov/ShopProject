using Microsoft.AspNetCore.Mvc;

namespace ShopProjectWebServer.Models
{
    public class ProfileViewModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public string Rule { get; set; }
        
        [BindProperty]
        public string OldPassword { get; set; }

        [BindProperty]
        public string Error { get; set; }

    }
}
