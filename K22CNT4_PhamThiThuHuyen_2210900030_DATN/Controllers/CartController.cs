using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private const string CART_KEY = "CART";

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // ================== LẤY GIỎ ==================
        private List<CartItemVM> GetCart()
        {
            var json = HttpContext.Session.GetString(CART_KEY);
            return json == null
                ? new List<CartItemVM>()
                : JsonConvert.DeserializeObject<List<CartItemVM>>(json)!;
        }

        // ================== LƯU GIỎ + UPDATE ICON ==================
        private void SaveCart(List<CartItemVM> cart)
        {
            HttpContext.Session.SetString(CART_KEY, JsonConvert.SerializeObject(cart));
            UpdateCartCount(cart);
        }

        private void UpdateCartCount(List<CartItemVM> cart)
        {
            int totalQuantity = cart.Sum(x => x.Quantity);
            HttpContext.Session.SetInt32("CART_COUNT", totalQuantity);
        }

        // ================== XEM GIỎ ==================
        public IActionResult Index()
        {
            return View(GetCart());
        }

        // ================== THÊM VÀO GIỎ ==================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ProductVariantId</param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public IActionResult Add(long id, int quantity = 1)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductVariantId == id);
            // Load ProductVariant với tất cả thông tin cần thiết
            var productVariant = _context.ProductVariants
                .Include(pv => pv.Product)
                    .ThenInclude(p => p.ProductImages)
                .Include(pv => pv.Size)
                .Include(pv => pv.Color)
                .Include(pv => pv.Material)
                .FirstOrDefault(pv => pv.ProductVariantid == id);

            // Kiểm tra ProductVariant có tồn tại không
            if (productVariant == null)
            {
                return NotFound("Không tìm thấy sản phẩm");
            }

            // Nếu sản phẩm chưa có trong giỏ
            if (item == null)
            {
                var newItem = new CartItemVM
                {
                    ProductId = productVariant.Productid,
                    ProductVariantId = id,
                    MoTa = $"(Kích cỡ: {productVariant.Size?.Name} - Màu: {productVariant.Color?.Name} - Chất liệu: {productVariant.Material?.Name})",
                    Name = productVariant.Product?.Name,
                    Price = (productVariant.Product?.Price ?? 0) + (productVariant.Price ?? 0),
                    Max = productVariant.Quantity ?? 0,
                    Image = productVariant.Product?.ProductImages?
                        .FirstOrDefault(i => i.Isdefault == 1)?.Urlimg,
                    Quantity = quantity
                };

                cart.Add(newItem);
            }
            else
            {
                // Nếu đã có trong giỏ, tăng số lượng
                item.Quantity += quantity;
            }

            SaveCart(cart);
            return RedirectToAction("Index");

        }

         //================== XÓA 1 SẢN PHẨM ==================
        public IActionResult Remove(long id)
        {
            var cart = GetCart();
            cart.RemoveAll(x => x.ProductVariantId == id);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // ================== CẬP NHẬT SỐ LƯỢNG ==================
        [HttpPost]
        public IActionResult Update(long id, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.ProductVariantId == id);

            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // ================== XÓA TẤT CẢ ==================
        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CART_KEY);
            HttpContext.Session.SetInt32("CART_COUNT", 0);
            return RedirectToAction("Index");
        }
        // ================== GET: CHECKOUT ==================
        public IActionResult Checkout()
        {
           var user = HttpContext.Session.Get("CUSTOMER_ID");
            if(user.IsNullOrEmpty())
            {
                return RedirectToAction("Login", "Account"); ;
            }
            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            var model = new CheckoutVM
            {
                TotalMoney = cart.Sum(x => x.Total)
            };

            return View(model);
        }

        // ================== POST: CHECKOUT ==================
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            var order = new Order
            {
                OrdersDate = DateTime.Now,
                Customerid = long.Parse(HttpContext.Session.Get("CUSTOMER_ID")), // ✅ KHÁCH VÃNG LAI
                TotalMoney = cart.Sum(x => x.Total),
                NameReceiver = model.NameReceiver,
                Phone = model.Phone,
                Address = model.Address,
                Isdelete = 0,
                Isactive = 1
            };


            _context.Orders.Add(order);
            _context.SaveChanges();
            order.Ordersid = _context.Orders.Max(s => s.Ordersid);
            // ===== LƯU ORDER DETAILS =====
            foreach (var item in cart)
            {
                _context.OrdersDetails.Add(new OrdersDetail
                {
                    Ordersid = order.Ordersid,
                    Productvariantid = item.ProductVariantId, 
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Price * item.Quantity
                });
            }

            _context.SaveChanges();

            // reset cart
            HttpContext.Session.Remove(CART_KEY);
            HttpContext.Session.SetInt32("CART_COUNT", 0);

            return RedirectToAction("Success");
        }

        // ================== SUCCESS ==================
        public IActionResult Success()
        {
            return View();
        }
    }
}
