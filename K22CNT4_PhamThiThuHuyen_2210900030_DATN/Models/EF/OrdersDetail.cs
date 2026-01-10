using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace K22CNT4_PhamThiThuHuyen_2210900030_DATN.Models.EF;

[Table("ORDERS_DETAILS")]
public partial class OrdersDetail
{
    [Key]
    [Column("ORDERS_DETAILSID")]
    public long OrdersDetailsid { get; set; }

    [Column("ORDERSID")]
    public long Ordersid { get; set; }

    [Column("PRODUCTVARIANTID")]
    public long Productvariantid { get; set; }

    [Column("PRICE", TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    [Column("QUANTITY")]
    public int? Quantity { get; set; }

    [Column("TOTAL", TypeName = "decimal(18, 2)")]
    public decimal? Total { get; set; }

    [ForeignKey("Ordersid")]
    [InverseProperty("OrdersDetails")]
    public virtual Order Orders { get; set; } = null!;

    [ForeignKey("Productvariantid")]
    [InverseProperty("OrdersDetails")]
    public virtual ProductVariant Productvariant { get; set; } = null!;
}
