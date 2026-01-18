using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        // ===== INDEX =====
        public IActionResult Index()
        {
            var sizes = _context.Sizes.ToList();
            return View(sizes);
        }

        // ===== CREATE =====
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Size model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Isactive = 1;
            _context.Sizes.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ===== EDIT =====
        [HttpGet]
        public IActionResult Edit(long id)
        {
            var size = _context.Sizes.Find(id);
            if (size == null) return NotFound();
            return View(size);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Size model)
        {
            var size = _context.Sizes.Find(model.Sizeid);
            if (size == null) return NotFound();

            size.Name = model.Name;
            size.AgeFrom = model.AgeFrom;
            size.AgeTo = model.AgeTo;
            size.Isactive = model.Isactive;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ===== TOGGLE ACTIVE =====
        [HttpPost]
        public IActionResult Toggle(long id)
        {
            var size = _context.Sizes.Find(id);
            if (size == null) return NotFound();

            size.Isactive = (byte)((size.Isactive == 1) ? 0 : 1);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
