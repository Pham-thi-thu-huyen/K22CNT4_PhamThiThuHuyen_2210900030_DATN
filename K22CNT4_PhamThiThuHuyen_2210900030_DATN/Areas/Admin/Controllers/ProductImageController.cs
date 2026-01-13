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

        // ================= DANH SÁCH ẢNH =================
        public IActionResult Index(long id)
        {
            var images = _context.ProductImages
                .Where(x => x.Productid == id)
                .OrderByDescending(x => x.Isdefault)
                .ToList();

            ViewBag.ProductId = id;
            return View(images);
        }

        // ================= THÊM ẢNH =================
        [HttpPost]
        public IActionResult Add(long productId, IFormFile image, bool isDefault)
        {
            if (image == null) return Json(new { success = false });

            if (isDefault)
            {
                var olds = _context.ProductImages.Where(x => x.Productid == productId);
                foreach (var i in olds) i.Isdefault = 0;
            }

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads/products"
            );

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(path, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            _context.ProductImages.Add(new ProductImage
            {
                Productid = productId,
                Urlimg = "/uploads/products/" + fileName,
                Isdefault = (byte)(isDefault ? 1 : 0)
            });

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // ================= XOÁ ẢNH =================
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var image = _context.ProductImages.Find(id);
            if (image == null) return Json(new { success = false });

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot" + image.Urlimg
            );

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _context.ProductImages.Remove(image);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // ================= ĐẶT MẶC ĐỊNH =================
        [HttpPost]
        public IActionResult SetDefault(long id)
        {
            var image = _context.ProductImages.Find(id);
            if (image == null) return Json(new { success = false });

            var olds = _context.ProductImages
                .Where(x => x.Productid == image.Productid);

            foreach (var i in olds) i.Isdefault = 0;

            image.Isdefault = 1;
            _context.SaveChanges();

            return Json(new { success = true });
        }

    }
}
