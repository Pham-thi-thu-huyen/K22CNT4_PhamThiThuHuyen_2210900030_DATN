using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ================= REGISTER =================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Customers.Any(x => x.Username == model.Username))
            {
                ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Email) &&
                _context.Customers.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("", "Email đã được sử dụng");
                return View(model);
            }

            var customer = new Customer
            {
                Username = model.Username,
                Password = HashPassword(model.Password),
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                UserType = "CUSTOMER",
                CreatedDate = DateTime.Now,
                Isdelete = 0
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        // ================= LOGIN =================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            // 🔐 HASH PASSWORD NHẬP VÀO
            var hashedPassword = HashPassword(password);

            var customer = _context.Customers.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == hashedPassword &&
                x.Isdelete != 1);

            if (customer == null)
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }

            // ✅ SET SESSION
            HttpContext.Session.SetInt32("CUSTOMER_ID", (int)customer.Customerid);
            HttpContext.Session.SetString("USERNAME", customer.Username);
            HttpContext.Session.SetString("AVATAR",
                 string.IsNullOrEmpty(customer.Avatar)
                     ? "/wwwroot/uploads/avatars"
                     : customer.Avatar
             );



            return RedirectToAction("Index", "Home");
        }

        // ================= LOGOUT =================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // ================= HASH PASSWORD =================
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
