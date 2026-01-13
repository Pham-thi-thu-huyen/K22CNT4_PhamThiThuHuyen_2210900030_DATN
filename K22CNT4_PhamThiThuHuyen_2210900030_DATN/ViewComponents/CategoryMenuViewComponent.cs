using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly AppDbContext db;

        public CategoryMenuViewComponent(AppDbContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var model = db.Categories
                .Where(c => c.Isactive == 1 && c.Isdelete == 0)
                .OrderBy(c => c.Name)
                .Select(c => new CategoryMenuVM
                {
                    Id = c.Id,
                    CategoryName = c.Name,
                    Slug = c.Slug
                })
                .ToList();

            return View(model); 
        }
    }
}
