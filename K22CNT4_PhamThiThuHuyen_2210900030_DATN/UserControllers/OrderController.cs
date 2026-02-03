using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

public class OrderController : Controller
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    // ===== ĐƠN HÀNG CỦA TÔI =====
    public IActionResult MyOrders()
    {
        // 1️⃣ Lấy CUSTOMER_ID từ Session
        var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");

        if (!customerId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        // 2️⃣ Lấy ĐƠN HÀNG TRỰC TIẾP từ bảng Orders
        var orders = _context.Orders
            .AsNoTracking() // ✅ tránh cache, đảm bảo trạng thái luôn mới
            .Include(o => o.TransportMethod)
            .Where(o => o.Customerid == customerId.Value)
            .OrderByDescending(o => o.OrdersDate)
            .ToList();

        return View(orders);
    }



    // ===== CHI TIẾT ĐƠN =====
    public IActionResult Detail(long id)
    {
        // 1️⃣ LẤY CUSTOMER_ID ĐÚNG KIỂU (GIỐNG MyOrders)
        var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");
        if (!customerId.HasValue)
            return RedirectToAction("Login", "Account");

        // 2️⃣ QUERY AN TOÀN – KHÔNG PARSE STRING
        var order = _context.Orders
            .AsNoTracking() // ✅ tránh cache
            .Include(o => o.TransportMethod)
            .Include(o => o.OrdersDetails)
                .ThenInclude(d => d.Productvariant)
                    .ThenInclude(pv => pv.Product)
            .FirstOrDefault(o =>
                o.Ordersid == id &&
                o.Customerid == customerId.Value);

        if (order == null)
            return NotFound();

        return View(order);
    }

}
