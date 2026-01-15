using K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class HomeVM
    {
        public List<CategoryVM> Categories { get; set; }

        public List<ProductVM> FeaturedProducts { get; set; }
        public int FeaturedPage { get; set; }
        public int FeaturedTotalPages { get; set; }

        public List<ProductVM> SaleProducts { get; set; }
        public int SalePage { get; set; }
        public int SaleTotalPages { get; set; }
    }
    public class CategoryVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
