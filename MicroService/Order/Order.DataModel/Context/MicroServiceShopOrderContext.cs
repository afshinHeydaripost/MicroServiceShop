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

    public virtual DbSet<Models.Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<ProductInfo> ProductInfos { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\DBSQL;Database=MicroServiceShopOrder;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Order_1");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.FinalizedDateTime).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDateFa)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderNo)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RevokedDateTime).HasColumnType("datetime");
            entity.Property(e => e.RevokedUserId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("null=>curent ");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(38, 0)");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK_OrderItem_1");

            entity.ToTable("OrderItem");

            entity.Property(e => e.OrderItemId).ValueGeneratedNever();
            entity.Property(e => e.BrandTitle)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CategotyTitle)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ColorTitle)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.ProductCode)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ProductTitle)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.Quantity).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(38, 0)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_ProductInfo");
        });

        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.ToTable("ProductInfo");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.BrandTitle)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategotyTitle)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ColorId).HasColumnName("ColorID");
            entity.Property(e => e.ColorTitle)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.LastUpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(38, 0)");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(300);
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.ToTable("ProductStock");

            entity.Property(e => e.ProductStockId).HasColumnName("ProductStockID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_ProductStock_Order");

            entity.HasOne(d => d.ProductInfo).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.ProductInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductStock_ProductInfo");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            entity.Property(e => e.UserCode)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
