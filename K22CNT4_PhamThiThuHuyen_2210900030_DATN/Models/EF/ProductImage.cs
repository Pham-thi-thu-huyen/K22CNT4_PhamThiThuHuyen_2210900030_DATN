using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("PRODUCT_IMAGES")]
public partial class ProductImage
{
    [Key]
    [Column("PRODUCT_IMAGESID")]
    public long ProductImagesid { get; set; }

    [Column("PRODUCTID")]
    public long Productid { get; set; }

    [Column("COLORID")]
    public long? Colorid { get; set; }

    [Column("URLIMG")]
    [StringLength(250)]
    public string Urlimg { get; set; } = null!;

    [Column("ISDEFAULT")]
    public byte? Isdefault { get; set; }

    [ForeignKey("Colorid")]
    [InverseProperty("ProductImages")]
    public virtual Color? Color { get; set; }

    [ForeignKey("Productid")]
    [InverseProperty("ProductImages")]
    public virtual Product Product { get; set; } = null!;
}
