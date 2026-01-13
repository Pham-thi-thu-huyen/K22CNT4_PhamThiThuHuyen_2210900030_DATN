using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext db;

        public ProductController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index(int? category)
        {
            var products = db.Products
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

            return View(result); 
        }
    }

    }

