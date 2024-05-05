using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

[Table("PO")]
public partial class Po
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("Sup_id")]
    public int SupId { get; set; }

    [Column("prod_ID")]
    public int ProdId { get; set; }

    [Column("order_date/time", TypeName = "datetime")]
    public DateTime OrderDateTime { get; set; }

    [Column("shipped_date")]
    public DateOnly ShippedDate { get; set; }

    [Column("address")]
    [StringLength(50)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    [Unicode(false)]
    public string City { get; set; } = null!;

    [Column("country")]
    [StringLength(50)]
    [Unicode(false)]
    public string Country { get; set; } = null!;

    [Column("status")]
    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("rfq_id")]
    public int RfqId { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Grn> Grns { get; set; } = new List<Grn>();

    [ForeignKey("ProdId")]
    [InverseProperty("Pos")]
    public virtual Product Prod { get; set; } = null!;

    [ForeignKey("RfqId")]
    [InverseProperty("Pos")]
    public virtual Rfq Rfq { get; set; } = null!;

    [ForeignKey("SupId")]
    [InverseProperty("Pos")]
    public virtual Supplier Sup { get; set; } = null!;
}
