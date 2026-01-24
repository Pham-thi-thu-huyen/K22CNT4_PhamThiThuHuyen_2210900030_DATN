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
            // 🔴 KIỂM TRA THỦ CÔNG
            if (model.Productid <= 0 ||
                string.IsNullOrWhiteSpace(model.Fullname) ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Content) ||
                model.Rating <= 0)
            {
                TempData["ReviewError"] = "❌ Vui lòng nhập đầy đủ thông tin đánh giá!";
                return Redirect(Request.Headers["Referer"].ToString());
            }

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
