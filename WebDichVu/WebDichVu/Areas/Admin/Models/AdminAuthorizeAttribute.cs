using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DichVuThuYService.Areas.Admin.Models
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isLoggedIn = context.HttpContext.Session.GetString("AdminLoggedIn");
            if (isLoggedIn != "true")
            {
                // Redirect to Admin Login page if not logged in
                context.Result = new RedirectToActionResult("Login", "AdminLogin", new { area = "Admin" });
            }
            base.OnActionExecuting(context);
        }
    }
}
