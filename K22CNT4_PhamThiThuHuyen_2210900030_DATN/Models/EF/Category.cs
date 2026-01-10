using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("CATEGORY")]
public partial class Category
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("NAME")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("ICON")]
    [StringLength(250)]
    public string? Icon { get; set; }

    [Column("PARENTID")]
    public long? Parentid { get; set; }

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

    [InverseProperty("Parent")]
    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    [ForeignKey("Parentid")]
    [InverseProperty("InverseParent")]
    public virtual Category? Parent { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
