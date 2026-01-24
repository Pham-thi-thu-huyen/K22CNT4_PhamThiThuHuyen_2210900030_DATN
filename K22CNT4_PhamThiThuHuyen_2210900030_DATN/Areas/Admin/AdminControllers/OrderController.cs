using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.Controllers;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Areas.Admin.AdminControllers
{
    [Area("Admin")]
    public class OrderController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // ================== DANH SÁCH ĐƠN HÀNG ==================
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.TransportMethod)
                .Where(o => o.Isdelete == 0 || o.Isdelete == null)
                .OrderByDescending(o => o.OrdersDate)
                .ToList();

            return View(orders);
        }

        // ================== CHI TIẾT ĐƠN HÀNG ==================
        public IActionResult Detail(long id)
        {
            var order = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.TransportMethod)
                .Include(o => o.OrdersDetails)
                    .ThenInclude(od => od.Productvariant)
                        .ThenInclude(pv => pv.Product)
                .FirstOrDefault(o => o.Ordersid == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // ================== CẬP NHẬT TRẠNG THÁI ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(long id, byte status)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.Isactive = status;
                _context.SaveChanges();
            }

            return RedirectToAction("Detail", new { id });
        }

        // ================== XÓA MỀM ĐƠN HÀNG ==================
        public IActionResult Delete(long id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                order.Isdelete = 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
