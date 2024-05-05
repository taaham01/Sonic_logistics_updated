using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

[Table("RFQ")]
public partial class Rfq
{
    [Key]
    [Column("rfq_id")]
    public int RfqId { get; set; }

    [Column("operational_unit")]
    [StringLength(50)]
    [Unicode(false)]
    public string OperationalUnit { get; set; } = null!;

    [Column("Shipping_address")]
    [StringLength(100)]
    [Unicode(false)]
    public string ShippingAddress { get; set; } = null!;

    [Column("create_date", TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column("prod_ID")]
    public int ProductName { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("Item_discription")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ItemDiscription { get; set; }

    [Column("currency")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Currency { get; set; }

 
    [InverseProperty("Rfq")]
    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();
}
