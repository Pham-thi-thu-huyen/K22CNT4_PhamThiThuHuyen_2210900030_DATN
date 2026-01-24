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

        public decimal TotalMoney { get; set; }
        public long TransportMethodId { get; set; }

    }
}
