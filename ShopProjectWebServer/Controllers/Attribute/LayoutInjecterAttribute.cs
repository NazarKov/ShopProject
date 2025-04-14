using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ShopProjectWebServer.Controllers.Attribute
{
    public class LayoutInjecterAttribute : ActionFilterAttribute
    {
        private readonly string _layoutName;

        public LayoutInjecterAttribute(string layoutName)
        {
            _layoutName = layoutName;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Result is ViewResult result)
            {
                result.ViewData["Layout"] = _layoutName;
            }
        }
    }
}
