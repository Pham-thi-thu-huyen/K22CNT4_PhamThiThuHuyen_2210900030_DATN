using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("SIZE")]
public partial class Size
{
    [Key]
    [Column("SIZEID")]
    public long Sizeid { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("AGE_FROM")]
    public int? AgeFrom { get; set; }

    [Column("AGE_TO")]
    public int? AgeTo { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [InverseProperty("Size")]
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
