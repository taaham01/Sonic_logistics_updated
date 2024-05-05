using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

[Table("Invoice")]
public partial class Invoice
{
    [Key]
    [Column("Invoice ID")]
    public int InvoiceId { get; set; }

    [Column("Operating Unit")]
    [StringLength(50)]
    [Unicode(false)]
    public string OperatingUnit { get; set; } = null!;

    [Column("Invoice Date", TypeName = "datetime")]
    public DateTime InvoiceDate { get; set; }

    [Column("Invoice Currency")]
    [StringLength(50)]
    [Unicode(false)]
    public string InvoiceCurrency { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Discription { get; set; } = null!;

    [Column("Payment Method")]
    [StringLength(50)]
    [Unicode(false)]
    public string PaymentMethod { get; set; } = null!;

    [Column("Total Amount")]
    public int TotalAmount { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("Sup_id")]
    public int SupId { get; set; }

    [Column("prod_ID")]
    public int ProdId { get; set; }

    [Column("rfq_id")]
    public int RfqId { get; set; }

    [Column("GRN ID")]
    public int GrnId { get; set; }

    [ForeignKey("GrnId")]
    [InverseProperty("Invoices")]
    public virtual Grn Grn { get; set; } = null!;
}
