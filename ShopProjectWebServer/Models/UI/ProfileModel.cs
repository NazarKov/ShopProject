using Microsoft.AspNetCore.Mvc;

namespace ShopProjectWebServer.Models.UI
{
    public class ProfileModel
    {
        [BindProperty]
        public string Login { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Role { get; set; } = string.Empty;

        [BindProperty]
        public string Rule { get; set; } = string.Empty;

        [BindProperty]
        public string OldPassword { get; set; } = string.Empty;

        [BindProperty]
        public string Error { get; set; } = string.Empty;

    }
}
