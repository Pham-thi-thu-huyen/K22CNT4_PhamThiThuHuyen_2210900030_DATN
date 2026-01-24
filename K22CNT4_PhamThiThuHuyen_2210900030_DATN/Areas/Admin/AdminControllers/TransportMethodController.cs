using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransportMethodController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public TransportMethodController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.TransportMethods
                .Where(x => x.Isdelete == 0 || x.Isdelete == null)
                .ToList();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(TransportMethod model)
        {
            model.CreatedDate = DateTime.Now;
            model.Isactive = 1;
            model.Isdelete = 0;
            _context.TransportMethods.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
