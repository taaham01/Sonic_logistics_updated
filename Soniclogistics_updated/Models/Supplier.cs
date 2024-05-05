using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

[Table("Supplier")]
public partial class Supplier
{
    [Key]
    [Column("Sup_id")]
    public int SupId { get; set; }

    [Column("Supplier_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string SupplierName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Contact { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public byte[]? Website { get; set; }

    [InverseProperty("Sup")]
    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();
}
