using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace e_shop.Controllers.Admin
{
    public class SecuredAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Controller controller = (Controller)context.Controller;

            if (controller.HttpContext.Session.GetString("login") == null)
            {
                string a = controller.ControllerContext.RouteData.Values["action"].ToString();
                string c = controller.ControllerContext.RouteData.Values["controller"].ToString();

                context.Result = new RedirectToActionResult("Index", "Login", new { next = $"{a}:{c}" });
            }
        }
    }
}
