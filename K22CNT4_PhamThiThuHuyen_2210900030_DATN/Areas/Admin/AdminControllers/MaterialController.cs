using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public MaterialController(AppDbContext context)
        {
            _context = context;
        }

        // ===== INDEX =====
        public IActionResult Index()
        {
            var materials = _context.Materials.ToList();
            return View(materials);
        }

        // ===== CREATE =====
        [HttpPost]
        public IActionResult Create(string name, string? notes)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction("Index");

            _context.Materials.Add(new Material
            {
                Name = name,
                Notes = notes,
                Isactive = 1
            });

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ===== TOGGLE ACTIVE =====
        [HttpPost]
        public IActionResult Toggle(long id)
        {
            var material = _context.Materials.Find(id);
            if (material != null)
            {
                material.Isactive = (byte)((material.Isactive == 1) ? 0 : 1);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
