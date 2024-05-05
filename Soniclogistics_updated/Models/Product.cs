using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

public partial class Product
{
    [Key]
    [Column("prod_ID")]
    public int ProdId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [Column("UOM")]
    [StringLength(50)]
    [Unicode(false)]
    public string Uom { get; set; } = null!;

    public int Price { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Details { get; set; }

    [Column("Product_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string ProductName { get; set; } = null!;

    [InverseProperty("Prod")]
    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();
}
