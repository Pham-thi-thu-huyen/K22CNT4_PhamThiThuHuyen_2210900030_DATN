using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        // ================= PROFILE =================
        public IActionResult Index()
        {
            var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");

            if (customerId == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem trang cá nhân";
                return RedirectToAction("Login", "Account");
            }

            var customer = _context.Customers
                .FirstOrDefault(x => x.Customerid == customerId.Value);

            if (customer == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin người dùng";
                return RedirectToAction("Login", "Account");
            }

            return View(customer);
        }

        // ================= EDIT (GET) =================
        [HttpGet]
        public IActionResult Edit()
        {
            var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");
            if (customerId == null)
                return RedirectToAction("Login", "Account");

            var customer = _context.Customers
                .FirstOrDefault(x => x.Customerid == customerId.Value);

            return View(customer);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer model, IFormFile avatarFile)
        {
            var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");
            if (customerId == null)
                return RedirectToAction("Login", "Account");

            var customer = _context.Customers
                .FirstOrDefault(x => x.Customerid == customerId.Value);

            if (customer == null)
                return RedirectToAction("Login", "Account");

            // cập nhật thông tin
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            customer.Address = model.Address;

            // 📸 UPLOAD AVATAR
            if (avatarFile != null && avatarFile.Length > 0)
            {
                var ext = Path.GetExtension(avatarFile.FileName);
                var fileName = $"avt_{customerId}{ext}";
                var folder = Path.Combine(Directory.GetCurrentDirectory(),
                                          "wwwroot/uploads/avatars");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    avatarFile.CopyTo(stream);
                }

                customer.Avatar = "/uploads/avatars/" + fileName;

                // 🔥 CẬP NHẬT SESSION AVATAR
                HttpContext.Session.SetString("AVATAR", customer.Avatar);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
