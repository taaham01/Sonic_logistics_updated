using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

[Table("Sales_quote")]
public partial class SalesQuote
{
    [Key]
    [Column("SQ_ID")]
    public int SqId { get; set; }

    [Column("SQ NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string SqName { get; set; } = null!;

    [Column("CUSTOMER")]
    [StringLength(50)]
    [Unicode(false)]
    public string Customer { get; set; } = null!;

    [Column("CONTACT")]
    [StringLength(50)]
    [Unicode(false)]
    public string Contact { get; set; } = null!;

    [Column("Product_ID")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column("UOM")]
    [StringLength(10)]
    [Unicode(false)]
    public string Uom { get; set; } = null!;

    [Column("Price_list")]
    [StringLength(50)]
    [Unicode(false)]
    public string PriceList { get; set; } = null!;

    [Column("Unit_Selling_Price")]
    public int UnitSellingPrice { get; set; }

    [Column("Total_Price")]
    public int TotalPrice { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Discription { get; set; } = null!;

    [Column("Product_avalibility")]
    [StringLength(50)]
    [Unicode(false)]
    public string ProductAvalibility { get; set; } = null!;

    [Column("Create date", TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column("expire date", TypeName = "datetime")]
    public DateTime ExpireDate { get; set; }
}
