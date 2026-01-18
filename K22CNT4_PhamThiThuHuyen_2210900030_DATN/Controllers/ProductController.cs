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
            var product = db.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Color)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Material)
                .FirstOrDefault(p => p.Slug == slug && (p.Isdelete == 0 || p.Isdelete == null));

            if (product == null)
                return NotFound();

            var vm = new ProductDetailVM
            {
                Id = product.Id,
                Name = product.Name,
                Slug = product.Slug,
                Price = product.Price ?? 0,
                Contents = product.Contents,
                Images = product.ProductImages
                    .OrderByDescending(i => i.Isdefault)
                    .Select(i => i.Urlimg)
                    .ToList()
            };

            ViewBag.Sizes = product.ProductVariants
                .Where(v => v.Isactive == 1 && v.Size.Isactive == 1)
                .Select(v => v.Size)
                .Distinct()
                .ToList();

            ViewBag.Colors = product.ProductVariants
                .Where(v => v.Isactive == 1 && v.Color.Isactive == 1)
                .Select(v => v.Color)
                .Distinct()
                .ToList();

            ViewBag.Materials = product.ProductVariants
                .Where(v => v.Isactive == 1 && v.Material.Isactive == 1)
                .Select(v => v.Material)
                .Distinct()
                .ToList();

            return View(vm); // ✅ DUY NHẤT
        }




        [HttpPost]
        public JsonResult GetProductVariant(long sizeId, long colorId, long materialId, long productId)
        {
            try
            {
                // Tìm ProductVariant dựa vào 3 thuộc tính
                var productVariant = db.ProductVariants
                    .Include(pv => pv.Product)
                    .FirstOrDefault(pv =>
                        pv.Productid == productId &&
                        pv.Sizeid == sizeId &&
                        pv.Colorid == colorId &&
                        pv.Materialid == materialId);

                if (productVariant != null)
                {
                    return Json(new
                    {
                        success = true,
                        productId = productVariant.Productid,
                        productName = productVariant.Product?.Name,
                        variantId = productVariant.ProductVariantid,
                        price = productVariant.Price + productVariant.Product.Price,
                        quantity = productVariant.Quantity
                       
                    });
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không tìm thấy sản phẩm với thuộc tính này"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Lỗi: " + ex.Message
                });
            }
        }
    }
}
