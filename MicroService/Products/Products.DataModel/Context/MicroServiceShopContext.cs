using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Products.DataModel.Models;

namespace Products.DataModel.Context;

public partial class MicroServiceShopContext : DbContext
{
    public MicroServiceShopContext()
    {
    }

    public MicroServiceShopContext(DbContextOptions<MicroServiceShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\DBSQL;Database=MicroServiceShop;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_Producer");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Logo).HasMaxLength(500);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.DiscountRate).HasColumnName("DiscountRate");
            entity.Property(e => e.DiscountPrice).HasColumnName("DiscountPrice");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.ValidityDate).HasColumnType("datetime");

            entity.HasOne(d => d.Brand).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Discount_Brand");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Discount_ProductCategory");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Discount_Product");

            entity.HasOne(d => d.ProductModel).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductModelId)
                .HasConstraintName("FK_Discount_ProductModel");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.Picture).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_Product_Producer");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.ToTable("ProductColor");

            entity.Property(e => e.ProductColorId).HasColumnName("ProductColorID");
            entity.Property(e => e.Rgb)
                .IsRequired()
                .HasMaxLength(7)
                .IsFixedLength()
                .HasColumnName("RGB");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("ProductModel");

            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.ColorId).HasColumnName("ColorID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Color).WithMany(p => p.ProductModels)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK_ProductModel_ProductColor");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductModels)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductModel_Product");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.ToTable("ProductStock");

            entity.Property(e => e.ProductStockId).HasColumnName("ProductStockID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductStocks)
                .HasForeignKey(d => d.ProductModelId)
                .HasConstraintName("FK_ProductStock_ProductModel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
