using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ================== LOGIN GET ==================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ================== LOGIN POST =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            var admin = _context.Admins
                .FirstOrDefault(x => x.UserName == username && x.Status == true);

            if (admin == null)
            {
                ViewBag.Error = "Tài khoản không tồn tại hoặc đã bị khóa";
                return View();
            }

            if (!BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                ViewBag.Error = "Sai mật khẩu";
                return View();
            }

            // ===== SESSION ADMIN =====
            HttpContext.Session.SetInt32("AdminId", admin.Id);
            HttpContext.Session.SetString("AdminUserName", admin.UserName);
            HttpContext.Session.SetString("AdminFullName", admin.FullName ?? "");
            HttpContext.Session.SetString("AdminRole", admin.Role ?? "Admin");

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        // ================== LOGOUT ==================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
