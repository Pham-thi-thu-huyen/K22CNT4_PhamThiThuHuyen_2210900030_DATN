using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("PRODUCT")]
public partial class Product
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("NAME")]
    [StringLength(500)]
    public string Name { get; set; } = null!;

    [Column("DESCRIPTION", TypeName = "ntext")]
    public string? Description { get; set; }

    [Column("CONTENTS", TypeName = "ntext")]
    public string? Contents { get; set; }

    [Column("CATEGORYID")]
    public long? Categoryid { get; set; }

    [Column("PRICE", TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    // 🔹 GENDER (THÊM MỚI)
    // 1 = Bé gái
    // 2 = Bé trai
    [Column("GENDER")]
    public int Gender { get; set; }

    [Column("SLUG")]
    [StringLength(160)]
    public string? Slug { get; set; }

    [Column("META_TITLE")]
    [StringLength(160)]
    public string? MetaTitle { get; set; }

    [Column("META_KEYWORD")]
    [StringLength(500)]
    public string? MetaKeyword { get; set; }

    [Column("META_DESC")]
    [StringLength(500)]
    public string? MetaDesc { get; set; }

    [Column("CREATED_DATE", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("ISDELETE")]
    public byte? Isdelete { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [Column("ISSALE")]
    public byte? Issale { get; set; }

    [Column("ISTOPSALE")]
    public byte? Istopsale { get; set; }

    [Column("ISHOME")]
    public byte? Ishome { get; set; }

    // 🔹 RELATIONSHIP
    [ForeignKey("Categoryid")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    [InverseProperty("Product")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
