using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // CUSTOMER
            var customers = _context.Customers
                .Where(x => x.Isdelete == 0 || x.Isdelete == null)
                .Select(x => new UserManageVM
                {
                    Id = x.Customerid,
                    Username = x.Username,
                    Email = x.Email,
                    Phone = x.Phone,
                    Address = x.Address,
                    Role = "CUSTOMER",
                    Status = true,
                    CreatedDate = x.CreatedDate
                }).ToList();

            // ADMIN
            var admins = _context.Admins
                .Select(x => new UserManageVM
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Email = null,
                    Phone = null,
                    Address = null,
                    Role = "ADMIN",
                    Status = x.Status ?? false,
                    CreatedDate = x.CreatedDate
                }).ToList();

            var model = customers.Concat(admins).OrderByDescending(x => x.CreatedDate).ToList();

            return View(model);
        }

        // Xóa USER (CUSTOMER)
        public IActionResult DeleteCustomer(long id)
        {
            var user = _context.Customers.Find(id);
            if (user != null)
            {
                user.Isdelete = 1;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Khóa / mở ADMIN
        public IActionResult ToggleAdmin(int id)
        {
            var admin = _context.Admins.Find(id);
            if (admin != null)
            {
                admin.Status = !(admin.Status ?? true);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

}
