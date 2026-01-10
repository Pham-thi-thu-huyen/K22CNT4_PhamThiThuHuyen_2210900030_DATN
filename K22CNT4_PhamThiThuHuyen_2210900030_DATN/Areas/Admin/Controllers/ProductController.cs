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
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductImages)
                .Where(x => x.Isdelete == 0 || x.Isdelete == null)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(products);
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
            _context.Products.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = model.Id });


            // ===== MAP CHECKBOX (byte?) =====
            model.Isactive = Request.Form["Isactive"] == "1" ? (byte)1 : (byte)0;
            model.Issale = Request.Form["Issale"] == "1" ? (byte)1 : (byte)0;
            model.Istopsale = Request.Form["Istopsale"] == "1" ? (byte)1 : (byte)0;
            model.Ishome = Request.Form["Ishome"] == "1" ? (byte)1 : (byte)0;

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

            _context.Products.Add(model);
            _context.SaveChanges();

            // ===== LƯU ẢNH =====
            if (images != null && images.Count > 0)
            {
                var uploadPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/products"
                );

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

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

            return RedirectToAction("Index");
        }


        // ================= EDIT (GET) =================
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var product = _context.Products
                .Include(x => x.ProductImages)
                .FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            return View(product);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(model);
            }

            var product = _context.Products.Find(model.Id);
            if (product == null)
                return NotFound();

            // ===== UPDATE CÁC FIELD CHO PHÉP =====
            product.Name = model.Name;
            product.Price = model.Price;
            product.Categoryid = model.Categoryid;
            product.Description = model.Description;
            product.Contents = model.Contents;
            product.MetaTitle = model.MetaTitle;
            product.MetaKeyword = model.MetaKeyword;
            product.MetaDesc = model.MetaDesc;
            product.Slug = model.Slug;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ================= DELETE (XÓA MỀM) =================
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
