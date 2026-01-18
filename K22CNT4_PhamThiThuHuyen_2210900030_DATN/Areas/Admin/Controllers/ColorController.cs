using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }

        // ===== INDEX =====
        public IActionResult Index()
        {
            var colors = _context.Colors.ToList();
            return View(colors);
        }

        // ===== CREATE =====
        [HttpPost]
        public IActionResult Create(string name, string? code)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction("Index");

            _context.Colors.Add(new Color
            {
                Name = name,
                Code = code,
                Isactive = 1
            });

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ===== TOGGLE ACTIVE =====
        [HttpPost]
        public IActionResult Toggle(long id)
        {
            var color = _context.Colors.Find(id);
            if (color != null)
            {
                color.Isactive = (byte)((color.Isactive == 1) ? 0 : 1);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
