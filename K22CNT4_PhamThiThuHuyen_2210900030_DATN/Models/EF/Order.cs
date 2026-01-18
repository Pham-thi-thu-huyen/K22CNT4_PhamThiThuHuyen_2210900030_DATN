using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("ORDERS")]
public partial class Order
{
    [Key]
    [Column("ID")]
    public long Ordersid { get; set; }

    [Column("ORDERS_DATE", TypeName = "datetime")]
    public DateTime? OrdersDate { get; set; }

    [Column("CUSTOMERID")]
    public long? Customerid { get; set; }   

    [Column("TOTAL_MONEY", TypeName = "decimal(18, 2)")]
    public decimal? TotalMoney { get; set; }

    [Column("NAME_RECEIVER")]
    [StringLength(250)]
    public string? NameReceiver { get; set; }

    [Column("ADDRESS")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("PHONE")]
    [StringLength(50)]
    public string? Phone { get; set; }

    [Column("ISDELETE")]
    public byte? Isdelete { get; set; }

    [Column("ISACTIVE")]
    public byte? Isactive { get; set; }

    [ForeignKey("Customerid")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Orders")]
    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();
}
