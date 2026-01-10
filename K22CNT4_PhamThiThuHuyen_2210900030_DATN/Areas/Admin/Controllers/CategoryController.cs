using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // ====== LIST ======
        public IActionResult Index()
        {
            var categories = _context.Categories
                .Where(x => x.Isdelete == 0)
                .OrderByDescending(x => x.Id)
                .ToList();

            return View(categories);
        }


        // ====== CREATE ======
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ParentCategories = _context.Categories
                .Where(x => x.Isdelete == 0)
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ParentCategories = _context.Categories
                    .Where(x => x.Isdelete == 0)
                    .ToList();

                return View(model);
            }

            if (string.IsNullOrEmpty(model.Slug))
            {
                model.Slug = model.Name.ToLower().Replace(" ", "-");
            }

            model.CreatedDate = DateTime.Now;
            model.Isdelete = 0;

            _context.Categories.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // ====== EDIT GET ======
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var category = _context.Categories
                .FirstOrDefault(x => x.Id == id && x.Isdelete == 0);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // ====== EDIT POST ======
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var category = _context.Categories.Find(model.Id);
            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;
            category.MetaTitle = model.MetaTitle;
            category.MetaDesc = model.MetaDesc;
            category.MetaKeyword = model.MetaKeyword;
            category.Isactive = model.Isactive; // ✅ CẬP NHẬT TRẠNG THÁI

            if (string.IsNullOrEmpty(category.Slug))
            {
                category.Slug = category.Name
                    .ToLower()
                    .Replace(" ", "-");
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // ====== DELETE (XÓA MỀM) ======
        public IActionResult Delete(long id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            category.Isdelete = 1;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}