using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

[Table("GRN")]
public partial class Grn
{
    [Key]
    [Column("GRN ID")]
    public int GrnId { get; set; }

    [Column("GRN Date", TypeName = "datetime")]
    public DateTime GrnDate { get; set; }

    [Column("prod_ID")]
    public int ProdId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Warehouse { get; set; } = null!;

    [Column("Batch No")]
    public int BatchNo { get; set; }

    [Column("Approved Warehouse")]
    [StringLength(300)]
    [Unicode(false)]
    public string ApprovedWarehouse { get; set; } 

    [Column("Unapproved Warehouse")]
    [StringLength(300)]
    [Unicode(false)]
    public int UnapprovedWarehouse { get; set; } 

    [Column("Sup_id")]
    public int SupId { get; set; }

    [Column("rfq_id")]
    public int RfqId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [InverseProperty("Grn")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [ForeignKey("OrderId")]
    [InverseProperty("Grns")]
    public virtual Po Order { get; set; } = null!;
}
