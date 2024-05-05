using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Soniclogistics_updated.Models;

public partial class SoniclogisticsDbContext : DbContext
{
    public SoniclogisticsDbContext()
    {
    }

    public SoniclogisticsDbContext(DbContextOptions<SoniclogisticsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Grn> Grns { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Po> Pos { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rfq> Rfqs { get; set; }

    public virtual DbSet<SalesQuote> SalesQuotes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8CNU755;Database=SonicLogistics1;Integrated Security=True; User Id=DESKTOP-8CNU755;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grn>(entity =>
        {
            entity.Property(e => e.GrnId).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.Grns)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GRN_PO");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.InvoiceId).ValueGeneratedNever();

            entity.HasOne(d => d.Grn).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_PO_GRN");
        });

        modelBuilder.Entity<Po>(entity =>
        {
            entity.Property(e => e.OrderId).ValueGeneratedNever();

            entity.HasOne(d => d.Prod).WithMany(p => p.Pos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_Products");

            entity.HasOne(d => d.Rfq).WithMany(p => p.Pos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_RFQ");

            entity.HasOne(d => d.Sup).WithMany(p => p.Pos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PO_Supplier");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProdId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Rfq>(entity =>
        {
            entity.Property(e => e.RfqId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SalesQuote>(entity =>
        {
            entity.Property(e => e.SqId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
