using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductVariantController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public ProductVariantController(AppDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index(long productId)
        {
            var product = _context.Products
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Color)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Material)
                .FirstOrDefault(p =>
                    p.Id == productId &&
                    (p.Isdelete == 0 || p.Isdelete == null));

            if (product == null)
                return NotFound();

            ViewBag.Product = product;

            ViewBag.Sizes = _context.Sizes
                .Where(x => x.Isactive == 1)
                .ToList();

            ViewBag.Colors = _context.Colors
                .Where(x => x.Isactive == 1)
                .ToList();
            ViewBag.Materials = _context.Materials
                .Where(x => x.Isactive == 1)
                .ToList();

            return View(product.ProductVariants.ToList());
        }


        // ================= CREATE =================
        [HttpPost]
        public IActionResult Create(
    long ProductId,
    long SizeId,
    long ColorId,
    long MaterialId,
    decimal Price,
    int Quantity
)
        {
            // 🔒 Kiểm tra trùng biến thể
            bool exists = _context.ProductVariants.Any(v =>
                v.Productid == ProductId &&
                v.Sizeid == SizeId &&
                v.Colorid == ColorId &&
                v.Materialid == MaterialId
            );

            if (exists)
            {
                TempData["Error"] = "Biến thể này đã tồn tại!";
                return RedirectToAction("Index", new { productId = ProductId });
            }

            var variant = new ProductVariant
            {
                Productid = ProductId,
                Sizeid = SizeId,
                Colorid = ColorId,
                Materialid = MaterialId,
                Price = Price,
                Quantity = Quantity,
                Isactive = 1
            };

            _context.ProductVariants.Add(variant);
            _context.SaveChanges();

            TempData["Success"] = "Thêm biến thể thành công!";
            return RedirectToAction("Index", new { productId = ProductId });
        }


        // ================= DELETE =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id, long productId)
        {
            var variant = _context.ProductVariants
                .FirstOrDefault(x => x.ProductVariantid == id);

            if (variant == null)
                return NotFound();

            _context.ProductVariants.Remove(variant);
            _context.SaveChanges();

            TempData["Success"] = "Đã xóa biến thể!";
            return RedirectToAction("Index", new { productId });
        }

    }
}
