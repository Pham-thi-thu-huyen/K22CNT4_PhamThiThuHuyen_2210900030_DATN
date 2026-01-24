using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : BaseAdminController
    {
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            return View();
        }
    }
}

