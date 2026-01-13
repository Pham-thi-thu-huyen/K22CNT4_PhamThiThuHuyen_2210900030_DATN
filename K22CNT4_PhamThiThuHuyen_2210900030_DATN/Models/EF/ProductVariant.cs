using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("PRODUCT_VARIANT")]
public partial class ProductVariant
{
    [Key]
    [Column("ID")]
    public long ProductVariantid { get; set; }

    [Column("PRODUCTID")]
    public long Productid { get; set; }

    [Column("COLORID")]
    public long Colorid { get; set; }

    [Column("SIZEID")]
    public long Sizeid { get; set; }

    [Column("MATERIALID")]
    public long Materialid { get; set; }

    [Column("PRICE", TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [Column("QUANTITY")]
    public int? Quantity { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [ForeignKey("Colorid")]
    [InverseProperty("ProductVariants")]
    public virtual Color Color { get; set; } = null!;

    [ForeignKey("Materialid")]
    [InverseProperty("ProductVariants")]
    public virtual Material Material { get; set; } = null!;

    [InverseProperty("Productvariant")]
    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    [ForeignKey("Productid")]
    [InverseProperty("ProductVariants")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("Sizeid")]
    [InverseProperty("ProductVariants")]
    public virtual Size Size { get; set; } = null!;
}
