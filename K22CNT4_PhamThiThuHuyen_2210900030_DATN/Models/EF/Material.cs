using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("MATERIAL")]
public partial class Material
{
    [Key]
    [Column("MATERIALID")]
    public long Materialid { get; set; }

    [Column("NAME")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("NOTES", TypeName = "ntext")]
    public string? Notes { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [InverseProperty("Material")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
