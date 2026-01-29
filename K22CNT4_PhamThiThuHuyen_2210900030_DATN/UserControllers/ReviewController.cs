using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext db;

        public ReviewController(AppDbContext context)
        {
            db = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review model)
        {
            // 🔐 1. KIỂM TRA ĐĂNG NHẬP
            var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");
            if (customerId == null)
            {
                TempData["ReviewError"] = "❌ Vui lòng đăng nhập để gửi đánh giá!";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // 🔴 2. KIỂM TRA DỮ LIỆU
            if (model.Productid <= 0 ||
                string.IsNullOrWhiteSpace(model.Content) ||
                model.Rating <= 0)
            {
                TempData["ReviewError"] = "❌ Vui lòng nhập đầy đủ nội dung và số sao!";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // 🔗 3. GÁN THÔNG TIN TỪ TÀI KHOẢN ĐÃ ĐĂNG NHẬP
            var customer = db.Customers.FirstOrDefault(x => x.Customerid == customerId);
            if (customer == null)
            {
                TempData["ReviewError"] = "❌ Tài khoản không hợp lệ!";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            model.Customerid = customer.Customerid;
            model.Fullname = customer.Username;   // hoặc Name nếu có
            model.Email = customer.Email;
            model.CreatedDate = DateTime.Now;
            model.IsApproved = 0; // chờ duyệt
            model.Isdelete = 0;
            model.Isactive = 1;

            db.Reviews.Add(model);
            db.SaveChanges();

            TempData["ReviewSuccess"] = "✅ Đánh giá đã được gửi và chờ admin duyệt.";

            var product = db.Products.FirstOrDefault(p => p.Id == model.Productid);
            return RedirectToAction("Detail", "Product", new { slug = product!.Slug });
        }


    }

}
