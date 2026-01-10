using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly AppDbContext _context;

        public ProductImageController(AppDbContext context)
        {
            _context = context;
        }

        // ================== DANH SÁCH ẢNH ==================
        public IActionResult Index(long id)
        {
            if (id <= 0)
                return NotFound();

            ViewBag.ProductId = id;

            var images = _context.ProductImages
                                 .Where(x => x.Productid == id)
                                 .ToList();

            return View(images);
        }

        // ================== THÊM ẢNH ==================
        [HttpPost]
        public IActionResult Add(long productId, string imageUrl, bool isDefault = false)
        {
            if (productId <= 0 || string.IsNullOrEmpty(imageUrl))
                return Json(new { success = false });

            // Nếu là ảnh mặc định → bỏ mặc định ảnh cũ
            if (isDefault)
            {
                var oldDefaults = _context.ProductImages
                                          .Where(x => x.Productid == productId);

                foreach (var img in oldDefaults)
                {
                    img.Isdefault = 0;
                }
            }

            var image = new ProductImage
            {
                Productid = productId,
                Urlimg = imageUrl,
                Isdefault = (byte)(isDefault ? 1 : 0)
            };

            _context.ProductImages.Add(image);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // ================== XOÁ ẢNH ==================
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var image = _context.ProductImages.Find(id);
            if (image == null)
                return Json(new { success = false });

            _context.ProductImages.Remove(image);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // ================== ĐẶT ẢNH MẶC ĐỊNH ==================
        [HttpPost]
        public IActionResult SetDefault(long id)
        {
            var image = _context.ProductImages.Find(id);
            if (image == null)
                return Json(new { success = false });

            var images = _context.ProductImages
                                 .Where(x => x.Productid == image.Productid);

            foreach (var img in images)
            {
                img.Isdefault = 0;
            }

            image.Isdefault = 1;
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
