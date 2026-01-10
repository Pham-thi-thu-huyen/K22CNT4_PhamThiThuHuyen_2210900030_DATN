using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("PAY_METHOD")]
public partial class PayMethod
{
    [Key]
    [Column("PAY_METHODID")]
    public long PayMethodid { get; set; }

    [Column("NAME")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("NOTES", TypeName = "ntext")]
    public string? Notes { get; set; }

    [Column("CREATED_DATE", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("ISDELETE")]
    public byte? Isdelete { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }
}
