using Microsoft.AspNetCore.Mvc;

namespace ShopProjectWebServer.Models.ViewComponents
{
    public class MenuComponentViewModel
    {
        [BindProperty]
        public string Login { get; set; }

    }
}
