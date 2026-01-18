using System.ComponentModel.DataAnnotations;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

}
