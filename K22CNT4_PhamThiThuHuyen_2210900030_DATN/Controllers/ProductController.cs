using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext db;

        public ProductController(AppDbContext context)
        {
            db = context;
        }

        // ================= TẤT CẢ SẢN PHẨM =================
        public IActionResult Index(int? category)
        {
            var products = db.Products
                .Where(p => p.Isdelete == 0 || p.Isdelete == null)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .AsQueryable();

            if (category.HasValue)
            {
                products = products.Where(p => p.Categoryid == category.Value);
            }

            var result = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.ProductImages
                    .Where(pi => pi.Isdefault == 1)
                    .Select(pi => pi.Urlimg)
                    .FirstOrDefault(),
                Price = p.Price ?? 0,
                Description = p.Description,
                Slug = p.Slug,
                CategoryName = p.Category.Name
            }).ToList();

            ViewBag.Title = "Tất cả sản phẩm";
            return View(result);
        }

        // ================= BÉ GÁI =================
        public IActionResult Girls(int? category)
        {
            var products = db.Products
                .Where(p => p.Gender == 1 && (p.Isdelete == 0 || p.Isdelete == null))
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .AsQueryable();

            if (category.HasValue)
            {
                products = products.Where(p => p.Categoryid == category.Value);
            }

            var result = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.ProductImages
                    .Where(pi => pi.Isdefault == 1)
                    .Select(pi => pi.Urlimg)
                    .FirstOrDefault(),
                Price = p.Price ?? 0,
                Description = p.Description,
                Slug = p.Slug,
                CategoryName = p.Category.Name
            }).ToList();

            ViewBag.Title = "Thời trang bé gái";
            return View("Index", result);
        }

        // ================= BÉ TRAI =================
        public IActionResult Boys(int? category)
        {
            var products = db.Products
                .Where(p => p.Gender == 2 && (p.Isdelete == 0 || p.Isdelete == null))
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .AsQueryable();

            if (category.HasValue)
            {
                products = products.Where(p => p.Categoryid == category.Value);
            }

            var result = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.ProductImages
                    .Where(pi => pi.Isdefault == 1)
                    .Select(pi => pi.Urlimg)
                    .FirstOrDefault(),
                Price = p.Price ?? 0,
                Description = p.Description,
                Slug = p.Slug,
                CategoryName = p.Category.Name
            }).ToList();

            ViewBag.Title = "Thời trang bé trai";
            return View("Index", result);
        }

        // ================= CHI TIẾT SẢN PHẨM ================
        public IActionResult Detail(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return NotFound();

            var product = db.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefault(p =>
                    p.Slug == slug &&
                    (p.Isdelete == 0 || p.Isdelete == null));

            if (product == null)
                return NotFound();

            var model = new ProductDetailVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price ?? 0,
                Description = product.Description,
                Contents = product.Contents,
                CategoryName = product.Category?.Name,
                Images = product.ProductImages
                    .OrderByDescending(i => i.Isdefault)
                    .Select(i => i.Urlimg)
                    .ToList()
            };

            return View(model);
        }
    }
}
