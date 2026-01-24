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
        var customerId = HttpContext.Session.GetString("CUSTOMER_ID");
        if (string.IsNullOrEmpty(customerId))
            return RedirectToAction("Login", "Account");

        var orders = _context.Orders
            .Include(o => o.TransportMethod)
            .Where(o => o.Customerid == long.Parse(customerId))
            .OrderByDescending(o => o.OrdersDate)
            .ToList();

        return View(orders);
    }

    // ===== CHI TIẾT ĐƠN =====
    public IActionResult Detail(long id)
    {
        var customerId = HttpContext.Session.GetString("CUSTOMER_ID");
        if (string.IsNullOrEmpty(customerId))
            return RedirectToAction("Login", "Account");

        var order = _context.Orders
            .Include(o => o.TransportMethod)
            .Include(o => o.OrdersDetails)
                .ThenInclude(d => d.Productvariant)
                    .ThenInclude(pv => pv.Product)
            .FirstOrDefault(o =>
                o.Ordersid == id &&
                o.Customerid == long.Parse(customerId));

        if (order == null) return NotFound();

        return View(order);
    }
}
