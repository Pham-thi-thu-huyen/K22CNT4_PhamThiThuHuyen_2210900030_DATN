using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int featuredPage = 1, int salePage = 1)
    {
        int pageSize = 8;

        // ===== NỔI BẬT =====
        var featuredQuery = _context.Products
            .Where(p => p.Ishome == 1 && (p.Isdelete == 0 || p.Isdelete == null));

        int featuredTotal = featuredQuery.Count();
        int featuredTotalPages = (int)Math.Ceiling(featuredTotal / (double)pageSize);

        var featuredProducts = featuredQuery
            .Include(p => p.ProductImages)
            .OrderByDescending(p => p.Id)
            .Skip((featuredPage - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price ?? 0,
                Slug = p.Slug,
                Image = p.ProductImages
                    .Where(i => i.Isdefault == 1)
                    .Select(i => i.Urlimg)
                    .FirstOrDefault()
            })
            .ToList();

        // ===== SALE =====
        var saleQuery = _context.Products
            .Where(p => p.Issale == 1 && (p.Isdelete == 0 || p.Isdelete == null));

        int saleTotal = saleQuery.Count();
        int saleTotalPages = (int)Math.Ceiling(saleTotal / (double)pageSize);

        var saleProducts = saleQuery
            .Include(p => p.ProductImages)
            .OrderByDescending(p => p.Id)
            .Skip((salePage - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price ?? 0,
                Slug = p.Slug,
                Image = p.ProductImages
                    .Where(i => i.Isdefault == 1)
                    .Select(i => i.Urlimg)
                    .FirstOrDefault()
            })
            .ToList();

        var model = new HomeVM
        {
            FeaturedProducts = featuredProducts,
            FeaturedPage = featuredPage,
            FeaturedTotalPages = featuredTotalPages,

            SaleProducts = saleProducts,
            SalePage = salePage,
            SaleTotalPages = saleTotalPages
        };

        return View(model);
    }

    public IActionResult LoadFeatured(int page = 1)
    {
        int pageSize = 8;

        var query = _context.Products
            .Where(p => p.Ishome == 1 && (p.Isdelete == 0 || p.Isdelete == null));

        int total = query.Count();
        int totalPages = (int)Math.Ceiling(total / (double)pageSize);

        var products = query
            .Include(p => p.ProductImages)
            .OrderByDescending(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price ?? 0,
                Slug = p.Slug,
                Image = p.ProductImages
                    .Where(i => i.Isdefault == 1)
                    .Select(i => i.Urlimg)
                    .FirstOrDefault()
            })
            .ToList();

        var vm = new HomeVM
        {
            FeaturedProducts = products,
            FeaturedPage = page,
            FeaturedTotalPages = totalPages
        };

        return PartialView("_FeaturedProducts", vm);
    }


}
