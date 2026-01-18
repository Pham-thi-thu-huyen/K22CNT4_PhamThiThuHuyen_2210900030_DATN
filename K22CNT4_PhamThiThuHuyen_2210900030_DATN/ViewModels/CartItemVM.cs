namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class CartItemVM
    {
        public long ProductId { get; set; }

        public long ProductVariantId { get; set; }
        public string MoTa { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Max { get; set; }
        public string? Image { get; set; }

        public decimal Total => Price * Quantity;
    }
}
