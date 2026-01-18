using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public IActionResult Index(
      int? categoryId,
      int? status,
      List<long> sizeIds,
      List<long> colorIds,
      List<long> materialIds
  )
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages) 
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Size)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Color)
                .Include(p => p.ProductVariants)
                    .ThenInclude(v => v.Material)
                .Where(p => p.Isdelete == 0 || p.Isdelete == null)
                .AsQueryable();

            // ===== FILTER CATEGORY =====
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.Categoryid == categoryId);
            }

            // ===== FILTER STATUS =====
            if (status.HasValue)
            {
                products = products.Where(p => p.Isactive == status);
            }

            // ===== FILTER SIZE =====
            if (sizeIds != null && sizeIds.Any())
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => sizeIds.Contains(v.Sizeid))
                );
            }

            // ===== FILTER COLOR =====
            if (colorIds != null && colorIds.Any())
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => colorIds.Contains(v.Colorid))
                );
            }

            // ===== FILTER MATERIAL =====
            if (materialIds != null && materialIds.Any())
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => materialIds.Contains(v.Materialid))
                );
            }

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Sizes = _context.Sizes.Where(x => x.Isactive == 1).ToList();
            ViewBag.Colors = _context.Colors.Where(x => x.Isactive == 1).ToList();
            ViewBag.Materials = _context.Materials.Where(x => x.Isactive == 1).ToList();

            return View(products.ToList());
        }

        // ================= CREATE (GET) =================
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories
                .Where(x => x.Isdelete == 0 || x.Isdelete == null)
                .ToList();

            return View();
        }

        // ================= CREATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model, List<IFormFile> images, int? defaultImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories
                    .Where(x => x.Isdelete == 0 || x.Isdelete == null)
                    .ToList();
                return View(model);
            }

            // ===== MAP CHECKBOX =====
            model.Isactive = Request.Form["Isactive"] == "1" ? (byte)1 : (byte)0;
            model.Issale = Request.Form["Issale"] == "1" ? (byte)1 : (byte)0;
            model.Istopsale = Request.Form["Istopsale"] == "1" ? (byte)1 : (byte)0;
            model.Ishome = Request.Form["Ishome"] == "1" ? (byte)1 : (byte)0;

            // ===== GENDER (1: Bé trai | 2: Bé gái | 3: Unisex – dự phòng) =====
            if (model.Gender != 1 && model.Gender != 2 && model.Gender != 3)
                model.Gender = 1;

            // ===== GIÁ TRỊ MẶC ĐỊNH =====
            model.CreatedDate = DateTime.Now;
            model.Isdelete = 0;

            if (string.IsNullOrEmpty(model.Slug))
            {
                model.Slug = model.Name?
                    .ToLower()
                    .Trim()
                    .Replace(" ", "-");
            }

            // ===== LƯU PRODUCT =====
            _context.Products.Add(model);
            _context.SaveChanges(); // ⚠️ PHẢI SAVE TRƯỚC

            // ===== LƯU ẢNH =====
            if (images != null && images.Count > 0)
            {
                var uploadPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/products"
                );

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                for (int i = 0; i < images.Count; i++)
                {
                    var file = images[i];
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _context.ProductImages.Add(new ProductImage
                    {
                        Productid = model.Id,
                        Urlimg = "/uploads/products/" + fileName,
                        Isdefault = (defaultImage.HasValue && defaultImage.Value == i)
                                    ? (byte)1
                                    : (byte)0
                    });
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = model.Id });
        }

        // ================= EDIT (GET) =================
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductVariants)
                .FirstOrDefault(p => p.Id == id && (p.Isdelete == 0 || p.Isdelete == null));

            if (product == null)
                return NotFound();

            ViewBag.Categories = _context.Categories
                .Where(c => c.Isdelete == 0 || c.Isdelete == null)
                .ToList();

            // 🔥 LẤY SIZE ĐANG ACTIVE
            ViewBag.Sizes = _context.Sizes
                .Where(s => s.Isactive == 1)
                .ToList();

            return View(product);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            var product = _context.Products
                .FirstOrDefault(x => x.Id == model.Id && (x.Isdelete == 0 || x.Isdelete == null));

            if (product == null) return NotFound();

            product.Name = model.Name;
            product.Slug = model.Slug;
            product.Categoryid = model.Categoryid;
            product.Description = model.Description;
            product.Contents = model.Contents;
            product.Price = model.Price;

            // 🔹 GENDER
            product.Gender = (model.Gender == 2 || model.Gender == 3) ? model.Gender : 1;

            // MAP CHECKBOX
            product.Isactive = Request.Form["Isactive"] == "1" ? (byte)1 : (byte)0;
            product.Issale = Request.Form["Issale"] == "1" ? (byte)1 : (byte)0;
            product.Istopsale = Request.Form["Istopsale"] == "1" ? (byte)1 : (byte)0;
            product.Ishome = Request.Form["Ishome"] == "1" ? (byte)1 : (byte)0;

            _context.SaveChanges();

            return RedirectToAction("Index");


   
        }

        // ================= DELETE (SOFT DELETE) =================
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return Json(new { success = false });

            product.Isdelete = 1;
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // ================= TOGGLE ACTIVE =================
        [HttpPost]
        public JsonResult IsActive(long id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return Json(new { success = false });

            product.Isactive = (byte)((product.Isactive == 1) ? 0 : 1);
            _context.SaveChanges();

            return Json(new { success = true, isActive = product.Isactive });
        }

        // ================= TOGGLE HOME =================
        [HttpPost]
        public JsonResult IsHome(long id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return Json(new { success = false });

            product.Ishome = (byte)((product.Ishome == 1) ? 0 : 1);
            _context.SaveChanges();

            return Json(new { success = true, isHome = product.Ishome });
        }

        // ================= TOGGLE SALE =================
        [HttpPost]
        public JsonResult IsSale(long id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return Json(new { success = false });

            product.Issale = (byte)((product.Issale == 1) ? 0 : 1);
            _context.SaveChanges();

            return Json(new { success = true, isSale = product.Issale });
        }
    }
}
