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
            HttpContext.Session.SetInt32("CART_COUNT", cart.Count);
        }


        // ================== XEM GIỎ ==================
        public IActionResult Index()
        {
            return View(GetCart());
        }

        // ================== THÊM SẢN PHẨM VÀO GIỎ ==================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ProductVariantId</param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// 
        // =============== MUA NGAY ===========================
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
        [HttpPost]
        public IActionResult AddAjax(long id, int quantity = 1)
        {
            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductVariantId == id);

            var productVariant = _context.ProductVariants
                .Include(pv => pv.Product)
                    .ThenInclude(p => p.ProductImages)
                .Include(pv => pv.Size)
                .Include(pv => pv.Color)
                .Include(pv => pv.Material)
                .FirstOrDefault(pv => pv.ProductVariantid == id);

            if (productVariant == null)
                return Json(new { success = false, message = "Không tìm thấy sản phẩm" });

            if (item == null)
            {
                cart.Add(new CartItemVM
                {
                    ProductId = productVariant.Productid,
                    ProductVariantId = id,
                    Name = productVariant.Product?.Name,
                    Quantity = quantity,
                    Price = (productVariant.Product?.Price ?? 0) + (productVariant.Price ?? 0),
                    Image = productVariant.Product?.ProductImages?
                        .FirstOrDefault(i => i.Isdefault == 1)?.Urlimg
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            SaveCart(cart);

            // 🔥 QUAN TRỌNG: TRẢ cartCount
            return Json(new
            {
                success = true,
                cartCount = cart.Count
            });
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
        // ================== GET: CHECKOUT =================
        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetString("CUSTOMER_ID");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            // 🔥 VẬN CHUYỂN
            ViewBag.TransportMethods = _context.TransportMethods
                .Where(x => x.Isactive == 1 && (x.Isdelete == 0 || x.Isdelete == null))
                .ToList();

            // 🔥 PHƯƠNG THỨC THANH TOÁN
            ViewBag.PayMethods = _context.PayMethods
                .Where(x => x.Isactive == 1 && (x.Isdelete == 0 || x.Isdelete == null))
                .ToList();

            var model = new CheckoutVM
            {
                TotalMoney = cart.Sum(x => x.Total)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            var cart = GetCart();
            if (!cart.Any())
                return RedirectToAction("Index");

            // ================== CHECK LOGIN ==================
            var customerId = HttpContext.Session.GetInt32("CUSTOMER_ID");
            if (customerId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            // ================== LOAD VIEWBAG LẠI ==================
            ViewBag.TransportMethods = _context.TransportMethods
                .Where(x => x.Isactive == 1 && (x.Isdelete == 0 || x.Isdelete == null))
                .ToList();

            ViewBag.PayMethods = _context.PayMethods
                .Where(x => x.Isactive == 1 && (x.Isdelete == 0 || x.Isdelete == null))
                .ToList();

            // ================== VALIDATE ==================
            if (!ModelState.IsValid)
            {
                model.TotalMoney = cart.Sum(x => x.Total);
                return View(model);
            }

            // ================== TÍNH TIỀN ==================
            var productTotal = cart.Sum(x => x.Total);

            var transport = _context.TransportMethods
                .FirstOrDefault(x => x.TransportMethodid == model.TransportMethodId);

            int shipFee = transport?.Price ?? 0;

            decimal totalMoney = productTotal + shipFee;

            // ================== TẠO ORDER ==================
            var order = new Order
            {
                OrdersDate = DateTime.Now,
                Customerid = customerId,

                NameReceiver = model.NameReceiver,
                Phone = model.Phone,
                Address = model.Address,

                TransportMethodid = model.TransportMethodId,
                PayMethodId = model.PayMethodId,

                TotalMoney = totalMoney,

                Status = 0, // 🔥 CHỜ THANH TOÁN
                Isdelete = 0,
                Isactive = 1
            };


            _context.Orders.Add(order);
            _context.SaveChanges();

            // ================== ORDER DETAILS ==================
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

            // ================== RESET CART ==================
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

