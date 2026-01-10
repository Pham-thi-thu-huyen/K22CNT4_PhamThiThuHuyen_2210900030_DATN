using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("CUSTOMER")]
[Index("Email", Name = "UQ__CUSTOMER__161CF7247B92D8CB", IsUnique = true)]
[Index("Username", Name = "UQ__CUSTOMER__B15BE12EF6D38386", IsUnique = true)]
public partial class Customer
{
    [Key]
    [Column("CUSTOMERID")]
    public long Customerid { get; set; }

    [Column("USERNAME")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("PASSWORD")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(150)]
    public string? Email { get; set; }

    [Column("PHONE")]
    [StringLength(50)]
    public string? Phone { get; set; }

    [Column("ADDRESS")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("AVATAR")]
    [StringLength(250)]
    public string? Avatar { get; set; }

    [Column("USER_TYPE")]
    [StringLength(20)]
    public string UserType { get; set; } = null!;

    [Column("CREATED_DATE", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("ISDELETE")]
    public byte? Isdelete { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Customer")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
