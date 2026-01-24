namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class UserManageVM
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; }     // ADMIN / CUSTOMER
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

}
