using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var adminId = context.HttpContext.Session.GetInt32("AdminId");
            var adminRole = context.HttpContext.Session.GetString("AdminRole");

            // Chưa đăng nhập hoặc không phải Admin
            if (adminId == null || adminRole != "Admin")
            {
                context.Result = new RedirectToActionResult(
                    "Login", "Account", new { area = "Admin" });
            }

            base.OnActionExecuting(context);
        }
    }
}
