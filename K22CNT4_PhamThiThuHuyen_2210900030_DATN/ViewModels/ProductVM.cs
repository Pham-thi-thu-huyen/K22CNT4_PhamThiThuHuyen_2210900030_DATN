namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class ProductVM
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Image { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
