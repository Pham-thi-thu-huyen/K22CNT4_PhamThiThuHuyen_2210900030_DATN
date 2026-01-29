using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;
using System.ComponentModel.DataAnnotations;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class CheckoutVM
    {
        [Required]
        public string NameReceiver { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        // 🔥 ID phương thức thanh toán được chọn
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public long PayMethodId { get; set; }

        // 🔥 ID vận chuyển
        [Required(ErrorMessage = "Vui lòng chọn phương thức vận chuyển")]
        public long TransportMethodId { get; set; }

        public decimal TotalMoney { get; set; }
    }
}
