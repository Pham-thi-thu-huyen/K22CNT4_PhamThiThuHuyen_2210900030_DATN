using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("COLOR")]
public partial class Color
{
    [Key]
    [Column("ID")]
    public long Colorid { get; set; }

    [Column("NAME")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("CODE")]
    [StringLength(50)]
    public string? Code { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [InverseProperty("Color")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Color")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
