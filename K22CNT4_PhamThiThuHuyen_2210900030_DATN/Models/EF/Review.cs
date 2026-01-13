using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("REVIEW")]
public partial class Review
{
    [Key]
    [Column("ID")]
    public long Reviewid { get; set; }

    [Column("PRODUCTID")]
    public long Productid { get; set; }

    [Column("CUSTOMERID")]
    public long? Customerid { get; set; }

    [Column("FULLNAME")]
    [StringLength(250)]
    public string Fullname { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("RATING")]
    public byte Rating { get; set; }

    [Column("TITLE")]
    [StringLength(500)]
    public string? Title { get; set; }

    [Column("CONTENT", TypeName = "ntext")]
    public string Content { get; set; } = null!;

    [Column("CREATED_DATE", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("IS_APPROVED")]
    public byte? IsApproved { get; set; }

    [Column("REPLY", TypeName = "ntext")]
    public string? Reply { get; set; }

    [Column("REPLY_DATE", TypeName = "datetime")]
    public DateTime? ReplyDate { get; set; }

    [Column("ISDELETE")]
    public byte? Isdelete { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [ForeignKey("Customerid")]
    [InverseProperty("Reviews")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("Productid")]
    [InverseProperty("Reviews")]
    public virtual Product Product { get; set; } = null!;
}
