using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Order.DataModel.Models;

namespace Order.DataModel.Context;

public partial class MicroServiceShopOrderContext : DbContext
{
    public MicroServiceShopOrderContext()
    {
    }

    public MicroServiceShopOrderContext(DbContextOptions<MicroServiceShopOrderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order.DataModel.Models.Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<ProductInfo> ProductInfo { get; set; }
    public virtual DbSet<ProductStock> ProductStocks { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-A7IIOP1\\LOCALSQL;Database=MicroServiceShopOrder;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order.DataModel.Models.Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDateFa).HasMaxLength(35);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(38, 0)");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.Property(e => e.ProductTitle).HasMaxLength(500);
            entity.Property(e => e.Quantity).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(38, 0)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");
        });
        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.ToTable("ProductInfo");
            entity.Property(e => e.Code).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Title).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Picture).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(38, 0)").IsRequired();
            entity.Property(e => e.ColorTitle).HasMaxLength(50).IsRequired();
            entity.Property(e => e.CategotyTitle).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CategotyImageUrl).HasMaxLength(500);
            entity.Property(e => e.BrandTitle).HasMaxLength(100).IsRequired();
            entity.Property(e => e.BrandLogo).HasMaxLength(500);
            entity.Property(e => e.CreateDateTime).IsRequired();
        });
        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.ToTable("ProductStock");
            entity.Property(e => e.ProductStockId).HasColumnName("ProductStockID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ProductStock_Order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
