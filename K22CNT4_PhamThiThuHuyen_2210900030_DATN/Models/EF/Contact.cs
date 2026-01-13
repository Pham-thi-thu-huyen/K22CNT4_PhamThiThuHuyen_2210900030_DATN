using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("CONTACT")]
public partial class Contact
{
    [Key]
    [Column("ID")]
    public long Contactid { get; set; }

    [Column("FULLNAME")]
    [StringLength(250)]
    public string Fullname { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(150)]
    public string Email { get; set; } = null!;

    [Column("PHONE")]
    [StringLength(50)]
    public string? Phone { get; set; }

    [Column("SUBJECT")]
    [StringLength(500)]
    public string? Subject { get; set; }

    [Column("MESSAGE", TypeName = "ntext")]
    public string Message { get; set; } = null!;

    [Column("CREATED_DATE", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("IS_READ")]
    public byte? IsRead { get; set; }

    [Column("REPLY", TypeName = "ntext")]
    public string? Reply { get; set; }

    [Column("REPLY_DATE", TypeName = "datetime")]
    public DateTime? ReplyDate { get; set; }

    [Column("ISDELETE")]
    public byte? Isdelete { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }
}
