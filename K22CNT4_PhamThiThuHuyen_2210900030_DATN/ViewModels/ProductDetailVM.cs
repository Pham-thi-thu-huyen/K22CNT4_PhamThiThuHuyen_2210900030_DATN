namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class ProductDetailVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string? Description { get; set; }
        public string? Contents { get; set; }
        public string? CategoryName { get; set; }

        public List<string> Images { get; set; } = new();
    }
}
