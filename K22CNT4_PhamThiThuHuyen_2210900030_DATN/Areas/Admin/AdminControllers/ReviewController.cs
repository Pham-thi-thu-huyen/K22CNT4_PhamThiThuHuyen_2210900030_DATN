using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : BaseAdminController
    {
        private readonly AppDbContext db;

        public ReviewController(AppDbContext context)
        {
            db = context;
        }

        // ================= DANH SÁCH REVIEW =================
        public IActionResult Index()
        {
            var reviews = db.Reviews
                .Include(r => r.Product)
                .Where(r => r.Isdelete == 0 || r.Isdelete == null)
                .OrderByDescending(r => r.CreatedDate)
                .ToList();

            return View(reviews);
        }

        // ================= DUYỆT =================
        public IActionResult Approve(long id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                review.IsApproved = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ================= HỦY DUYỆT =================
        public IActionResult UnApprove(long id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                review.IsApproved = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ================= TRẢ LỜI =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reply(long id, string reply)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                review.Reply = reply;
                review.ReplyDate = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // ================= XÓA =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            var review = db.Reviews.Find(id);
            if (review != null)
            {
                review.Isdelete = 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
