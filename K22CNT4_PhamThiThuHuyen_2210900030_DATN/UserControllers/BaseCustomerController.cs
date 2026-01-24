using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class BaseCustomerController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetString("CUSTOMER_ID");
            var userType = context.HttpContext.Session.GetString("USER_TYPE");

            if (userId == null || userType != "CUSTOMER")
            {
                context.Result = new RedirectToActionResult(
                    "Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
